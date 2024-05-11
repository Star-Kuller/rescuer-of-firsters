using System.Collections.Generic;
using System.Linq;
using Models;
using UnityEngine;

namespace Services
{
    public class AnimalService : IService
    {
        public int AnimalsTotal { get; }
        public int AnimalsInShip { get; private set; }
        public int AnimalsOnHomePlanet { get; private set; }

        private readonly EventBus _eventBus;
        private readonly GameObject _player;
        public AnimalService()
        {
            var services = ServiceLocator.Current;
            _eventBus = services.Get<EventBus>();
            _player = services.Get<PlayerService>().Player;
            _eventBus.Subscribe(EventList.LandedOnPlanet, CheckPlanet);
            _eventBus.Subscribe(EventList.LandedOnHomePlanet, RealiseAnimals);
            
            var planets = GameObject.FindGameObjectsWithTag("Planet");
            AnimalsTotal = planets.Count(x => x.GetComponent<PlanetModel>().animalAvailability);
        }

        private void CheckPlanet()
        {
            var planet = _player.GetComponent<PlayerModel>().OnPlanet.GetComponent<PlanetModel>();
            if (!planet.animalAvailability) return;
            
            planet.animalAvailability = false;
            AnimalsInShip += 1;
            _eventBus.CallEvent(EventList.PickUpAnimal);
        }

        private void RealiseAnimals()
        {
            AnimalsOnHomePlanet += AnimalsInShip;
            AnimalsInShip = 0;
            if(AnimalsOnHomePlanet >= AnimalsTotal)
                _eventBus.CallEvent(EventList.Victory);
            Debug.Log($"Возвращено: {AnimalsOnHomePlanet}/{AnimalsTotal}");
        }
    }
}