using Experiments.DomainServices;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverManagementMicroservice.Subscribers
{
    public class TripsSubscriber
    {

        #region properties

        protected readonly IConnectionFactory connectionFactory = null;
        protected readonly IConnection connection = null;
        protected readonly IModel channel = null;
        protected readonly IDriversDomainService driversDomainService = null;

        #endregion

        #region constructor

        public TripsSubscriber(
            IDriversDomainService driversDomainService)
        {
            this.driversDomainService = driversDomainService;

            this.connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            this.connection = this.connectionFactory.CreateConnection();
            this.channel = this.connection.CreateModel();

            // creates a work queue
            channel.QueueDeclare(
                queue: "unhandled_trips_queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
        }

        #endregion

        #region public

        public void Listen()
        {

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += newTripHandler;
            channel.BasicConsume(
                queue: "unhandled_trips_queue",
                noAck: false,
                consumer: consumer);
            Console.WriteLine(" [*] Waiting for messages.");

        }

        #endregion

        #region private

        private void newTripHandler(object model, BasicDeliverEventArgs ea) {

            var body = ea.Body;
            var jsonMessage = Encoding.UTF8.GetString(body);

            Console.WriteLine(" [*] new message arrived... " + jsonMessage);

            var newTrip = Newtonsoft.Json.JsonConvert.DeserializeObject<Trip>(jsonMessage);

            // select driver for trip
            var driver = this.driversDomainService.GetNextDriver();

            // notify selected driver
            notify(driver, newTrip);

            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        }

        private void notify(Driver driver, Trip trip)
        {
            var notification = new Notification() {
                Data = trip,
                Event = string.Format("newTrip_{0}", driver.Id),
                GroupName = "drivers"
            };

            var notificationsClient = new RestClient("http://localhost:5003");
            var request = new RestRequest("api/notifications", Method.POST);
            request.AddJsonBody(notification);

            notificationsClient.Execute(request);
        }

        #endregion
    }
}
