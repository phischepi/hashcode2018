using System;
using System.Linq;
using HashCode.Model;

namespace HashCode.Algo
{
    internal class JSY : IAlgo
    {
        public void Execute(Simulation sim)
        {
            var startPoint = new Vehicule();
            foreach (var simToRemove in sim.Rides.Where(r => startPoint.GetDistanceTo(r.StartPoint) + r.GetDistance() > sim.Steps))
                sim.Rides.Remove(simToRemove);

            for (; sim.CurrentStep < sim.Steps; sim.CurrentStep++)
            {
                AssignRide(sim);
                foreach (var vehicule in sim.Vehicules)
                    vehicule.ComputeNextPosition();
            }
        }

        private static void AssignRide(Simulation sim)
        {
            var remaining = sim.Steps - sim.CurrentStep;
            foreach (var vehicle in sim.Vehicules.Where(v => v.CurrentRide == null))
            {
                var bestRide = sim.Rides
                    .OrderByDescending(r => r.GetDistance() - vehicle.GetDistanceTo(r.StartPoint)
                                  + (r.GetDistance() - vehicle.GetDistanceTo(r.StartPoint) < r.LatestFinish
                                      ? sim.Bonus
                                      : 0))
                    .FirstOrDefault(r => vehicle.GetDistanceTo(r.StartPoint) + r.GetDistance() <= remaining);

                var bestReward = bestRide != null
                    ? bestRide.GetDistance() - vehicle.GetDistanceTo(bestRide.StartPoint)
                      + (bestRide.GetDistance() - vehicle.GetDistanceTo(bestRide.StartPoint) < bestRide.EarlyStart
                          ? sim.Bonus
                          : 0) : long.MinValue;

                foreach (var ride in sim.Rides.Where(r => vehicle.GetDistanceTo(r.StartPoint) + r.GetDistance() <= remaining))
                {
                    var reward = ride.GetDistance() - vehicle.GetDistanceTo(ride.StartPoint)
                                 + (ride.GetDistance() - vehicle.GetDistanceTo(ride.StartPoint) < ride.LatestFinish
                                     ? sim.Bonus
                                     : 0);
                    if (bestReward >= reward)
                        continue;
                    bestReward = reward;
                    bestRide = ride;
                }

                if (bestRide != null && vehicle.AssignRide(bestRide))
                    sim.Rides.Remove(bestRide);
            }
        }
    }
}