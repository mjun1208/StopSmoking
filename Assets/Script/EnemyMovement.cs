using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject Player;
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

        switch (State)
        {
            case EnemyState.Walk:
                DelayTime += Time.deltaTime;
                //rigid.MovePosition(transform.position + (new Vector3(Target.x , 0 , 0) * Speed * Time.deltaTime));
                transform.position += (new Vector3(Target.x, 0, 0) * Time.deltaTime * Speed);
                if (Target.x > 0)
                    transform.localScale = new Vector3(4.3f, 4.3f, 4.3f);
                else
                    transform.localScale = new Vector3(-4.3f, 4.3f, 4.3f);


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
                collider.enabled = false;

                if (transform.position.y == -4)
                {
                    invincibility += Time.deltaTime;
                    rigid.velocity = Vector2.zero;
                }

                if (invincibility > 1f)
                {
                    invincibility = 0;
                    State = EnemyState.Walk;
                    anime.SetBool("IsKnockOut", false);
                    anime.SetBool("IsColl", false);
                    IsKnockOut = false;
                    collider.enabled = true;
                }
                break;
            case EnemyState.Dead:
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
            }

            invincibility = 0;
            anime.SetBool("IsJump", false);
            anime.SetBool("IsFlyAttack", false);
            anime.SetBool("IsAttack", false);
        }
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "PlayerAttackColl")
    //    {
    //        State = EnemyState.Coll;
    //        if (IsJump)
    //        {
    //            IsFlyKick = false;
    //            IsJump = false;
    //            anime.SetBool("IsKnockOut", true);
    //        }
    //        else
    //        {
    //            anime.SetBool("IsColl", true);
    //        }
    //
    //        anime.SetBool("IsJump", false);
    //        anime.SetBool("IsFlyAttack", false);
    //        anime.SetBool("IsAttack", false);
    //    }
    //}
}
