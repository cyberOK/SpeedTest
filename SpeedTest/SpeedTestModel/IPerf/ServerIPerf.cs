﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTestModel.IPerf
{
    public class ServerIPerf
    {
        public int Id { get; set; }
        public bool IsCurrent { get; set; }
        public string IPerf3Server { get; set; }
        public string Location { get; set; }
        public string ProviderName { get; set; }
        public int Port { get; set; }
        public string IpAdress { get; set; }
    }
}
