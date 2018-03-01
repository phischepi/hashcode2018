using System.Collections.Generic;

namespace HashCode.Model
{
    internal class Simulation
    {
        public long CurrentStep { get; } = 0;
        public long Rows { get; set; }
        public long Columns { get; set; }
        public long Bonus { get; set; }
        public IList<Ride> Rides { get; set; } = new List<Ride>();
        public IList<Vehicule> Vehicules { get; set; } = new List<Vehicule>();
        public long Steps { get; set; }
    }
}