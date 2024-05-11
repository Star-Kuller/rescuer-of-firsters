using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    private Camera _camera;
    public Sprite[] _spiteArray;
    private SpriteRenderer _spriteRender;
    private Rigidbody2D _rb;
    private PlayerModel _playerModel;
    private float StartCameraSize;
    private float StartLocalScale;
    public Vector3 StartPos;
    //private NewBehaviourScript newBehaviourScript;

    [Header("Полоска топлива")]
    [SerializeField]
    public bool isFuel;
    [Header("Является ли диалоговым окном")]
    [SerializeField]
    public bool isMessege;
    public bool winMessege;
    [Header("Кнопка")]
    [SerializeField]
    public bool isButton;
    public bool isFuelButton;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRender = GetComponent<SpriteRenderer>();
        _camera = Camera.main;
        _rb = GetComponent<Rigidbody2D>();
        StartCameraSize = _camera.orthographicSize;
        StartLocalScale = transform.localScale.x;
        StartPos = transform.position;
        _playerModel = FindObjectOfType<PlayerModel>();
        //newBehaviourScript = FindObjectOfType<NewBehaviourScript>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.localScale = new Vector3(1, 1, 0) * _camera.orthographicSize / StartCameraSize * StartLocalScale;
        if(isFuel)
            transform.position = new Vector3 ((_playerModel.Fuel - _playerModel.MaxFuel) / 100 * 3.04f, 0, 0) + _camera.transform.position + StartPos * _camera.orthographicSize / StartCameraSize;
        else
            transform.position = _camera.transform.position + StartPos * _camera.orthographicSize / StartCameraSize;

        if(isButton && !_playerModel.IsOnPlanet)
            transform.position += new Vector3(0, 0, StartPos.z - 20);
        else if(!_playerModel._planet.GetComponent<PlanetModel>().isFuel && isFuelButton)
            transform.position += new Vector3(0, 0, StartPos.z - 20);
        else if(!_playerModel._planet.GetComponent<PlanetModel>().animalAvailability && !isFuelButton && isButton)
            transform.position += new Vector3(0, 0, StartPos.z - 20);
        else if (!isMessege){
             if(isButton && !isFuelButton){
                 _spriteRender.sprite = _spiteArray[_playerModel._planet.GetComponent<PlanetModel>().spriteNumber];
                transform.position += new Vector3(0, 0, StartPos.z + 10);
             }
             else
                transform.position += new Vector3(0, 0, StartPos.z + 20);
            var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            if(transform.position.x - 1 < mousePosition.x && transform.position.x + 1 > mousePosition.x &&
            transform.position.y - 1 < mousePosition.y && transform.position.y + 1 > mousePosition.y && Input.GetMouseButton(0) && isButton){
                if(isFuelButton){
                    _playerModel.Fuel = _playerModel.MaxFuel;
                    _playerModel._planet.GetComponent<PlanetModel>().isFuel = false;
                }
                else{
                    _playerModel.AnimalCount++;
                    _playerModel._planet.GetComponent<PlanetModel>().animalAvailability = false;
                }
            }
        }
        if(isMessege && winMessege && _playerModel.isWin){
            transform.position += new Vector3(0, 0, StartPos.z + 20);
            //Debug.Log(newBehaviourScript);
            //newBehaviourScript.Show();
            Time.timeScale = 0;
        }
        if(isMessege && !winMessege && _playerModel.Fuel == 0){
            transform.position += new Vector3(0, 0, StartPos.z + 20);
            //newBehaviourScript.Show();
            Time.timeScale = 0;
        }
    }
}
