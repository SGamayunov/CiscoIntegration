using Steeltoe.Messaging.RabbitMQ.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CiscoIntegration.Services
{
    public class RabbitSenderService
    {
        private IRabbitTemplate MqTemplate { get; }
        public RabbitSenderService(IRabbitTemplate template)
        {
            MqTemplate = template;
        }

        public void Send(string data)
        {
            MqTemplate.ConvertAndSend("cisco", "", data);
        }
    }
}
