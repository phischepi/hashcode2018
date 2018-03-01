using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using HashCode.Model;

namespace HashCode.Algo
{
    internal class SB : IAlgo
    {

        public void Execute(Simulation sim)
        {
            PointExecute(sim);
        }

        public void PointExecute(Simulation sim)
        {
            foreach (var v in sim.Vehicules)
            {
                var step = 0L;
                foreach (var ride in sim.Rides.ToArray())
                {
                    var point = 0L;
                    Ride minRide = null;
                    var distance = v.GetDistanceTo(ride.StartPoint);
                    if (distance + step + ride.GetDistance() <= ride.LatestFinish)
                    {
                        var timePoint = (distance + step - ride.EarlyStart) * 1000;
                        var distancePoint = distance + ride.GetDistance() * 50;

                        if (point > timePoint + distancePoint)
                        {
                            minRide = ride;
                            point = timePoint + distancePoint;

      
                        }
                    }
                    if (minRide != null)
                    {
                        v.AddRide(minRide);
                        step = Math.Max(step + distance, minRide.EarlyStart) + minRide.GetDistance();
                        v.CurrentPosition = minRide.EndPoint;
                        sim.Rides.Remove(minRide);
                    }
                }

            }
        }

        public void MixteExecute(Simulation sim)
        {

            var ridesBySteps = sim.Rides.GroupBy(r => r.LatestFinish).OrderBy(r => r.Key).Select(t => new Tuple<long, List<Ride>>(t.Key, t.ToList())).ToArray();


            foreach (var v in sim.Vehicules)
            {
                var step = 0L;


                foreach (var rideByStep in ridesBySteps)
                {
                    if (rideByStep.Item1 > step)
                    {
                        var minDistance = 0L;
                        var earlyDistance = 0L;
                        Ride minRide = null;

                        foreach (var ride in rideByStep.Item2)
                        {
                            var distance = v.GetDistanceTo(ride.StartPoint);
                            if ((distance + step + ride.GetDistance()) <= rideByStep.Item1 && (minDistance >= distance || earlyDistance < ride.EarlyStart || minRide == null))
                            {
                                    minRide = ride;
                                    minDistance = distance;
                                    earlyDistance = ride.EarlyStart;

                            }
                        }

                        if (minRide != null)
                        {
                            v.AddRide(minRide);
                            step = Math.Max(step + minDistance, minRide.EarlyStart) + minRide.GetDistance();
                            v.CurrentPosition = minRide.EndPoint;
                            rideByStep.Item2.Remove(minRide);

                        }
                    }
                }
            }
        }


        public void OptimizeLatestFinishExecute(Simulation sim)
        {

            var ridesBySteps = sim.Rides.GroupBy(r => r.LatestFinish).OrderBy(r => r.Key).Select(t => new Tuple<long, List<Ride>>(t.Key, t.ToList())).ToArray();


            foreach (var v in sim.Vehicules)
            {
                var step = 0L;


                foreach (var rideByStep in ridesBySteps)
                {
                    if (rideByStep.Item1 > step)
                    {
                        var minDistance = 0L;
                        Ride minRide = null;

                        foreach (var ride in rideByStep.Item2)
                        {
                            var distance = v.GetDistanceTo(ride.StartPoint);
                            if ((distance + step + ride.GetDistance()) <= rideByStep.Item1 && (minDistance > distance || minRide == null))
                            {
                                minRide = ride;
                                minDistance = distance;
                            }
                        }

                        if (minRide != null)
                        {
                            v.AddRide(minRide);
                            step = Math.Max(step + minDistance, minRide.EarlyStart) + minRide.GetDistance();
                            v.CurrentPosition = minRide.EndPoint;
                            rideByStep.Item2.Remove(minRide);

                        }
                    }
                }
            }
        }

        public void OptimizeEarlyStartExecute(Simulation sim)
        {
            var ridesBySteps = sim.Rides.GroupBy(r => r.EarlyStart).OrderBy(r => r.Key).Select(t => new Tuple<long, List<Ride>>(t.Key, t.ToList())).ToArray();


            foreach (var v in sim.Vehicules)
            {
                var step = 0L;


                foreach (var rideByStep in ridesBySteps)
                {
                    if (rideByStep.Item1 > step)
                    {
                        var minDistance = 0L;
                        Ride minRide = null;

                        foreach (var ride in rideByStep.Item2)
                        {
                            var distance = v.GetDistanceTo(ride.StartPoint);
                            if (distance + step <= rideByStep.Item1 && (minDistance > distance || minRide == null))
                            {
                                minRide = ride;
                                minDistance = distance;
                            }
                        }

                        if (minRide != null)
                        {
                            v.AddRide(minRide);
                            step = Math.Max(step + minDistance, minRide.EarlyStart) + minRide.GetDistance();
                            v.CurrentPosition = minRide.EndPoint;
                            rideByStep.Item2.Remove(minRide);

                        }
                    }
                }
            }
        }
    }
}