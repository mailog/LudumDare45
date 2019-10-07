using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReaction : MonoBehaviour
{
    public GameManager gameManager;

    public Collider2D coll;

    public GameObject blood;

    public Transform playerTrans, trans;

    public bool stunned;

    public Rigidbody2D rb;

    public float stunCounter, stunTime;

    public GameObject stunEffect;

    public float knockBackFactor;

    public GameObject hitEffect;

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindWithTag("Player") != null)
            playerTrans = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(stunned)
        {
            StunRecover();
        }
    }

    void StunRecover()
    {
        if (stunCounter >= stunTime)
        {
            stunCounter = 0;
            TurnOffStun();
        }
        else
        {
            stunCounter += Time.deltaTime;
        }
    }


    public void TurnOffStun()
    {
        rb.velocity = Vector2.zero;
        stunned = false;
        stunEffect.SetActive(false);
    }

    void Die()
    {
        gameManager.EnemyDeath();
        Instantiate(blood, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void TurnOnStun()
    {
        GetComponent<WhiteFlash>().FlashWhite();
        Vector2 heading = (Vector2)playerTrans.position - (Vector2)trans.position;
        float distance = heading.magnitude;
        Vector2 direction = heading / distance;

        rb.AddForce(-direction * knockBackFactor);
        stunned = true;
        stunEffect.SetActive(true);
        rb.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<NakedHero>().charging)
            {
                Vector2 contactPoint = collision.contacts[0].point;
                Instantiate(hitEffect, contactPoint, Quaternion.identity);
                TurnOnStun();
            }
        }
        else if(collision.gameObject.CompareTag("Enemy"))
        {
            if(stunned)
                collision.gameObject.GetComponent<EnemyReaction>().TurnOnStun();
        }
        else if (collision.gameObject.CompareTag("Hazard"))
        {
            Die();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<NakedHero>().charging)
            {
                Vector2 contactPoint = collision.contacts[0].point;
                Instantiate(hitEffect, contactPoint, Quaternion.identity);
                TurnOnStun();
            }
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            if (stunned)
                collision.gameObject.GetComponent<EnemyReaction>().TurnOnStun();
        }
        else if (collision.gameObject.CompareTag("Hazard"))
        {
            Die();
        }
    }

}
