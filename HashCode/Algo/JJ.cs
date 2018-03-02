using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Xml.Serialization;
using HashCode.Model;

namespace HashCode.Algo
{
    internal class JJ : IAlgo
    {
        private SortedList<long, List<Ride>> _sortedsRides;
        private Simulation _sim;
        private int[] _vhcSteps;
        private bool[] _rAssigned; //False not assigned
        private bool[][] _vhcScore; //False is interessant for optimising

        public void Execute(Simulation sim)
        {
            _sim = sim;
            _vhcSteps = new int[sim.Vehicules.Count];
            _rAssigned = new bool[sim.Rides.Count];
            for (var i = 0; i < sim.Steps; i++)
            {
                for (int j = 0; j < sim.Vehicules.Count; j++)
                {
                    if (_vhcSteps[j] <= 0)
                    {
                        var vhc = sim.Vehicules[j];
                        //Need to compute and fine a target
                        var ride = ComputeScoreForVhc(vhc, i);
                        if (ride != null)
                        {
                            AddRide(ride, vhc);
                        }
                    }

                }
            }
        }

        private void AddRide(Ride ride, Vehicule vhc)
        {
            _rAssigned[ride.Order] = true;
            vhc.AddRide(ride);
        }
        
        private Ride ComputeScoreForVhc(Vehicule vhc, int cStep)
        {
            long maxScore = 0;
            Ride ret = null;
            
            for (int i = 0; i < _sim.Rides.Count; i++)
            {
                if (!_rAssigned[i])
                {
                    var ride = _sim.Rides[i];
                    var score =vhc.GetDistanceTo(ride.StartPoint);
                    if (score > maxScore)
                    {
                        ret = ride;
                        maxScore = score;
                    }
                }
            }

            return ret;
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