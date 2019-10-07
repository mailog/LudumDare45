using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
    public GameObject arrow;

    //0 - up
    //1 - down
    //2 - right
    //3 - left

    public Collider2D coll;

    public int dir;
    
    public bool ready;

    public float readyCounter, readyTime;

    public float attackCounter, attackTime;

    public SpriteRenderer sr;

    public Sprite[] loadedSprites, unloadedSprites;

    // Start is called before the first frame update
    void Start()
    {
        CheckDirection();
    }


    // Update is called once per frame
    void Update()
    {
        if(ready)
        {
            CheckAttack();
        }
        else
        {
            CheckReady();
        }
    }
    
    void CheckDirection()
    {
        sr.sprite = unloadedSprites[dir];
        if(dir == 2)
        {
            sr.flipX = true;
        }
    }

    void CheckAttack()
    {
        if(attackCounter >= attackTime)
        {
            Shoot();
            attackCounter = 0;
            ready = false;
            sr.sprite = unloadedSprites[dir];
        }
        else
        {
            attackCounter += Time.deltaTime;
        }
    }

    void CheckReady()
    {
        if(readyCounter >= readyTime)
        {
            readyCounter = 0;
            ready = true;
            sr.sprite = loadedSprites[dir];
        }
        else
        {
            readyCounter += Time.deltaTime;
        }
    }

    void Shoot()
    {
        GameObject tmp = Instantiate(arrow, transform.position, Quaternion.identity);
        Vector2 direction = Vector2.zero;

        switch (dir)
        {
            case 0:
                direction = new Vector2(0, 1);
                break;
            case 1:
                direction = new Vector2(0, -1);
                break;
            case 2:
                direction = new Vector2(1, 0);
                break;
            case 3:
                direction = new Vector2(-1, 0);
                break;
        }
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        tmp.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));

        Physics2D.IgnoreCollision(coll, tmp.GetComponent<Collider2D>());
        tmp.GetComponent<Arrow>().Shoot();
    }
}
