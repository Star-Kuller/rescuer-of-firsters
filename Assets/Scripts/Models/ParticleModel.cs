using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleModel : MonoBehaviour
{
    private ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        var emission = particle.emission;
        emission.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void EmmitionContinue(Vector2 direction)
    {
        transform.up = direction;
        var emission = particle.emission;
        emission.enabled = true;
    }
    public void EmmitionStop()
    {
        particle.Clear();
        var emission = particle.emission;
        emission.enabled = false;
    }
}
