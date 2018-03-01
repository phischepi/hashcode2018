using System;
using System.Collections.Generic;
using System.Linq;

namespace HashCode.Model
{
    internal class Vehicule
    {
        public Vehicule()
        {
            CurrentPosition = new Tuple<long, long>(0, 0);
        }

        public IList<Ride> Rides { get; set; } = new List<Ride>();
        public Ride CurrentRide { get; set; }

        public Tuple<long, long> CurrentPosition { get; set; }
        public Tuple<long, long> TargetPosition { get; set; }

        public override string ToString()
        {
            return $"{Rides.Count} {string.Join(" ", Rides.OrderBy(r => r.Order).Select(r => r.Order))}";
        }

        public long GetDistanceTo(Tuple<long, long> endPoint)
        {
            return Math.Abs(CurrentPosition.Item1 - endPoint.Item1) + Math.Abs(CurrentPosition.Item2 - endPoint.Item2);
        }

        public long GetDistanceToTarget()
        {
            return Math.Abs(CurrentPosition.Item1 - TargetPosition.Item1) +
                   Math.Abs(CurrentPosition.Item2 - TargetPosition.Item2);
        }

        public bool AssignRide(Ride ride, bool eraseRide = false)
        {
            //todo: handle changing ride in midlle of another ride
            // => move to start and then to end
            if (CurrentRide != null)
                return false;
            CurrentRide = ride;
            return true;
        }

        public void AddRide(Ride ride)
        {
            Rides.Add(ride);
            CurrentPosition = ride.EndPoint;
        }

        public void ComputeNextPosition()
        {
            if (TargetPosition == null)
                return;
            if (Equals(TargetPosition, CurrentPosition) && CurrentRide != null)
            {
                Rides.Add(CurrentRide);
                CurrentRide = null;
            }

            var item1 = CurrentPosition.Item1;
            var item2 = CurrentPosition.Item2;
            if (Math.Abs(TargetPosition.Item1 - CurrentPosition.Item1) > 0)
                item1 = TargetPosition.Item1 > CurrentPosition.Item1 ? -1 : 1;
            else if (Math.Abs(TargetPosition.Item2 - CurrentPosition.Item2) > 0)
                item2 = TargetPosition.Item2 > CurrentPosition.Item2 ? -1 : 1;

            CurrentPosition = new Tuple<long, long>(item1, item2);
        }
    }
}