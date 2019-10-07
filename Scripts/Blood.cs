using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    public SpriteRenderer pool;

    public Sprite[] possiblePools;
    
    // Start is called before the first frame update
    void Start()
    {
        pool.sprite = possiblePools[Random.Range(0, possiblePools.Length)];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
