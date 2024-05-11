using System.Collections;
using System.Collections.Generic;
using Models;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    GameObject gameObject;
    PlayerModel playerModel;
    Button button;
    // Start is called before the first frame update
    void Start(){
        gameObject = GetComponent<GameObject>();
        button = GetComponent<Button>();
        playerModel = FindObjectOfType<PlayerModel>();
                    transform.gameObject.SetActive(false);

    }
    void FixedUpdate(){

    }
    public void Show(){
            transform.gameObject.SetActive(true);

    }
}
