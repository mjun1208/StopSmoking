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

    private float DelayTime;
    private float RandomTime;

    public GameObject Player;
    private PlayerMovement playerMovement;

    [HideInInspector] public int Hp = 100;

    private bool IsJump;
    private bool IsDown;
    private bool RandomDir;

    private bool IsInvincibility;

    public GameObject PinkCiga;
    private PinkCiga PinkCiga_Script;
    public GameObject Bullet;
    private Bullet Bullet_Script;
    public GreenEnemySmokeManager SmokeManager;

    private bool IsFirst;
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
        IsInvincibility = false;

        IsFirst = false;

        PinkCiga_Script = PinkCiga.GetComponent<PinkCiga>();
        Bullet_Script = Bullet.GetComponent<Bullet>();
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
        switch (State)
        {
            case GreenEnemyState.Idle:
                rigid.velocity = Vector2.zero;
                DelayTime += Time.deltaTime;

                anime.SetBool("IsJump", false);
                anime.SetBool("IsShoot", false);
                anime.SetBool("IsSpawnCiga", false);
                anime.SetBool("IsSpawnSmoke", false);
                anime.Rebind();
                if (DelayTime > RandomTime && transform.localPosition.y == -4 && anime.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
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
                    SoundManager.instance.PlayJump();
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
                if (!IsFirst)
                {
                    SoundManager.instance.PlaySwing();
                    anime.Rebind();
                    IsFirst = true;
                    Bullet.transform.localPosition = this.gameObject.transform.localPosition;
                    Bullet_Script.Dir = new Vector2(Target.x, 0);
                }

                Bullet.SetActive(true);
                rigid.velocity = Vector2.zero;
                anime.SetBool("IsShoot", true);
                if (anime.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && anime.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
                {
                    if (transform.localPosition.y == -4)
                    {
                        State = GreenEnemyState.Idle;
                        anime.SetBool("IsShoot", false);
                    }
                }
                break;
            case GreenEnemyState.SpawnSmoke:
                if (!IsFirst)
                {
                    anime.Rebind();
                    IsFirst = true;
                }

                rigid.velocity = Vector2.zero;
                if (transform.localPosition.y == -4)
                {
                    anime.SetBool("IsSpawnSmoke", true);
                    if (anime.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && anime.GetCurrentAnimatorStateInfo(0).IsName("SpawnSmoke"))
                    {
                        State = GreenEnemyState.Idle;
                        IsInvincibility = false;
                    }
                    else if (anime.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.6f && anime.GetCurrentAnimatorStateInfo(0).IsName("SpawnSmoke"))
                    {
                        IsInvincibility = true;
                        SoundManager.instance.PlayFart();
                        SmokeManager.SpawnSmoke();
                    }

                    //Debug.Log(anime.GetCurrentAnimatorStateInfo(0).normalizedTime);
                }
                break;
            case GreenEnemyState.SpawnCiga:
                if (!IsFirst)
                {
                    SoundManager.instance.PlaySwing();
                    anime.Rebind();
                    IsFirst = true;
                    PinkCiga.transform.localPosition = this.gameObject.transform.localPosition;
                    PinkCiga_Script.Dir = new Vector2(Target.x, 0);
                }

                rigid.velocity = Vector2.zero;
                anime.SetBool("IsSpawnCiga", true);
                PinkCiga.SetActive(true);
                if (anime.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && anime.GetCurrentAnimatorStateInfo(0).IsName("SpawnCiga"))
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
        anime.Rebind();
        IsFirst = false;
        if (State == GreenEnemyState.Idle || State == GreenEnemyState.Jump)
        {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerAttackColl" && !IsInvincibility)
        {
            SoundManager.instance.PlayHit();
            if (!playerMovement.IsJump)
                Hp -= 10;
            else
                Hp -= 5;
            if (Hp <= 0)
                Hp = 0;
        }
    }
}
