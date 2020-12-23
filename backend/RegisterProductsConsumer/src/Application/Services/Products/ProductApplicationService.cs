using Application.Builder;
using Confluent.Kafka;
using Domain.Models;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace Application.Services.Products
{
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly IKafkaBuilder _kafkaBuilder;
        private readonly IProductRepository _productRepository;

        public ProductApplicationService(IKafkaBuilder kafkaBuilder, IProductRepository productRepository)
        {
            _kafkaBuilder = kafkaBuilder;
            _productRepository = productRepository;
        }

        public void ReceiveEventMessage()
        {
            var builderConf = _kafkaBuilder.connection();
            Console.WriteLine("Conexão com Kafka realizada com sucesso.");

            using (var c = new ConsumerBuilder<Ignore, string>(builderConf).Build())
            {
                c.Subscribe(_kafkaBuilder.GetTopicName());

                var cts = new CancellationTokenSource();

                Console.CancelKeyPress += (_, e) => {
                    e.Cancel = true; // prevent the process from terminating.
                    cts.Cancel();
                };

                try
                {
                    while (true)
                    {
                        try
                        {
                            var cr = c.Consume(cts.Token);

                            InsertProduct(cr.Value);

                            Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occured: {e.Error.Reason}");
                        }

                        Thread.Sleep(5000);
                    }
                }
                catch (OperationCanceledException)
                {
                    // Ensure the consumer leaves the group cleanly and final offsets are committed.
                    c.Close();
                }
            }
        }

        private void InsertProduct(string product)
        {
            var productObject = JsonSerializer.Deserialize<Product>(product);

            _productRepository.Save(productObject);

        }
    }
}
