using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Settings
{
    public class KafkaSettings
    {
        public string GroupId { get; set; }
        public string BootstrapServer { get; set; }
        public string Topic { get; set; }
    }
}
