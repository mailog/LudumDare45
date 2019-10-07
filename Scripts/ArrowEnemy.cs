using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowEnemy : MonoBehaviour
{
    public GameObject shootSound;

    public bool attackRec;
    public float attackRecCounter, attackRecTime;

    public int dir;

    public Animator anim;

    public bool attacking;

    public Collider2D coll;

    public GameObject arrow;

    private Vector2 targetPos, heading, direction;

    private float distance;

    public float moveSpeed;

    public GameObject player;

    public Transform playerTrans, trans;

    public EnemyReaction reaction;

    public bool entered;

    public float attackDistance;

    public float attackCounter, attackTime;

    public Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if(player)
        {
            playerTrans = player.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (!reaction.stunned)
            {
                if(attackRec)
                {
                    attackCounter = 0;
                    rb.velocity = Vector2.zero;
                    dir = -1;
                    anim.SetInteger("dir", dir);
                    RecoverAttack();
                }
                else
                {
                    if (entered)
                    {
                        targetPos = playerTrans.position;
                        Calculate();
                        CheckAttack();
                    }
                    else
                    {
                        targetPos = Vector2.zero;
                    }
                    if (!attacking)
                        MoveToTarget();
                    CheckAnim();
                }
            }
            else
            {
                CheckStun();
            }
        }
        else
        {
            attacking = false;
            attackCounter = 0;
            rb.velocity = Vector2.zero;
            dir = -1;
            anim.SetInteger("dir", dir);
        }

    }

    void RecoverAttack()
    {
        if (attackRecCounter >= attackRecTime)
        {
            attacking = false;
            attackRec = false;
            attackRecCounter = 0;
        }
        else
        {
            attackRecCounter += Time.deltaTime;
        }
    }

    void CheckAnim()
    {
        float absX = Mathf.Abs(direction.x);
        float absY = Mathf.Abs(direction.y);

        if (absY > absX)
        {
            if (direction.y >= 0)
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
            if (direction.x >= 0)
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

    void Calculate()
    {
        targetPos = playerTrans.position;
        heading = targetPos - (Vector2)trans.position;
        distance = heading.magnitude;
        direction = heading / distance;
    }

    void CheckAttack()
    {
        if(attacking)
        {
            if (attackCounter >= attackTime)
            {
                attackCounter = 0;
                attacking = false;
                Attack();
                attackRec = true;
                anim.SetBool("attacking", attacking);
            }
            else
            {
                attackCounter += Time.deltaTime;
            }
        }
        else if (distance <= attackDistance)
        {
            attacking = true;
            anim.SetBool("attacking", attacking);
            switch(dir)
            {
                case 0:
                    anim.Play("Up Ready");
                    break;
                case 1:
                    anim.Play("Down Ready");
                    break;
                case 2:
                    anim.Play("Left Ready");
                    break;
                case 3:
                    anim.Play("Left Ready");
                    break;
                default:
                    break;
            }
        }
    }
    
    void Attack()
    {
        Instantiate(shootSound, transform.position, Quaternion.identity);
        GameObject tmp = Instantiate(arrow, transform.position, Quaternion.identity);
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        tmp.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));

        tmp.GetComponent<Arrow>().Shoot();

        Physics2D.IgnoreCollision(coll, tmp.GetComponent<Collider2D>());
    }

    void MoveToTarget()
    {
        if(!entered || distance > attackDistance)
            trans.position = Vector2.MoveTowards(trans.position, targetPos, moveSpeed * Time.deltaTime);
    }

    void Enter()
    {
        trans.position = Vector2.MoveTowards(trans.position, targetPos, moveSpeed * Time.deltaTime);
    }

    void CheckStun()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arena"))
        {
            entered = true;
        }
    }
}
