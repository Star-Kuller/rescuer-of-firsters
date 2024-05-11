using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;

public class FuelTrace : MonoBehaviour
{
    private PlayerModel _playerModel;
    Vector3 StartPos;
    Vector3 SpaceshipStartPos;
    // Start is called before the first frame update
    void Start()
    {
        _playerModel = FindObjectOfType<PlayerModel>();
        StartPos = transform.position;
        SpaceshipStartPos = _playerModel.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerModel.isMoving)
            transform.position = _playerModel.transform.position /*+ new Vector3(0, 0, 0)*/;
        else
            transform.position = _playerModel.transform.position + new Vector3(0, 0, StartPos.z - 20);

    }
}
