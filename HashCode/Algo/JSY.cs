using System.Linq;
using HashCode.Model;

namespace HashCode.Algo
{
    internal class JSY : IAlgo
    {
        public void Execute(Simulation sim)
        {
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
                Ride bestRide = null;
                var bestDistance = long.MaxValue;
                foreach (var ride in sim.Rides)
                {
                    var distance = vehicle.GetDistanceTo(ride.StartPoint);
                    if (bestDistance <= distance)
                        continue;
                    bestDistance = distance;
                    bestRide = ride;
                }

                if(bestRide != null && vehicle.AssignRide(bestRide))
                    sim.Rides.Remove(bestRide);
            }
        }
    }
}