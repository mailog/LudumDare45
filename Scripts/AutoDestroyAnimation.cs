using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyAnimation : MonoBehaviour
{
    public float destroyTime;
    
    // Use this for initialization
    void Start()
    {
        destroyTime = this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        StartCoroutine(SelfDestroy());
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSecondsRealtime(destroyTime);
        Destroy(gameObject);
    }
}
