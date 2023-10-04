using RabbitMQ.Client;
using System;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            Port = 15672,
            UserName = "guest",
            Password = "guest"

        };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            // Exchange Direct
            channel.ExchangeDeclare("direct_exchange", ExchangeType.Direct, true);
            var directMessage = "Mensaje para el exchange direct";
            var directBody = Encoding.UTF8.GetBytes(directMessage);

            channel.BasicPublish("direct_exchange", "routing_key", null, directBody);

            Console.WriteLine($"Enviado al exchange direct: {directMessage}");

            // Exchange Topic
            channel.ExchangeDeclare("topic_exchange", ExchangeType.Topic);
            var topicMessage = "Mensaje para el exchange topic";
            var topicBody = Encoding.UTF8.GetBytes(topicMessage);

            channel.BasicPublish("topic_exchange", "app.example", null, topicBody);

            Console.WriteLine($"Enviado al exchange topic: {topicMessage}");

            // Exchange Fanout
            channel.ExchangeDeclare("fanout_exchange", ExchangeType.Fanout);
            var fanoutMessage = "Mensaje para el exchange fanout";
            var fanoutBody = Encoding.UTF8.GetBytes(fanoutMessage);

            channel.BasicPublish("fanout_exchange", "", null, fanoutBody);

            Console.WriteLine($"Enviado al exchange fanout: {fanoutMessage}");
        }
    }
}