using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmoothCamera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _targetTransform;
        Vector2 size = new Vector2(21, 10);
        void Update()
        {
            var nextPosition = Vector2.Lerp(transform.position, _targetTransform.position, Time.deltaTime/* * ((transform.position - _targetTransform.position).magnitude - size.magnitude / 2)*/);

            if (Mathf.Abs((transform.position - _targetTransform.position).x) > size.x / 3 || Mathf.Abs((transform.position - _targetTransform.position).y) > size.y / 3)
                transform.position = new Vector3(nextPosition.x, nextPosition.y, -10);
        }
    }

}
