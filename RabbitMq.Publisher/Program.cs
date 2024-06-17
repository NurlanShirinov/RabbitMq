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
channel.QueueDeclare(queue: "example-queue", exclusive: false);

//Queue-a mesaj gonderme
//RabbitMQ queue atilan mesajlari byte tipinden qebul edir. Mesajlari byte cast etmek lazimdir.

for(int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes("Merhaba "+ i);
    channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);
}


Console.Read();