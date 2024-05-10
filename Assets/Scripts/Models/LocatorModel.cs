using System;
using System.Collections.Generic;
using System.Linq;
using Services;
using UnityEngine;

namespace Models
{
    public class LocatorModel : MonoBehaviour
    {
        private ParticleSystem _particle;
        private Transform _player;
        private Dictionary<PlanetModel, Transform> _planets;
        private void Start()
        {
            _particle = GetComponent<ParticleSystem>();
            var emission = _particle.emission;
            emission.enabled = false;
        
            var planets = GameObject.FindGameObjectsWithTag("Planet");
            _planets = planets.ToDictionary(
                k => k.GetComponent<PlanetModel>(),
                v => v.transform);
        
            var services = ServiceLocator.Current;
            _player = services.Get<PlayerService>().Player.transform;
        }

        private void FixedUpdate()
        {
            CheckPlanets();
            transform.position = _player.position;
        }
    
        private void CheckPlanets()
        {
            var checkPlanetsKey = KeyCode.E; // по умолчанию используем E

            if (PlayerPrefs.HasKey("checkPlanetsKey"))
            {
                var keyName = PlayerPrefs.GetString("checkPlanetsKey");
                checkPlanetsKey = (KeyCode)Enum.Parse(typeof(KeyCode), keyName);
            }
            
            if (Input.GetKey(checkPlanetsKey))
            {
                EmissionContinue(ClosestPlanet().position - transform.position);
            }
            else
            {
                EmissionStop();
            }
        }

        private void EmissionContinue(Vector2 direction)
        {
            transform.up = direction;
            var emission = _particle.emission;
            emission.enabled = true;
        }
        private void EmissionStop()
        {
            var emission = _particle.emission;
            emission.enabled = false;
        }
    
        private Transform ClosestPlanet()
        {
            Transform closestPlanet = null;
            var distanceToClosestPlanet = float.MaxValue;
            
            foreach (var body in
                     _planets.Where(x => x.Key.aliensAvailability))
            {
                var distanceToBody = (body.Value.position - _player.position).magnitude;
                if (distanceToBody < distanceToClosestPlanet)
                {
                    distanceToClosestPlanet = distanceToBody;
                    closestPlanet = body.Value;
                }
            }
            return closestPlanet;
        }
    }
}
