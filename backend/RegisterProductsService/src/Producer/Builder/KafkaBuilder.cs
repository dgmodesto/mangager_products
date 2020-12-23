using Confluent.Kafka;
using Producer.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Producer.Builder
{
    public class KafkaBuilder : IKafkaBuilder
    {
        private readonly KafkaSettings _kafkaSettings;

        public KafkaBuilder(KafkaSettings kafkaSettings)
        {
            _kafkaSettings = kafkaSettings;
        }

        public ProducerConfig connection()
        {
            return new ProducerConfig
            {
                BootstrapServers = _kafkaSettings.BootstrapServer,
            };
        }

        public string GetTopicName()
        {
            return _kafkaSettings.Topic;
        }
    }
}
