using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Highcharts.Options
{
    public class SeriesNetworkgraphDataLabels
    {
        public bool Enabled { get; set; }
        public TextPath TextPath { get; set; }
        public string LinkFormat { get; set; }
        public string Format { get; set; }
        public bool AllowOverlap { get; set; }
    }
}
