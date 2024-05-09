using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEditor.VersionControl;
using UnityEngine;

public class PlanetModel : MonoBehaviour, IService
{
    private Camera _camera;
    private Rigidbody2D _rb;
    private HashSet<Rigidbody> affectedBodies = new HashSet<Rigidbody>();
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _rb = transform.GetComponent<Rigidbody2D>();
        var services = ServiceLocator.Current;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("oooo");
        if (other.attachedRigidbody != null)
        {
            affectedBodies.Add(other.attachedRigidbody);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.attachedRigidbody != null)
        {
            affectedBodies.Remove(other.attachedRigidbody);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("l");

        foreach (Rigidbody body in affectedBodies)
        {
            Debug.Log("j");
            Vector3 directionToPlanet = (transform.position - body.position).normalized;
            body.AddForce(directionToPlanet * -1000);
        }
    }
}
