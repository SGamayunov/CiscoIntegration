using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using CiscoIntegration.Models;
using CiscoIntegration.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CiscoIntegration.Controllers
{
    [Route("api/cisco")]
    [ApiController]
    public class CiscoController : ControllerBase
    {
        private ILogger<CiscoController> Logger { get; }
        private RabbitSenderService MqSender { get; }
        public CiscoController(ILogger<CiscoController> logger, RabbitSenderService mqSender)
        {
            Logger = logger;
            MqSender = mqSender;
        }

        [Route("event")]
        [HttpPost]
        public string CiscoEvent()
        {
            Logger.LogInformation("POST ");
            return "OK";
        }

        [Route("event")]
        [HttpPut]
        public string CiscoEventPut()
        {
            string body;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                body = reader.ReadToEndAsync().Result;
            }
            Logger.LogInformation("PUT " + body);


            var doc = XDocument.Parse(body);
            CallData data = new CallData();
            data.Address = doc.Root.Element("Address")?.Value;
            data.Agent = doc.Root.Element("Agent")?.Value;
            data.Ext = doc.Root.Element("Ext")?.Value;
            data.ANI = doc.Root.Element("ANI")?.Value;
            data.Lang = doc.Root.Element("Lang")?.Value;

            string jsonData = JsonConvert.SerializeObject(data);
            MqSender.Send(jsonData);
            return "OK";
        }

        [Route("event")]
        [HttpGet]
        public string CiscoEventGet()
        {
            return "GET";
        }
    }
}
