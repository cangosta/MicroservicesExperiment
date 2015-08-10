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

        protected readonly IConnectionFactory exchangeConnectionFactory = null;
        protected readonly IConnection exchangeConnection = null;
        protected readonly IModel exchangeChannel = null;

        #endregion
        
        #region constructor
        
        public TripPublisher(IConnectionFactory exchangeConnectionFactory)
        {
            if (exchangeConnectionFactory == null) throw new ArgumentNullException("exchangeConnectionFactory");
            this.exchangeConnectionFactory = exchangeConnectionFactory;

            this.exchangeConnection = this.exchangeConnectionFactory.CreateConnection();
            this.exchangeChannel = this.exchangeConnection.CreateModel();

            // creates the fanout exchange
            this.exchangeChannel.ExchangeDeclare("trips", "fanout");
        }

        #endregion

        #region interface

        void ITripPublisher.notifyNewTrip(Trip newTrip)
        {
            var jsonTrip = Newtonsoft.Json.JsonConvert.SerializeObject(newTrip);
            var message = Encoding.UTF8.GetBytes(jsonTrip);
            this.exchangeChannel.BasicPublish("trips", "", null, message);
        }

        #endregion

    }
}
