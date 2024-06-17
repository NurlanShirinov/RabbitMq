//Consumerde edilmesi lazim olanlar
//1.RabbitMQ-ya bağlantı qurulması
//2. Baglantını aktivləşdirmək və kanal açmaq
//3. Queue olushturmaq
//4. Queue-dan mesaji oxumaq


//Baglanti olushturmaq
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://mukxipkr:gYUrkZlYfZpCMrPRfKW1DXKsZ7JOG_20@shark.rmq.cloudamqp.com/mukxipkr");

//Baglanti Olushturma
using IConnection connection = factory.CreateConnection();

//Kanal acmaq
using IModel channel = connection.CreateModel();

//Queue Olushturma
channel.QueueDeclare(queue: "example-queue", exclusive: false); // consumerde queue publisherde oldugu kimi eynisi olmalidir

//Queueden Mesaj oxuma
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example-queue", false, consumer);

consumer.Received += (sender , e)=>
{
    //queue gelen mesajin ishlendiyi yer.
    //e.Body : queue daki mesjin datasini hamisini getirecekdir.
    //e.Body.Span ve ya e.Body.ToArray() : queuedaki mesajin byte formasini getirecekdir
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();