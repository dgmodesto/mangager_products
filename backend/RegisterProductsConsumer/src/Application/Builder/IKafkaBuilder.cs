using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Builder
{
    public interface IKafkaBuilder
    {
        public ConsumerConfig connection();
        public string GetTopicName();
    }
}
