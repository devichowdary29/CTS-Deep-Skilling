using Confluent.Kafka;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaChatApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string topic = "chat-topic";
            string bootstrapServers = "localhost:9092";

            Console.WriteLine("=== Welcome to Kafka Console Chat ===");
            Console.WriteLine("Type '/exit' to quit.\n");
            
            Console.Write("Enter your chat username: ");
            string username = Console.ReadLine() ?? "Anonymous";

            // Consumer configuration
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                // Use a unique group ID so every instance receives all messages in a broadcast fashion
                GroupId = "chat-group-" + Guid.NewGuid().ToString(),
                AutoOffsetReset = AutoOffsetReset.Latest
            };

            // Producer configuration
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = bootstrapServers
            };

            var cts = new CancellationTokenSource();

            // Run consumer on a background thread
            _ = Task.Run(() =>
            {
                using var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
                consumer.Subscribe(topic);

                try
                {
                    while (!cts.Token.IsCancellationRequested)
                    {
                        var consumeResult = consumer.Consume(cts.Token);
                        
                        // Clear current line for clean output if possible, then rewrite prompt
                        Console.WriteLine($"\n[Incoming] {consumeResult.Message.Value}");
                        Console.Write($"{username} > ");
                    }
                }
                catch (OperationCanceledException)
                {
                    consumer.Close();
                }
            });

            // Run producer on the main thread
            using var producer = new ProducerBuilder<Null, string>(producerConfig).Build();
            
            while (true)
            {
                Console.Write($"{username} > ");
                string text = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(text)) continue;
                
                if (text.ToLower() == "/exit")
                {
                    cts.Cancel();
                    break;
                }

                string formattedMessage = $"{username}: {text}";
                await producer.ProduceAsync(topic, new Message<Null, string> { Value = formattedMessage });
            }
        }
    }
}
