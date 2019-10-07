using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverEffects : MonoBehaviour
{
    public Image fadeBG;

    public Color startColor, endColor;

    public float effectCounter, effectTime;

    public Vector2 ogPos;

    public Transform cameraTrans;

    public Transform trans;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(effectCounter < effectTime)
            CheckEffect();
    }

    void CheckEffect()
    {
        effectCounter += Time.deltaTime;
        trans.position = Vector2.Lerp(ogPos, cameraTrans.position, effectCounter/effectTime);
        fadeBG.color = Color.Lerp(startColor, endColor, effectCounter / effectTime);
    }

    private void OnEnable()
    {
        ogPos = trans.position;
    }
}
