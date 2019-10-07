using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Transform trans, playerTrans;

    public GameObject player;

    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        trans = transform;
        playerTrans = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTrans != null)    
            trans.position = Vector3.MoveTowards(trans.position, new Vector3(playerTrans.position.x, playerTrans.position.y, -10), moveSpeed * Time.deltaTime);
    }
}
