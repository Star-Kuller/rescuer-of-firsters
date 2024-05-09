using Models;
using UnityEngine;

namespace Services
{
    public class PlanetService : IService
    {
        public GameObject Planet { get; private set; }

        private PlanetModel _model;
        public PlanetService()
        {
            Planet = GameObject.FindWithTag("Planet");
            var services = ServiceLocator.Current;
        }
    }
}