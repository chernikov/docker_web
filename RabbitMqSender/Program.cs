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

    // Create a message
    var rand = new Random();

    
    var key = ' ';
    while (key != 'q')
    {
        var next = rand.Next(100);
        string message = $"Hello, {next}";
        var body = Encoding.UTF8.GetBytes(message);


        channel.BasicPublish(exchange: "", routingKey: "my_queue", basicProperties: null, body: body);
        Console.WriteLine("Message sent: {0}", message);
        Console.WriteLine("Press `q` to exit.");
        key = Console.ReadKey().KeyChar;
    }
    // Publish the message to the queue
    
}


Console.ReadLine();