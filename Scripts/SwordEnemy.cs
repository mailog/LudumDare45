using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemy : MonoBehaviour
{
    public GameObject spinSound;

    public Collider2D coll;

    public EnemyReaction reaction;
    
    //0 - up
    //1 - down
    //2 - right
    //3 - left

    public bool entered;

    public bool ready, attacking;

    public GameObject attackHB;

    public float activeAttackCounter, activeAttackTime;

    public float attackCounter, attackTime;

    public float attackCDCounter, attackCDTime;

    public int dir;

    public Vector2 direction;
    
    public float distance, attackDistance;

    private Rigidbody2D rb;

    private Animator anim;

    private Transform trans;

    public GameObject player;

    public Transform playerTrans;

    public Vector2 targetPos;

    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player != null)
            playerTrans = player.transform;
        trans = transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if (!reaction.stunned)
            {
                if (entered)
                {
                    CheckAttack();
                    MoveToPlayer();
                }
                else
                {
                    Enter();
                }
                CheckAnim();
            }
            else
            {
                CheckStun();
            }
        }
        else
        {
            attackCounter = 0;
            rb.velocity = Vector2.zero;
            dir = -1;
            anim.SetInteger("dir", dir);
        }
    }

    void Enter()
    {
        targetPos = Vector2.zero;
        trans.position = Vector2.MoveTowards(trans.position, targetPos, moveSpeed * Time.deltaTime);
    }
    
    void CheckStun()
    {
        ready = false;
        attacking = false;
        activeAttackCounter = 0;
        attackHB.SetActive(false);
        if (dir != -1)
        {
            dir = -1;
            anim.SetInteger("dir", dir);
            anim.Play("Idle");
        }
    }

    private void MoveToPlayer()
    {
        targetPos = playerTrans.position;
        trans.position = Vector2.MoveTowards(trans.position, targetPos, moveSpeed * Time.deltaTime);
    }

    void CheckAttack()
    {
        if(attacking)
        {
            if (activeAttackCounter >= activeAttackTime)
            {
                ready = false;
                attacking = false;
                activeAttackCounter = 0;
                attackHB.SetActive(false);
            }
            else
            {
                activeAttackCounter += Time.deltaTime;
            }
        }
        else if(ready)
        {
            if (attackCounter >= attackTime)
            {
                attackCounter = 0;
                attacking = true;
                Instantiate(spinSound, transform.position, Quaternion.identity);
                anim.Play("Attack");
                attackHB.SetActive(true);
                Physics2D.IgnoreCollision(coll, attackHB.GetComponent<Collider2D>());
                activeAttackCounter = 0;
                attackCDCounter = attackCDTime;
            }
            else
            {
                attackCounter += Time.deltaTime;
            }

        }
        else
        {
            if (attackCDCounter <= 0)
            {
                if (distance <= attackDistance)
                {
                    ready = true;
                    anim.Play("Anticipation");  
                    /*switch (dir)
                    {
                        case 0:
                            anim.Play("Attack Up");
                            break;
                        case 1:
                            anim.Play("Attack Down");
                            break;
                        case 2:
                            anim.Play("Attack Side");
                            break;
                        case 3:
                            anim.Play("Attack Side");
                            break;
                        default:
                            break;
                    }*/
                }
                else if(attackCounter > 0)
                {
                    attackCounter = 0;
                }
            }
            else
            {
                attackCDCounter -= Time.deltaTime;
            }
        }
    }

    void CheckAnim()
    {
        Vector2 heading = targetPos - (Vector2)trans.position;
        distance = heading.magnitude;
        direction = heading / distance;

        float absX = Mathf.Abs(direction.x);
        float absY = Mathf.Abs(direction.y);

        if (absY > absX)
        {
            if(direction.y >= 0)
            {
                dir = 0;
            }
            else
            {
                dir = 1;
            }
        }
        else
        {
            if(direction.x >= 0)
            {
                dir = 2;
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                dir = 3;
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        anim.SetInteger("dir", dir);
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Arena"))
        {
            entered = true;
        }
    }
}
