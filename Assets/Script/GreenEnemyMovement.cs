using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemyMovement : MonoBehaviour
{
    enum GreenEnemyState
    {
        Idle,
        Jump,
        Shoot,
        SpawnSmoke,
        SpawnCiga
    };
    private GreenEnemyState State;
    private Rigidbody2D rigid;
    private Animator anime;
    public float Speed = 3;
    public float jumpSpeed = 8;

    private Vector2 Target;
    private Vector2 Dir;

    private float DelayTime;
    private float RandomTime;

    public GameObject Player;
    private PlayerMovement playerMovement;

    [HideInInspector] public int Hp = 100;

    private bool IsJump;
    private bool IsDown;
    private bool RandomDir;
    //private bool IsDown;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        playerMovement = Player.GetComponent<PlayerMovement>();

        State = GreenEnemyState.Jump;
        Hp = 100;

        DelayTime = 0;
        RandomTime = Random.Range(0.3f, 1.0f);
        IsJump = false;
        IsDown = false;
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
        else if (transform.localPosition.y > -4 && State != GreenEnemyState.Jump)
        {
            transform.localPosition += Vector3.down * 0.1f;
        }

        if (Target.x > 0)
        {
            transform.localScale = new Vector3(4.3f, 4.3f, 4.3f);
        }
        else
        {
            transform.localScale = new Vector3(-4.3f, 4.3f, 4.3f);
        }
        //if ()
        Debug.Log(rigid.velocity);
        switch (State)
        {
            case GreenEnemyState.Idle:
                rigid.velocity = Vector2.zero;
                DelayTime += Time.deltaTime;
                if (DelayTime > RandomTime && transform.localPosition.y == -4)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        DelayTime = 0;
                        RandomTime = Random.Range(0.3f, 1.0f);
                        State = GreenEnemyState.Jump;
                    }
                    else
                    {
                        DelayTime = 0;
                        RandomTime = Random.Range(0.3f, 1.0f);
                        StartCoroutine(SetAttack());
                    }
                }
                break;
            case GreenEnemyState.Jump:
                if (!IsJump)
                {
                    rigid.velocity = Vector2.zero;
                    if (Random.Range(0, 2) == 0)
                        RandomDir = true;
                    else
                        RandomDir = false;

                    if (RandomDir)
                        rigid.AddForce((Vector3.up + (Vector3.right / 2)) * jumpSpeed, ForceMode2D.Impulse);
                    else
                        rigid.AddForce((Vector3.up + (Vector3.left / 2)) * jumpSpeed, ForceMode2D.Impulse);
                    IsJump = true;
                    anime.SetBool("IsJump", true);
                }
                else
                {
                    if (rigid.velocity.y < 0 && !IsDown)
                    {
                        StartCoroutine(SetAttack());
                        IsDown = true;
                    }
                }

                if (transform.localPosition.y == -4 && IsDown)
                {
                    //State = GreenEnemyState.Idle;
                    IsJump = false;
                    IsDown = false;
                    anime.SetBool("IsJump", false);
                }
                break;
            case GreenEnemyState.Shoot:
                rigid.velocity = Vector2.zero;
                anime.SetBool("IsShoot", true);
                if (anime.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
                {
                    if (transform.localPosition.y == -4)
                    {
                        State = GreenEnemyState.Idle;
                        anime.SetBool("IsShoot", false);
                    }
                }
                break;
            case GreenEnemyState.SpawnSmoke:
                rigid.velocity = Vector2.zero;
                if (transform.localPosition.y == -4)
                {
                    anime.SetBool("IsSpawnSmoke", true);
                    if (anime.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
                        State = GreenEnemyState.Idle;
                }
                break;
            case GreenEnemyState.SpawnCiga:
                rigid.velocity = Vector2.zero;
                anime.SetBool("IsSpawnCiga", true);
                if (anime.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
                {
                    if (transform.localPosition.y == -4)
                    {
                        State = GreenEnemyState.Idle;
                        anime.SetBool("IsSpawnCiga", false);
                    }
                }
                break;
            default:
                break;
        }
    }

    private IEnumerator SetAttack()
    {
        if (State == GreenEnemyState.Idle || State == GreenEnemyState.Jump)
        {
            anime.Rebind();
            if (State == GreenEnemyState.Idle)
            {
                switch (Random.Range(0, 3))
                {
                    case 0:
                        State = GreenEnemyState.SpawnSmoke;
                        break;
                    case 1:
                        State = GreenEnemyState.SpawnCiga;
                        break;
                    case 2:
                        State = GreenEnemyState.Shoot;
                        break;
                }
            }
            else
            {
                switch (Random.Range(0, 2))
                {
                    case 0:
                        State = GreenEnemyState.SpawnCiga;
                        break;
                    case 1:
                        State = GreenEnemyState.Shoot;
                        break;
                }
            }
        }
        yield return null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" && State == GreenEnemyState.Jump)
        {
            if (transform.localPosition.x < 0 && rigid.velocity.x < 0)
                rigid.velocity = new Vector2(0, rigid.velocity.y);
            if (transform.localPosition.x > 0 && rigid.velocity.x > 0)
                rigid.velocity = new Vector2(0, rigid.velocity.y);
        }
    }
}
