using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Simulation;

// Mit Hilfe von Jonathan Mandl erstellt

namespace Consumer;


internal class Program
{
    private static IMongoCollection<BsonDocument> _mongoCollection = null!;

    static async Task Main(string[] args)
    {
        // MongoDB Setup
        // "mongodb://<db_username>:<db_password>@<hostname>:<port>/?authSource=users";
        var mongoClient = new MongoClient("mongodb://root:password@localhost:27017");

        var database = mongoClient.GetDatabase("my_database");

        database.GetCollection<BsonDocument>("meteringPoints");
        _mongoCollection = database.GetCollection<BsonDocument>("meteringPoints");

        // Configure MassTransit with RabbitMQ
        var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.Host("localhost", h =>
            {
            });
            cfg.ReceiveEndpoint("SmartMeter", e =>
            {
                e.Bind("SmartMeter");
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

    public class MessageConsumer : IConsumer<MeteringPoint>
    {
        public async Task Consume(ConsumeContext<MeteringPoint> context)
        {
            try
            {
                MeteringPoint meteringPoint = context.Message;
                Console.WriteLine($"Received message: {meteringPoint.Timestamp}");

                var document = new BsonDocument
                {
                    { "meteringPointNumber", meteringPoint.MeteringPointNumber.ToString() },
                    { "meterReading", meteringPoint.MeterReading },
                    { "operatingState", meteringPoint.Status },
                    { "timestamp", meteringPoint.Timestamp }
                };

                _mongoCollection.InsertOne(document);
                Console.WriteLine($"Sent to MongoDB: {meteringPoint.Timestamp}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            await Task.CompletedTask;
        }
    }
}