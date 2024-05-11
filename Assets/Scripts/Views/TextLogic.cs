using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Models;

public class TextLogic : MonoBehaviour
{
    private TMP_Text canvasText;
    private PlayerModel _playerModel;
    // Start is called before the first frame update
    void Start()
    {
        canvasText = GetComponent<TMP_Text>();
        _playerModel = FindObjectOfType<PlayerModel>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerModel.AnimalCount < _playerModel.animalMaxCount)
            canvasText.text = _playerModel.animalData;
        else
            canvasText.text = "Go back!";
    }
}
