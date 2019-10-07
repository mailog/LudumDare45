using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteFlash : MonoBehaviour
{
    public bool flashing;

    public float flashCounter, flashTime;

    public Material defaultMat, whiteMat;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (flashing)
        {
            if (flashCounter <= 0)
            {
                SetSelfMat(defaultMat);
                flashing = false;
            }
            else
            {
                flashCounter -= Time.deltaTime;
            }
        }
    }

    public void FlashWhite()
    {
        flashCounter = flashTime;
        SetSelfMat(whiteMat);
        flashing = true;
    }

    public void SetSelfMat(Material mat)
    {
        gameObject.GetComponent<SpriteRenderer>().material = mat;
    }
}