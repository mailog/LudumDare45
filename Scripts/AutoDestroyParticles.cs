using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyParticles : MonoBehaviour
{
    public float stopCounter, stopTime = 5;
    public float destroyCounter, destroyTime = 5;

    public ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ps)
        {
            if (!ps.IsAlive())
            {
                Destroy(gameObject);
            }

            if (stopCounter >= stopTime)
            {
                ps.Stop();
            }
            stopCounter += Time.deltaTime;

            if (destroyCounter >= destroyTime)
            {
                Destroy(gameObject);
            }
            destroyCounter += Time.deltaTime;
        }
    }
}
