using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLogic : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject layout; // Assign in inspector
    void Start()
    {
        layout = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        layout.SetActive(false);
    }
}
