using System.Collections.Generic;

namespace HashCode.Model
{
    internal class Simulation
    {
        public long CurrentStep { get; } = 0;
        public long Rows { get; set; }
        public long Columns { get; set; }
        public long Bonus { get; set; }
        public IList<Ride> Rides { get; set; }
        public IList<Vehicule> Vehicules { get; set; }
        public long Steps { get; set; }

        public void ComputeNextStep()
        {
            foreach (var vehicule in Vehicules)
                vehicule.ComputeNextPosition();
        }
    }
}