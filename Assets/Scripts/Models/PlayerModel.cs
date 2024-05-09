using Services;
using UnityEngine;
using UnityEngine.Serialization;

namespace Models
{
    public class PlayerModel : MonoBehaviour, IService
    {
        public float Thrust { get; set; }
        public float Fuel { get; set; }

        public float MaxFuel { get; set; }
        
        private Camera _camera;
        private Rigidbody2D _rb;

        //Инспектор
        [Tooltip("Мощность двигателя")]
        [SerializeField]
        private float startThrust;
        
        [Tooltip("Начальное топливо")]
        [SerializeField]
        private float startFuel;
        
        [Tooltip("Расход топлива в секунду")]
        [SerializeField]
        private float fuelConsumption;

        private void Start()
        {
            _camera = Camera.main;
            _rb = transform.GetComponent<Rigidbody2D>();
            Fuel = startFuel;
            MaxFuel = startFuel;
            Thrust = startThrust;
            var services = ServiceLocator.Current;
        }
        
        private void FixedUpdate()
        {
            Rotate();
            MoveForward();
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

        // ReSharper disable Unity.PerformanceAnalysis
        private void MoveForward()
        {
            var moveForwardKey = KeyCode.W; // по умолчанию используем W
    
            if (PlayerPrefs.HasKey("MoveForwardKey"))
            {
                var keyName = PlayerPrefs.GetString("MoveForwardKey");
                moveForwardKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyName);
            }

            if (Fuel <= 0)
            {
                Fuel = 0;
                return;
            }
            
            if (!Input.GetKey(moveForwardKey)) return;
            Debug.Log(Fuel);
            Fuel -= fuelConsumption * Time.deltaTime;
            _rb.AddForce(transform.up * Thrust);
        }
    }
}
