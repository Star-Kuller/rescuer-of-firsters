using Services;
using UnityEngine;

namespace Views
{
    public class CameraController : MonoBehaviour
    {
        private Transform _player;
        private Rigidbody2D _playerRb;
        private readonly float _smoothSpeed = 0.1f;
        private Vector3 _velocity = Vector3.zero;
        private bool _isPlayerRbNotNull;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
            var services = ServiceLocator.Current;
            _player = services.Get<PlayerService>().Player.transform;
            _playerRb = _player.GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            var targetPosition = _player.position;

            targetPosition = new Vector3(targetPosition.x, targetPosition.y, -10);

            transform.position = targetPosition;
        }
    }
}
