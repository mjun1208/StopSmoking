using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject Player;
    public float Speed = 3;
    private Vector2 Target;
    private Vector2 Dir;

    private Rigidbody2D rigid;
    private Animator anime;
    private float DelayTime;
    private float RandomTime;

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
        DelayTime = 0;
        RandomTime = Random.Range(0.3f, 1.5f);
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
                //State = EnemyState.Walk;
                //rigid.MovePosition(transform.position + (new Vector3(Target.x, 0, 0) * Speed * Time.deltaTime));
                //if (Target.x > 0)
                //{
                //    transform.localScale = new Vector3(4.3f, 4.3f, 4.3f);
                //}
                //else
                //{
                //    transform.localScale = new Vector3(-4.3f, 4.3f, 4.3f);
                //}
                break;

            case EnemyState.Jump:
                //if (transform.localScale.x > 0)
                //{
                //    rigid.AddForce(Vector3.up * Speed, ForceMode2D.Impulse);
                //    //transform.position += ((Vector3.right + Vector3.up) * Time.deltaTime * Speed);
                //}
                //else
                //{
                //    rigid.AddForce(Vector3.up * Speed, ForceMode2D.Impulse);
                //    //transform.position += ((Vector3.left + Vector3.up) * Time.deltaTime * Speed);
                //}
                rigid.AddForce(Vector3.up * Speed, ForceMode2D.Impulse);
                State = EnemyState.Walk;
                break;

            case EnemyState.Coll:
                break;

            case EnemyState.Dead:
                break;
        }
    }

    private IEnumerator SetAttack()
    {
        if (State == EnemyState.Walk)
        {
            float Distance = Vector2.Distance(new Vector2(Player.transform.position.x, 0), new Vector2(transform.position.x, 0));
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
}
