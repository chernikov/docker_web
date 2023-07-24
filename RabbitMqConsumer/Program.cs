// Create a connection factory
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory
{
    HostName = "localhost", // Replace with your RabbitMQ server's hostname
    UserName = "guest",     // Replace with your RabbitMQ username
    Password = "guest"      // Replace with your RabbitMQ password
    
};

using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    // Declare a queue
    channel.QueueDeclare(queue: "my_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

    // Create a consumer and set up event handling for received messages
    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);

        Console.WriteLine("Message received: {0}", message);
    };

    // Start consuming messages from the queue
    channel.BasicConsume(queue: "my_queue", autoAck: true, consumer: consumer);

    Console.WriteLine("Waiting for messages. Press any key to exit.");
    Console.ReadLine();
}