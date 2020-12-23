using Confluent.Kafka;
using Domain.Models;
using Producer.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Producer.Services.Products
{
    public class ConsumerService : IConsumerService
    {
        private readonly IKafkaBuilder _kafkaBuilder;

        public ConsumerService(IKafkaBuilder kafkaBuilder)
        {
            _kafkaBuilder = kafkaBuilder;
        }

        public List<Product> GetProducts()
        {
            throw new NotImplementedException();
        }

        public async Task SendEventMessageAsync(Product product)
        {

            var builderConf = _kafkaBuilder.connection();
            Console.WriteLine("Connection with Kafka realized successfully.");

            // If serializers are not specified, default serializers from
            // `Confluent.Kafka.Serializers` will be automatically used where
            // available. Note: by default strings are encoded as UTF8.
            using (var producer = new ProducerBuilder<Null, string>(builderConf).Build())
            {
                try
                {
                    string productJsonString = JsonSerializer.Serialize(product);

                    var dr = await producer.ProduceAsync(_kafkaBuilder.GetTopicName(),
                        new Message<Null, string> { Value = $"{ productJsonString }" });

                    Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset} | { productJsonString }'");

                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }
        }
    }
}
