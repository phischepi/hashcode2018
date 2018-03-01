using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using HashCode.Model;

namespace HashCode.Algo
{
    internal class JJ : IAlgo
    {
        private SortedList<long, List<Ride>> _sortedsRides;
        private Simulation _sim;
        public void Execute(Simulation sim)
        {
            _sim = sim;
            ComputeScore();
            var idx = 0;
            foreach (var bRides in _sortedsRides.Reverse())
            {
                foreach (var ride in bRides.Value)
                {
                    _sim.Vehicules[idx].AddRide(ride);
                    idx = (idx+1)% _sim.Vehicules.Count;
                }
            }
        }

        public void ComputeScore()
        {
            _sortedsRides = new SortedList<long, List<Ride>>();
            foreach (var ride in _sim.Rides)
            {
                var score = ride.GetDistance();
                List<Ride> res;
                if (!_sortedsRides.TryGetValue(score, out res))
                {
                    res = new List<Ride>();
                    _sortedsRides.Add(score,res);
                }
                res.Add(ride);
            }
        }
    }
}