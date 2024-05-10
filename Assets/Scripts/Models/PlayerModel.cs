using System;
using Services;
using UnityEngine;

namespace Models
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerModel : MonoBehaviour
    {
        public float Thrust { get; set; }
        public float Fuel { get; set; }
        public float MaxFuel { get; set; }
        public float JumpForce { get; set; }
        public bool IsOnPlanet { get; private set; } = false;
        public float MaxSpeed { get; set; }

        private Camera _camera;
        private Rigidbody2D _rb;
        private Vector2 _contactPoint;
        private GameObject _planet;

        //Инспектор
        [Header("Эти параметры невозможно изменить когда игра запущена", order = 0)]
        [Header("Мощность двигателя")]
        [SerializeField]
        [Range(0, 15)]
        private float startThrust;
        
        [Header("Импульс при старте с планеты")]
        [SerializeField]
        private float jumpForce;
        
        [Space]
        [Header("Начальное топливо")]
        [SerializeField]
        private float startFuel;

        [Header("Расход топлива в секунду")]
        [SerializeField]
        private float fuelConsumption;
        [Tooltip("Максимальная скорость")]
        [SerializeField]
        private float maxSpeed;

        private void Start()
        {
            _camera = Camera.main;
            _rb = transform.GetComponent<Rigidbody2D>();
            Fuel = startFuel;
            MaxFuel = startFuel;
            Thrust = startThrust;
            JumpForce = jumpForce;
            MaxSpeed = maxSpeed;
        }
        
        private void FixedUpdate()
        {
            if (IsOnPlanet)
            {
                StayOnPlanet();
                Jump();
            }
            else
            {
                Rotate();
                MoveForward();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Planet")) return; 
            if(IsOnPlanet) return;
            IsOnPlanet = true;
            var contact = other.GetContact(0);
            _planet = other.gameObject;
            _contactPoint = _planet.transform.InverseTransformPoint(contact.point);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Planet")) return; 
            IsOnPlanet = false;
        }

        private void Rotate()
        {
            var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            var direction = new Vector2(
                mousePosition.x - transform.position.x,
                mousePosition.y - transform.position.y
            );

            transform.up = direction;
        }
        
        private void MoveForward()
        {
            var moveForwardKey = KeyCode.W; // по умолчанию используем W
    
            if (PlayerPrefs.HasKey("MoveForwardKey"))
            {
                var keyName = PlayerPrefs.GetString("MoveForwardKey");
                moveForwardKey = (KeyCode)Enum.Parse(typeof(KeyCode), keyName);
            }
            if (_rb.velocity.magnitude > MaxSpeed)
                _rb.velocity = _rb.velocity.normalized * MaxSpeed;
            if (Fuel <= 0)
            {
                Fuel = 0;
                Fuel = MaxFuel;
                return;
            }
            
            if (!Input.GetKey(moveForwardKey)) return;
            Fuel -= fuelConsumption * Time.deltaTime;
            _rb.AddForce(transform.up * Thrust);
        }

        private void StayOnPlanet()
        {
            transform.position =
                _planet.transform.TransformPoint(
                    new Vector3(_contactPoint.x, _contactPoint.y, 0));
            
            var directionToCenter =  _planet.transform.position - transform.position;
            transform.up = -directionToCenter.normalized;
        }

        private void Jump()
        {
            var jumpKey = KeyCode.Space;
            if (PlayerPrefs.HasKey("jumpKey"))
            {
                var keyName = PlayerPrefs.GetString("jumpKey");
                jumpKey = (KeyCode)Enum.Parse(typeof(KeyCode), keyName);
            }

            if (!Input.GetKey(jumpKey)) return;
            _rb.AddForce(transform.up * JumpForce);
            IsOnPlanet = false;
        }
    }
}
