using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace DataServer.RabbitMQ
{
    public static class Rabbit
    {

        public delegate void ReceiveDoWork(string message);

        public static void Send(string queue, string message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue, false, false, false, null);
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish("", queue, null, body);
                }
            }
        }

        public static void Receive(string queue, ReceiveDoWork receiveWork)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue, false, false, false, null);
                    
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(message);
                        receiveWork(message);
                    };
                    channel.BasicConsume(queue, true, consumer);

                    Console.WriteLine("Write exit to exit.");
                    string input;
                    do
                    {
                        input = Console.ReadLine();
                    }
                    while (input.ToLower() != "exit");
                }
            }
        }
    }
}
