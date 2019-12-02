using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject Player;
    private PlayerMovement playermovement;
    public float Speed = 3;
    public float jumpSpeed = 8;
    private Vector2 Target;
    private Vector2 Dir;

    private Rigidbody2D rigid;
    private Animator anime;
    private BoxCollider2D collider;
    private float DelayTime;
    private float RandomTime;

    private bool IsJump;
    private bool IsFlyKick;

    private int Hp = 100;
    public GameObject HpEdge;
    private SpriteRenderer HpGauge_SpriteRender;
    
    private float invincibility;
    private bool IsKnockOut;
    private enum EnemyState
    {
        Walk,
        Attack,
        Jump,
        Coll,
        Dead
    };

    private EnemyState State;
    // Start is called before the first frame update
    void Start()
    {
        playermovement = Player.GetComponent<PlayerMovement>();
        State = EnemyState.Walk;
        rigid = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        DelayTime = 0;
        RandomTime = Random.Range(0.3f, 1.5f);
        IsJump = false;
        IsFlyKick = false;
        IsKnockOut = false;

        invincibility = 0;

        HpGauge_SpriteRender = HpEdge.transform.GetChild(0).GetComponent<SpriteRenderer>();
        Hp = 100;
    }

    // Update is called once per frame
    void Update()
    {
        Target = Player.transform.position - transform.position;
        Target.Normalize();

        if (transform.localPosition.y < -4)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -4f, 0);
        }
        else if (transform.localPosition.y > -4 && State != EnemyState.Jump)
        {
            transform.localPosition += Vector3.down * 0.1f;
        }

        if (Hp <= 0 && State != EnemyState.Dead) 
        {
            anime.Rebind();
            anime.SetBool("IsDead", true);
            anime.speed = 0.1f;
            State = EnemyState.Dead;
        }

        switch (State)
        {
            case EnemyState.Walk:
                DelayTime += Time.deltaTime;
                //rigid.MovePosition(transform.position + (new Vector3(Target.x , 0 , 0) * Speed * Time.deltaTime));
                transform.position += (new Vector3(Target.x, 0, 0) * Time.deltaTime * Speed);
                if (Target.x > 0)
                {
                    transform.localScale = new Vector3(4.3f, 4.3f, 4.3f);
                    HpEdge.transform.localScale = new Vector3(0.2f, HpEdge.transform.localScale.y, HpEdge.transform.localScale.z);
                }
                else
                {
                    transform.localScale = new Vector3(-4.3f, 4.3f, 4.3f);
                    HpEdge.transform.localScale = new Vector3(-0.2f, HpEdge.transform.localScale.y, HpEdge.transform.localScale.z);
                }

                if (DelayTime > RandomTime)
                {
                    DelayTime = 0;
                    StartCoroutine(SetAttack());
                    RandomTime = Random.Range(0.3f, 3f);
                }

                break;

            case EnemyState.Attack:
                anime.SetBool("IsAttack", true);

                if (anime.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f)
                {
                    State = EnemyState.Walk;
                    anime.SetBool("IsAttack", false);
                }
                break;

            case EnemyState.Jump:
                if (!IsJump)
                {
                    rigid.velocity = Vector2.zero;
                    rigid.AddForce(Vector3.up * jumpSpeed, ForceMode2D.Impulse);
                    IsJump = true;
                    anime.SetBool("IsJump", true);
                }
                else
                {
                    if (rigid.velocity.y < 0)
                    {
                        if (transform.localScale.x > 0)
                        {
                            //rigid.velocity = Vector3.zero;
                            rigid.MovePosition(transform.position + ((Vector3.right + Vector3.down) * Time.deltaTime * jumpSpeed * 2));
                        }
                        else
                        {
                            //rigid.velocity = Vector3.zero;
                            rigid.MovePosition(transform.position + ((Vector3.left + Vector3.down) * Time.deltaTime * jumpSpeed * 2));
                        }
                        //IsJump = false;
                        IsFlyKick = true;
                        anime.SetBool("IsFlyAttack",true);
                    }
                }

                if (transform.localPosition.y == -4 && IsFlyKick)
                {
                
                    State = EnemyState.Walk;
                    IsFlyKick = false;
                    IsJump = false;

                    anime.SetBool("IsJump", false);
                    anime.SetBool("IsFlyAttack", false);
                }
                break;

            case EnemyState.Coll:
                //collider.enabled = false;
                if (transform.position.y == -4)
                {
                    invincibility += Time.deltaTime;
                    if (IsKnockOut)
                        rigid.velocity = Vector2.zero;
                }

                if (invincibility > 1f)
                {
                    rigid.velocity = Vector2.zero;
                    invincibility = 0;
                    State = EnemyState.Walk;
                    anime.SetBool("IsKnockOut", false);
                    anime.SetBool("IsColl", false);
                    IsKnockOut = false;
                    //collider.enabled = true;
                }
                break;
            case EnemyState.Dead:
                if (transform.localPosition.y == -4)
                {
                    rigid.velocity = Vector2.zero;
                    anime.speed = 0.7f; 

                    if (anime.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
                        this.gameObject.SetActive(false);
                }
                break;
        }
    }

    private IEnumerator SetAttack()
    {
        if (State == EnemyState.Walk)
        {
            float Distance = Vector3.Distance(Player.transform.position, transform.position);
            if (Distance < 1.5f)
            {
                anime.Rebind();
                State = EnemyState.Attack;
            }
            else
                State = EnemyState.Jump;
        }
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerAttackColl" && State != EnemyState.Coll)
        {
            if (!playermovement.IsJump)
                Hp -= 20;
            else
                Hp -= 10;
            if (Hp <= 0)
                Hp = 0;
            HpGauge_SpriteRender.size = new Vector2((Hp * 0.02f), 0.5f);
            State = EnemyState.Coll;
            if (IsJump)
            {
                IsFlyKick = false;
                IsJump = false;
                anime.SetBool("IsKnockOut", true);
                IsKnockOut = true;
                
                if (Player.transform.localScale.x > 0)
                {
                    rigid.AddForce((Vector2.up + Vector2.right) * 3,ForceMode2D.Impulse);
                }
                else
                {
                    rigid.AddForce((Vector2.up + Vector2.left) * 3, ForceMode2D.Impulse);
                }
            }
            else
            {
                anime.SetBool("IsColl", true);
                IsKnockOut = false;

                if (Player.transform.localScale.x > 0)
                {
                    rigid.AddForce(Vector2.right * 5, ForceMode2D.Impulse);
                }
                else
                {
                    rigid.AddForce(Vector2.left * 5, ForceMode2D.Impulse);
                }
            }

            invincibility = 0;
            anime.SetBool("IsJump", false);
            anime.SetBool("IsFlyAttack", false);
            anime.SetBool("IsAttack", false);
        }
        else if (collision.gameObject.tag == "Wall" && State == EnemyState.Coll)
        {
            rigid.velocity = Vector2.zero;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !playermovement.IsColl && !playermovement.IsSpin)
        {
            if (IsFlyKick || State == EnemyState.Attack)
            {
                playermovement.Hp -= 10;
                playermovement.rigid.velocity = Vector2.zero;
                if (transform.localScale.x > 0)
                {
                    playermovement.rigid.AddForce(Vector2.right * 5, ForceMode2D.Impulse);
                }
                else
                {
                    playermovement.rigid.AddForce(Vector2.left * 5, ForceMode2D.Impulse);
                }
                playermovement.IsColl = true;
            }
        }
    }
}
