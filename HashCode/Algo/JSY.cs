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
                var bestRide = sim.Rides.FirstOrDefault();
                var bestDistance = long.MaxValue;
                var bestReward = long.MinValue;
                var bestScore = double.MinValue;
                foreach (var ride in sim.Rides)
                {
                    var distance = vehicle.GetDistanceTo(ride.StartPoint);
                    var reward = ride.GetDistance() + (remaining - ride.GetDistance() > 0 ? sim.Bonus : 0);
                    var score = distance * 0.5 + reward * 0.5;
                    if (score <= bestScore && distance + reward <= remaining)
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