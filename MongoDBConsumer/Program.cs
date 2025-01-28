using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnergySimulation;
using System.Configuration;
using System.Collections.Specialized;
using System.Xml;
using Microsoft.Extensions.Configuration;
using MassTransit;
using System.Security.Principal;
using System.Text.Json;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace Simulation
{
    internal class Program
    {
        private static IMongoCollection<BsonDocument> _mongoCollection;

        static async Task Main(string[] args)
        {
            // MongoDB Setup
            // "mongodb://<db_username>:<db_password>@<hostname>:<port>/?authSource=users";
            var mongoClient = new MongoClient("mongodb://root:password@rp5.tail08f7ce.ts.net:27017/?authSource=admin");

            var database = mongoClient.GetDatabase("my_database");
            _mongoCollection = database.GetCollection<BsonDocument>("meteringPoints");

            // Configure MassTransit with RabbitMQ
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("rp5.tail08f7ce.ts.net", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ReceiveEndpoint("ConsumerQueue", e =>
                {
                    e.Consumer<MessageConsumer>();
                });
            });

            await busControl.StartAsync();

            try
            {
                Console.WriteLine("Press any key to exit");
                await Task.Run(() => Console.Read());
            }
            finally
            {
                // Stop the bus
                await busControl.StopAsync();
            }
        }
        public class MessageConsumer : IConsumer<EnergySimulation.MeteringPoint>
        {
            public async Task Consume(ConsumeContext<EnergySimulation.MeteringPoint> context)
            {
                try {
                MeteringPoint newPoint = context.Message;
                Console.WriteLine($"Received message: {newPoint.Timestamp}");

                var document = new BsonDocument
                {
                    { "name", newPoint.MeteringPointName },
                    { "meteringPointNumber", newPoint.MeteringPointNumber },
                    { "meterReading", (long)newPoint.MeterReading },
                    { "operatingState", (int)newPoint.Status },
                    { "type", newPoint.Type.ToString() },
                    { "timestamp", DateTime.Now }
                };

                _mongoCollection.InsertOne(document);
                Console.WriteLine($"Sent to MongoDB: {newPoint.Timestamp}");
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                await Task.CompletedTask;
            }
        }
    }
}