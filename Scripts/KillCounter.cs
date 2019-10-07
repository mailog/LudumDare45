using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    public int prevCount;

    public int currCount;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddKill()
    {
        currCount++;
    }

    public int GetKills()
    {
        return currCount;
    }

    public void Reset()
    {
        currCount = 0;
    }
}
