using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DotNet.Highcharts.Samples.Controllers
{
    internal class Keyword {
        public string name { get; set; }
        public int count { get; set; }
    }
    public class NetworkgraphController : Controller
    {
        // GET: Networkgraph
        public ActionResult Index()
        {
            IList<Keyword> dataList = new List<Keyword>();
            LoadDummyDataList(ref dataList);
            IList<object[]> relationDataList = new List<object[]>();
            LoadDummyRelationDataList(ref relationDataList);

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string dataJson = oSerializer.Serialize(dataList);

            Highcharts chart = new Highcharts("chart");
            chart.InitChart(new Chart()
            {
                Type = Enums.ChartTypes.Networkgraph,
                MarginTop = 80,
                Events = new ChartEvents {
                    Load = @"
                            Highcharts.addEvent(Highcharts.seriesTypes.networkgraph, 'afterSetOptions', 
                            function (e) {
                                var dataList = " + dataJson + @"; 
                                var nodes = {};
                                dataList.forEach(function(data){
        	                        nodes[data.name] = {
                                        id: data.name,
                                        marker: {
                                            radius: data.count*0.7
                                        }
                                    };
                                });
                                e.options.nodes = Object.keys(nodes).map(function (id) {
                                    return nodes[id];
                                });
                            })"
                }
            })
                .SetTitle(new Title { Text = "Keywords Relationship"})
                .SetSubtitle(new Subtitle { Text = "Network Graph in .NET Highcharts"})
                .SetPlotOptions(new PlotOptions {
                    Networkgraph = new PlotOptionsNetworkgraph {
                        Keys = new string[3]{ "from", "to", "link_number" },
                        LayoutAlgorithm = new PlotOptionsNetworkgraphLayoutAlgorithm {
                            EnableSimulation = true,
                            Integration = "verlet",
                            LinkLength = 100
                        }
                    }
                })
                .SetSeries(new SeriesNetworkgraph
                {
                    Marker = new SeriesNetworkgraphMarker
                    {
                        Radius = 7,
                        FillColor = "yellow"
                    },
                    DataLabels = new SeriesNetworkgraphDataLabels
                    {
                        Enabled = true,
                        TextPath = new TextPath {
                            Enabled = false
                        },
                        LinkFormat = "{point.link_number}",
                        Format = "{point.name}",
                        AllowOverlap = false
                    },
                    Data = new Data(relationDataList.ToArray())
                })
                .SetTooltip(new Tooltip {
                    Enabled = true,
                    UseHTML = true,
                    Formatter = @"
                                function() {
                                    var dataList = " + dataJson + @"; 
                                    let table = '<table>';
                                    let row = ''
                                    + '<tr>'
                                    + '<td>Keyword </td>'
                                    + '<td>'+dataList[this.point.index].name+'</td>'
                                    +'</tr>'
                                    + '<tr>'
                                    + '<td>Count </td>'
                                    + '<td>'+dataList[this.point.index].count+'</td>'
                                    +'</tr>'
                                    +'';
                                    table += row;
                                    table += '</table>';
                                    return table;
                                }"
                });

            return View(chart);
        }

        private void LoadDummyDataList(ref IList<Keyword> dataList) {
            dataList.Add(new Keyword { name = "Order", count = 25 });
            dataList.Add(new Keyword { name = "Burger", count = 17 });
            dataList.Add(new Keyword { name = "Good", count = 15 });
            dataList.Add(new Keyword { name = "Food", count = 14 });
            dataList.Add(new Keyword { name = "Drive", count = 10 });
            dataList.Add(new Keyword { name = "Time", count = 18 });
            dataList.Add(new Keyword { name = "Service", count = 11 });
            dataList.Add(new Keyword { name = "Wait", count = 12 });
        }

        private void LoadDummyRelationDataList(ref IList<object[]> relationDataList) {
            relationDataList.Add(new object[] { "Order", "Burger", 100 });
            relationDataList.Add(new object[] { "Order", "Good", 100 });
            relationDataList.Add(new object[] { "Order", "Food", 100 });
            relationDataList.Add(new object[] { "Order", "Drive", 100 });
            relationDataList.Add(new object[] { "Order", "Time", 100 });
            relationDataList.Add(new object[] { "Food", "Service", 100 });
            relationDataList.Add(new object[] { "Food", "Burger", 100 });
            relationDataList.Add(new object[] { "Drive", "Wait", 100 });
            relationDataList.Add(new object[] { "Time", "Wait", 100 });
            relationDataList.Add(new object[] { "Wait", "Food", 100 });
            relationDataList.Add(new object[] { "Good", "Service", 100 });
            relationDataList.Add(new object[] { "Good", "Drive", 100 });
        }
    }

}