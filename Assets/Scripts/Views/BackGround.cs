using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    private Camera _camera;
    private Rigidbody2D _rb;
    private float StartCameraSize;
    private float StartLocalScale;
    private Vector3 StartPos;

    [Header("Время плавного перехода при приземлении на планету")]
    [SerializeField]
    public float smoothTime;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _rb = GetComponent<Rigidbody2D>();
        StartCameraSize = _camera.orthographicSize;
        StartLocalScale = transform.localScale.x;
        StartPos = transform.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.localScale = new Vector3(1, 1, 0) * _camera.orthographicSize / StartCameraSize * StartLocalScale;
        transform.position = _camera.transform.position + StartPos * _camera.orthographicSize / StartCameraSize;
        transform.position += new Vector3(0, 0, StartPos.z + 20);
    }
}
