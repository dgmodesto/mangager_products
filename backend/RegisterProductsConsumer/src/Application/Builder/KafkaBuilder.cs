using Application.Settings;
using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Builder
{
    public class KafkaBuilder : IKafkaBuilder
    {
        private readonly KafkaSettings _kafkaSettings;

        public KafkaBuilder(KafkaSettings kafkaSettings)
        {
            _kafkaSettings = kafkaSettings;
        }

        public ConsumerConfig connection()
        {
            return new ConsumerConfig
            {
                GroupId = _kafkaSettings.GroupId,
                BootstrapServers = _kafkaSettings.BootstrapServer,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
        }

        public string GetTopicName()
        {
            return _kafkaSettings.Topic;
        }
    }
}
