using System;

namespace HashCode.Model
{
    internal class Ride
    {
        public Ride(long order)
        {
            Order = order;
        }

        public long Id { get; set; }
        public long Order { get; set; }
        public Tuple<long, long> StartPoint { get; set; }
        public Tuple<long, long> EndPoint { get; set; }
        public long EarlyStart { get; set; }
        public long LatestFinish { get; set; }

        public long GetDistance()
        {
            return Math.Abs(StartPoint.Item1 - EndPoint.Item1) + Math.Abs(StartPoint.Item2 - EndPoint.Item2);
        }
    }
}