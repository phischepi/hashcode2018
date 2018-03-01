using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCode.Model
{
    class Vehicule
    {
        private IList<Ride> Rides { get; set; }

        public override string ToString()
        {
            return $"{Rides.Count} {string.Join(" ", Rides.OrderBy(r => r.Order).Select(r => r.Id))}";
        }
    }
}
