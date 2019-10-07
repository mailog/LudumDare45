using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyAudio : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, GetComponent<AudioSource>().clip.length);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
