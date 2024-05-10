using Models;
using Services;
using UnityEngine;

namespace Views
{
    public class CameraController : MonoBehaviour
    {
        private Transform _playerTransform;
        private Rigidbody2D _playerRb;
        private PlayerModel _player;
        private bool _isPlayerRbNotNull;
        private Camera _camera;
        private float _startsSize;
        private Vector3 _velocity = Vector3.zero;
        
        [Header("Максимальная дистанция смещения камеры")]
        [SerializeField]
        private float maxDistance;
        
        [Header("Максимальная дистанция отдаления камеры")]
        [SerializeField]
        private float maxSize;
        
        [Header("Скорость плавного перехода")]
        [SerializeField]
        private float smoothSpeed;

        [Header("Время плавного перехода при приземлении на планету")]
        [SerializeField]
        public float smoothTime;

        private void Start()
        {
            _camera = Camera.main;
            _startsSize = _camera.orthographicSize;
            var services = ServiceLocator.Current;
            _playerTransform = services.Get<PlayerService>().Player.transform;
            _player = _playerTransform.GetComponent<PlayerModel>();
            _playerRb = _player.GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            var targetPosition = _playerTransform.position;
            if (_player.IsOnPlanet)
            {
                targetPosition.z = -10;
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
            }
            else
            {
                var velocity = _playerRb.velocity;

                _camera.orthographicSize =
                    _startsSize +
                    (Mathf.Clamp(new Vector3(velocity.x, velocity.y, 0).magnitude / smoothSpeed, 0, 1) *
                     maxSize);

                targetPosition +=
                    new Vector3(velocity.x, velocity.y, 0).normalized *
                    (Mathf.Clamp(new Vector3(velocity.x, velocity.y, 0).magnitude / smoothSpeed,0, 1)
                     * maxDistance);

                targetPosition.z = -10;
                        
                transform.position = targetPosition;
            }
        }
    }
}
