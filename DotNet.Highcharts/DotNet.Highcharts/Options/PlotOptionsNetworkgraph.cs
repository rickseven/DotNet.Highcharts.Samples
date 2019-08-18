using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Highcharts.Options
{
    public class PlotOptionsNetworkgraph
    {
        public string[] Keys { get; set; }
        public PlotOptionsNetworkgraphLayoutAlgorithm LayoutAlgorithm { get; set; }
    }

    public class PlotOptionsNetworkgraphLayoutAlgorithm
    {
        public bool EnableSimulation { get; set; }
        public string Integration { get; set; }
        public int LinkLength { get; set; }
    }
}
