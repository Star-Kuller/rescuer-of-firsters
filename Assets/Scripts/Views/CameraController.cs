using Services;
using UnityEngine;

namespace Views
{
    public class CameraController : MonoBehaviour
    {
        private Transform _player;
        private Rigidbody2D _playerRb;
        private bool _isPlayerRbNotNull;
        
        [Header("Максимальная дистанция отлёта камеры")]
        [SerializeField]
        private float maxDistance;
        
        [Header("Смягчение движения камеры")]
        [SerializeField]
        private float Smoosh;

        private void Start()
        {
            var services = ServiceLocator.Current;
            _player = services.Get<PlayerService>().Player.transform;
            _playerRb = _player.GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            var targetPosition = _player.position;

            targetPosition += new Vector3(targetPosition.x, targetPosition.y, -10);

            transform.position = _player.position;
            transform.position += new Vector3(0, 0, -10);
        }
    }
}
