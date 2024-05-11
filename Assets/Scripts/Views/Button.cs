using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;

public class Button : MonoBehaviour
{
    private Camera _camera;
    private PlayerModel _playerModel;
    // Start is called before the first frame update
    [Header("Топливная")]
    [SerializeField]
    public bool isFuel;
    void Start()
    {
        _camera = Camera.main;
        _playerModel = FindObjectOfType<PlayerModel>();
    }

    // Update is called once per frame
    void Update()
    {
        var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        if(transform.position.x - 1 < mousePosition.x && transform.position.x + 1 > mousePosition.x &&
        transform.position.y - 1 < mousePosition.y && transform.position.y + 1 > mousePosition.y && Input.GetMouseButton(0) && _playerModel.IsOnPlanet)
            if(isFuel)
                _playerModel.Fuel = _playerModel.MaxFuel;
    }
}
