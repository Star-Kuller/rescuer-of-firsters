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

            targetPosition += new Vector3(_playerRb.velocity.x, _playerRb.velocity.y, 0);

            // Преобразуем позицию игрока в координаты viewport
            var viewPos =  _camera.WorldToViewportPoint(targetPosition);
            
            Debug.Log(viewPos);
            // Определяем границу "безопасной зоны", внутри которой камера должна двигаться обычно
            var boundary = 0.33f;
            
            
            // Проверяем, не пересекает ли игрок границу
            if(viewPos.x > boundary && viewPos.x < 1 - boundary && viewPos.y > boundary && viewPos.y < 1 - boundary)
            {
                var desiredPosition = targetPosition;
                // Игрок в пределах безопасной зоны, камера следует за игроком как обычно
                desiredPosition.z = transform.position.z;
                transform.position = desiredPosition;
            }
        }
    }
}
