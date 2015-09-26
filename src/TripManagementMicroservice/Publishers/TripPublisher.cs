using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Experiments.DomainServices;
using RabbitMQ.Client;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace TripManagementMicroservice.Publishers
{
    public class TripPublisher : ITripPublisher
    {

        #region properties

        protected readonly IConnectionFactory connectionFactory = null;
        protected readonly IConnection connection = null;
        protected readonly IModel channel = null;

        #endregion
        
        #region constructor
        
        public TripPublisher(IConnectionFactory connectionFactory)
        {
            if (connectionFactory == null) throw new ArgumentNullException("connectionFactory");
            this.connectionFactory = connectionFactory;

            this.connection = this.connectionFactory.CreateConnection();
            this.channel = this.connection.CreateModel();

            // creates a work queue
            channel.QueueDeclare(
                queue: "unhandled_trips_queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        #endregion

        #region interface

        void ITripPublisher.notifyNewTrip(Trip newTrip)
        {
            var jsonTrip = Newtonsoft.Json.JsonConvert.SerializeObject(newTrip);
            var message = Encoding.UTF8.GetBytes(jsonTrip);

            var properties = this.channel.CreateBasicProperties();

            this.channel.BasicPublish(
                exchange:  "",
                routingKey:  "unhandled_trips_queue", 
                basicProperties: properties, 
                body: message);
        }

        #endregion

    }
}
