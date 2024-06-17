//Publisherde edilmesi lazim olanlar
//1.RabbitMQ-ya bağlantı qurulması
//2. Baglantını aktivləşdirmək və kanal açmaq
//3. Queue olushturmaq
//4. Queue ya mesaj gondermek


using RabbitMQ.Client;
using System.Text;

//baglanti olushturmaq
ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://mukxipkr:gYUrkZlYfZpCMrPRfKW1DXKsZ7JOG_20@shark.rmq.cloudamqp.com/mukxipkr");

//Baglantini aktivleshtirme
using IConnection connection = factory.CreateConnection();

//Kanal acmaq
using IModel channel = connection.CreateModel();

//Queue olushtumaq
channel.QueueDeclare(queue: "example-queue", exclusive: false, durable: true); // queue nun qalici olmasi ucun etdiyimiz configuration durable : true

//Queue-a mesaj gonderme
//RabbitMQ queue atilan mesajlari byte tipinden qebul edir. Mesajlari byte cast etmek lazimdir.

IBasicProperties properties = channel.CreateBasicProperties();  //Mesajin qalici olmasi ucun edilen configuration
properties.Persistent = true;

for (int i = 0; i < 100; i++)
{
    byte[] message = Encoding.UTF8.GetBytes("Merhaba");
    channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message, basicProperties:properties);
}

Console.Read();