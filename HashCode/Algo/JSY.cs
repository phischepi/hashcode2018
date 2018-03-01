using System.Linq;
using HashCode.Model;

namespace HashCode.Algo
{
    internal class JSY : IAlgo
    {
        public void Execute(Simulation sim)
        {
            foreach (var simToRemove in sim.Rides.Where(r => r.GetDistance() > sim.Steps))
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
                    .OrderBy(r => r.GetDistance() - vehicle.GetDistanceTo(r.StartPoint)
                                  + (r.GetDistance() - vehicle.GetDistanceTo(r.StartPoint) < r.EarlyStart
                                      ? sim.Bonus
                                      : 0))
                    .FirstOrDefault(r => vehicle.GetDistanceTo(r.StartPoint) + r.GetDistance() <= remaining);
                var bestDistance = bestRide != null ? vehicle.GetDistanceTo(bestRide.StartPoint) : long.MaxValue;
                var bestReward = bestRide != null
                    ? bestRide.GetDistance() - vehicle.GetDistanceTo(bestRide.StartPoint)
                      + (bestRide.GetDistance() - vehicle.GetDistanceTo(bestRide.StartPoint) < bestRide.EarlyStart
                          ? sim.Bonus
                          : 0) : long.MinValue;
                var bestScore = bestDistance * 0.5 + bestReward * 0.5;
                foreach (var ride in sim.Rides)
                {
                    var distance = vehicle.GetDistanceTo(ride.StartPoint);
                    var reward = ride.GetDistance() - vehicle.GetDistanceTo(ride.StartPoint)
                                 + (ride.GetDistance() - vehicle.GetDistanceTo(bestRide.StartPoint) < ride.EarlyStart
                                     ? sim.Bonus
                                     : 0);
                    var score = distance * 0.5 + reward * 0.5;
                    if (score <= bestScore)
                        continue;
                    bestDistance = distance;
                    bestReward = reward;
                    bestScore = score;
                    bestRide = ride;
                }

                if (bestRide != null && vehicle.AssignRide(bestRide))
                    sim.Rides.Remove(bestRide);
            }
        }
    }
}