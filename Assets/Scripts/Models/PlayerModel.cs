using System;
using Services;
using UnityEngine;

namespace Models
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerModel : MonoBehaviour
    {
        public float Thrust
        {
            get => thrust;
            set => thrust = value;
        }
        public float Fuel
        {
            get => fuel;
            set => fuel = value;
        }
        public float MaxFuel
        {
            get => maxFuel;
            set => maxFuel = value;
        }
        public float JumpForce
        {
            get => jumpForce;
            set => jumpForce = value;
        }
        public bool IsOnPlanet { get; private set; } = false;
        public bool isMoving;
        public GameObject OnPlanet => _planet;
        public float MaxSpeed
        {
            get => maxSpeed;
            set => maxSpeed = value;
        }

        private Camera _camera;
        private Rigidbody2D _rb;
        private Vector2 _contactPoint;
        public GameObject _planet;
        private bool _isGameStarted;
        private EventBus _eventBus;
        public int AnimalCount;
        public String animalData;
        public bool isWin = false;
        
        
        //Инспектор
        [Header("Эти параметры невозможно изменить когда игра запущена", order = 0)]
        [Header("Мощность двигателя")]
        [SerializeField]
        [Range(0, 15)]
        private float thrust;

        [Header("Импульс при старте с планеты")]
        [SerializeField]
        private float jumpForce;
        
        [Header("Топливо")]
        [SerializeField]
        private float fuel;
        
        [Header("Максимальное количество топлива")]
        [SerializeField]
        private float maxFuel;
        
        [Header("Расход топлива в секунду")]
        [SerializeField]
        private float fuelConsumption;
        
        [Tooltip("Максимальная Скорость")]
        [SerializeField]
        private float maxSpeed;
        [Tooltip("Всего животных")]
        [SerializeField]
        public int animalMaxCount;

        private void Start()
        {
            _camera = Camera.main;
            _rb = transform.GetComponent<Rigidbody2D>();
            var serviceLocator = ServiceLocator.Current;
            _eventBus = serviceLocator.Get<EventBus>();
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
            animalData = AnimalCount + "/" + animalMaxCount;
            Debug.Log(animalData);
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
            _eventBus.CallEvent(EventList.StartFromPlanet);
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
                //Fuel = MaxFuel;
                return;
            }

            if (!Input.GetKey(moveForwardKey)){
                isMoving = false;
                return;
                 }
            isMoving = true;
            Fuel -= fuelConsumption * Time.deltaTime;
            _rb.AddForce(transform.up * Thrust);

            if (_isGameStarted) return;
            _eventBus.CallEvent(EventList.GameStart);
            _isGameStarted = true;
        }
        
        private void StayOnPlanet()
        {
            isMoving = false;
            transform.position =
                _planet.transform.TransformPoint(
                    new Vector3(_contactPoint.x, _contactPoint.y, 0));

            var directionToCenter = _planet.transform.position - transform.position;
            transform.up = -directionToCenter.normalized;
            if(AnimalCount >= animalMaxCount && _planet.GetComponent<PlanetModel>().isHomePlanet){
                isWin = true;
            }
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
            
            if (_isGameStarted) return;
            _eventBus.CallEvent(EventList.GameStart);
            _isGameStarted = true;
        }
    }
}
