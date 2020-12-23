using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;

namespace Producer.Builder
{
    public interface IKafkaBuilder
    {
        public ProducerConfig connection();
        public string GetTopicName();
    }
}
