using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public class PlanetModel : MonoBehaviour
    {
        private readonly HashSet<Rigidbody2D> _affectedBodies = new HashSet<Rigidbody2D>();
        private readonly HashSet<GameObject> _collisionBodies = new HashSet<GameObject>();

        [Header("Масса планеты")]
        [SerializeField]
        private float mass;
        
        [Header("Скорость вращения планеты")]
        [SerializeField]
        private float rotationSpeed;

        [Header("Наличие потеряного животного")]
        [SerializeField]
        public bool aliensAvailability;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.attachedRigidbody != null)
            {
                _affectedBodies.Add(other.attachedRigidbody);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            _collisionBodies.Add(other.gameObject);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.attachedRigidbody != null)
            {
                _affectedBodies.Remove(other.attachedRigidbody);
            }
        }
        
        private void OnCollisionExit2D(Collision2D other)
        {
            _collisionBodies.Remove(other.gameObject);
        }
        
        private void FixedUpdate()
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            foreach (var body in _affectedBodies)
            {
                if(_collisionBodies.Contains(body.gameObject)) continue;
                
                var position = new Vector2(transform.position.x, transform.position.y);
                var directionToPlanet = (position - body.position).normalized;

                var strength = CalculateStrength(body, position);
                body.AddForce(directionToPlanet * strength);
            }
        }

        private float CalculateStrength(Rigidbody2D body, Vector2 position)
        {
            var distanceSqr = (position - body.position).sqrMagnitude; 
            return 10 * (mass * body.mass) / distanceSqr;
        }
    }
}
