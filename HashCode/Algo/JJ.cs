using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using HashCode.Model;

namespace HashCode.Algo
{
    internal class JJ : IAlgo
    {
        private SortedList<long, List<long>> _sortedsRides;
        private Simulation _sim;
        public void Execute(Simulation sim)
        {
            _sim = sim;
            ComputeScore();
            var idx = 0;
            foreach (var bRides in _sortedsRides)
            {
                foreach (var ride in bRides.Value)
                {
                    _sim.Vehicules[idx].AddRide(_sim.Rides[(int) ride]);
                    idx = (idx+1)% _sim.Vehicules.Count;
                }
            }
        }

        public void ComputeScore()
        {
            _sortedsRides = new SortedList<long, List<long>>();
            foreach (var ride in _sim.Rides)
            {
                var score = ride.GetDistance();
                List<long> res;
                if (!_sortedsRides.TryGetValue(score, out res))
                {
                    res = new List<long>();
                    _sortedsRides.Add(score,res);
                }
                res.Add(ride.Id);
            }
        }
    }
}