using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public class PlanetModel : MonoBehaviour
    {
        private readonly HashSet<Rigidbody2D> _affectedBodies = new HashSet<Rigidbody2D>();
        
        [Tooltip("Масса планеты")]
        [SerializeField]
        private float mass;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.attachedRigidbody != null)
            {
                _affectedBodies.Add(other.attachedRigidbody);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.attachedRigidbody != null)
            {
                _affectedBodies.Remove(other.attachedRigidbody);
            }
        }
        
        private void FixedUpdate()
        {
            foreach (var body in _affectedBodies)
            {
                var position = new Vector2(transform.position.x, transform.position.y);
                var directionToPlanet = (position - body.position).normalized;
                
                var distanceSqr = (position - body.position).sqrMagnitude; 
                var strength = 10 * mass * body.mass / distanceSqr;
                body.AddForce(directionToPlanet * strength);
            }
        }
    }
}
