using System;
using System.Collections.Generic;
using System.Linq;
using OxyPlot;

namespace Benchmark.GUI
{
    public class Result
    {
        public string Db { get; set; }
        public double Time { get; set; }
        public List<long> Delays { get; set; }

        public List<DataPoint> Points
        {
            get
            {
                var i = 0;
                var points = new List<DataPoint>();
                foreach (var delay in Delays)
                {
                    points.Add(new DataPoint(100 * i, delay));
                    i++;
                }
                return points;
            }
            set
            {
                Points = value;
            }
        }

        public long DelaysAvg { get { return (long) Delays.Average(x => x); }}
    }
}