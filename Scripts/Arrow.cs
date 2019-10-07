using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject hitEffect;

    public float force;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        GetComponent<Rigidbody2D>().AddForce(-transform.right * force);
    }

    void Hit()
    {
        Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        Hit();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Hit();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Hit();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Arena"))
        {
            Hit();
        }
    }
}
