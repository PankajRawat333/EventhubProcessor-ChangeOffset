using EventHubProcessor1;
using Microsoft.ServiceBus.Messaging;
using System;

namespace EventHubProcessorNew1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string eventHubConnectionString = "Endpoint=**************.servicebus.windows.net/;SharedAccessKeyName=AlertingListen;SharedAccessKey==**************.";
            string eventHubName = "prd-=**************.-eh-01";
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=prdamsussccatsa01;AccountKey==**************;EndpointSuffix=core.windows.net";

            string _guid = Guid.NewGuid().ToString();
            string eventProcessorHostName = _guid;

            EventProcessorHost eventProcessorHost = new EventProcessorHost(
                eventProcessorHostName,
                eventHubName,
                "alerting",
                eventHubConnectionString,
                storageConnectionString, "local-webjobs-eventhub"
                );


            Console.WriteLine("Registering EventProcessor...!");

            eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>(new EventProcessorOptions
            {
                InitialOffsetProvider = (partitionId) => DateTime.UtcNow
            }).Wait();

            Console.WriteLine("Press enter to stop");
            Console.Read();
            eventProcessorHost.UnregisterEventProcessorAsync().Wait();
        }
    }
}