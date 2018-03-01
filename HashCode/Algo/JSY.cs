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
            foreach (var vehicle in sim.Vehicules.Where(v => v.CurrentRide == null))
            {
                var bestRide = sim.Rides.FirstOrDefault();
                var bestDistance = long.MaxValue;
                var bestReward = long.MinValue;
                foreach (var ride in sim.Rides)
                {
                    var distance = vehicle.GetDistanceTo(ride.StartPoint);
                    var reward = ride.GetDistance() +
                                 (sim.Steps - sim.CurrentStep - ride.GetDistance() > 0 ? sim.Bonus : 0);
                    if (bestDistance <= distance && bestReward <= reward)
                        continue;
                    bestDistance = distance;
                    bestReward = reward;
                    bestRide = ride;
                }

                if (bestRide != null && vehicle.AssignRide(bestRide))
                    sim.Rides.Remove(bestRide);
            }
        }
    }
}