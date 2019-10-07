using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NakedHero : MonoBehaviour
{
    public GameObject tackle;

    public GameObject arrowPivot;

    public GameManager gameManager;

    public bool stunned;

    public float stunCounter, stunTime;

    public GameObject blood, stunEffect;

    public float minMagnitude, maxMagnitude;

    public bool keysActive;

    public float xVel, yVel;

    public float velocityFactor;

    public GameObject chargeEffect;

    public SpriteRenderer chargeSprite;

    public bool chargeCD;

    public bool charging;

    public float chargeCounter, chargeTime;

    public float chargeForce;

    public bool chargeRecovery;

    public float recoveryCounter, recoveryTime;

    private Rigidbody2D rb;

    private Animator anim;

    private Transform trans;

    // Start is called before the first frame update
    void Start()
    {
        arrowPivot.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        trans = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!charging && !stunned)
        {
            DetectKeys();
            DetectMouse();
        }
        else if(charging)
        {
            CheckCharge();

            if(chargeRecovery)
                CheckRecovery();
        }
        else if(stunned)
        {
            CheckStun();
        }

        CheckAnim();

        CheckSpeed();

        RotateArrow();
    }

    void RotateArrow()
    {   
        Vector2 heading = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)trans.position;
        float distance = heading.magnitude;
        Vector2 direction = heading / distance;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrowPivot.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
    }

    void CheckStun()
    {
        if (stunCounter >= stunTime)
        {
            stunCounter = 0;
            EndStun();
        }
        else
        {
            stunCounter += Time.deltaTime;
        }
    }

    void StartStun()
    {
        Instantiate(tackle, transform.position, Quaternion.identity);
        GetComponent<WhiteFlash>().FlashWhite();
        stunned = true;
        EndCharge();
        rb.velocity = Vector2.zero;
        stunEffect.SetActive(true);
    }

    void EndStun()
    {
        stunned = false;
        stunEffect.SetActive(false);
    }

    void CheckSpeed()
    {
        if(rb.velocity.magnitude <= minMagnitude)
        {
            rb.velocity = Vector2.zero;
            if(!chargeCD && charging)
            {
                chargeRecovery = true;
            }
        }
    }

    void EndCharge()
    {
        chargeCD = false;
        chargeRecovery = false;
        charging = false;
        chargeEffect.SetActive(false);
    }

    void CheckCharge()
    {
        chargeSprite.color = Color.Lerp(new Color(255, 255, 255, 0), Color.white, rb.velocity.magnitude / maxMagnitude);

        if (chargeCounter >= chargeTime)
        {
            chargeCD = false;
        }
        else
        {
            chargeCounter += Time.deltaTime;
        }
    }

    void CheckRecovery()
    {
        if(recoveryCounter >= recoveryTime)
        {
            chargeRecovery = false;
            charging = false;
            chargeEffect.SetActive(false);
        }
        else
        {
            recoveryCounter += Time.deltaTime;
        }
    }

    void CheckAnim()
    {
        anim.SetFloat("Mag", rb.velocity.magnitude);
        Vector2 vel = rb.velocity.normalized;

        anim.SetFloat("xVel", vel.x);
        anim.SetFloat("yVel", vel.y);
    }

    void DetectKeys()
    {
        if(Input.GetKeyDown("w") || Input.GetKeyDown("s") || Input.GetKeyDown("a") || Input.GetKeyDown("d") || Input.GetKeyDown("up") || Input.GetKeyDown("down") || Input.GetKeyDown("left") || Input.GetKeyDown("right"))
        {
            rb.velocity = Vector2.zero;
        }

        if(Input.GetKeyUp("s") || Input.GetKeyUp("w"))
        {
            rb.velocity = Vector2.zero;
        }

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            rb.velocity = Vector2.zero;
        }

        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            rb.velocity = new Vector2(rb.velocity.x, yVel).normalized * velocityFactor;
        }
        else if (Input.GetKey("down") || Input.GetKey("s"))
        {
            rb.velocity = new Vector2(rb.velocity.x, -yVel).normalized * velocityFactor;
        }
        else
        {
            //rb.velocity = new Vector2(rb.velocity.x, 0).normalized * velocityFactor;
        }


        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            rb.velocity = new Vector2(-xVel, rb.velocity.y).normalized * velocityFactor;
        }
        else if (Input.GetKey("right") || Input.GetKey("d"))
        {
            rb.velocity = new Vector2(xVel, rb.velocity.y).normalized * velocityFactor;
        }
        else
        {
           //rb.velocity = new Vector2(0, rb.velocity.y).normalized * velocityFactor;
        }
    }

    void DetectMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Charge(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    void Charge(Vector2 targetPos)
    {
        chargeEffect.SetActive(true);

        chargeCounter = 0;
        charging = true;
        chargeCD = true;
        chargeRecovery = false;

        rb.velocity = Vector2.zero;
        Vector2 heading = targetPos - (Vector2)trans.position;
        float distance = heading.magnitude;
        Vector2 direction = heading / distance;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        chargeEffect.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        
        rb.AddForce(direction * chargeForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Hazard"))
        {
            gameManager.GameOver();
            Instantiate(blood, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            if(charging)
            {
                StartStun();
            }
        }
    }
    
}
