using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rigid;
    private Animator anime;
    public GameObject Image;
    public GameObject AttackColl;

    public GameObject Wasted;

    public float Speed = 5;
    public float JumpSpeed = 6;

    [HideInInspector] public bool IsJump;
    [HideInInspector] public bool IsColl; 
    private bool IsFlyAttack;
    private bool IsAttack;
    [HideInInspector] public bool IsSpin;
    [HideInInspector] public int Hp = 100;

    private float invincibility;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anime = GetComponentInChildren<Animator>();

        IsJump = false;
        IsFlyAttack = false;
        IsAttack = false;
        IsSpin = false;
        IsColl = false;
        Hp = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Hp <= 0)
        {
            Wasted.SetActive(true);
            anime.SetBool("IsJump", false);
            anime.SetBool("IsFlyAttack", false);
            anime.SetBool("IsSpin", false);
            anime.SetBool("IsAttack", false);
            anime.SetBool("IsMove", false);

            anime.SetBool("IsKnockOut", true);

            if (IsJump)
                anime.SetBool("IsGround", false);
            else
                anime.SetBool("IsGround", true);
        }
        else
        {
            if (IsColl)
            {
                anime.SetBool("IsJump", false);
                anime.SetBool("IsFlyAttack", false);
                anime.SetBool("IsSpin", false);
                anime.SetBool("IsAttack", false);
                anime.SetBool("IsMove", false);
                IsSpin = false;
                IsAttack = false;
                IsFlyAttack = false;

                if (IsJump)
                {
                    anime.SetBool("IsKnockOut", true);
                }
                else
                {
                    anime.SetBool("IsColl", true);
                    anime.SetBool("IsGround", true);
                    invincibility += Time.deltaTime;
                }

                if (invincibility > 0.5f)
                {
                    IsColl = false;
                    anime.SetBool("IsKnockOut", false);
                    anime.SetBool("IsColl", false);
                    invincibility = 0;
                }
            }
            else
            {
                if (IsJump)
                    anime.SetBool("IsJump", true);
                else
                {
                    anime.SetBool("IsJump", false);
                    IsFlyAttack = false;
                    anime.SetBool("IsFlyAttack", false);
                }

                if (!IsSpin && !IsFlyAttack && !IsAttack)
                    Move();
                else if (IsSpin)
                {
                    if (anime.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
                    {
                        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                        anime.SetBool("IsSpin", false);
                        IsSpin = false;
                    }
                    else
                    {
                        if (transform.localScale.x > 0)
                            rigid.MovePosition(transform.position + (Vector3.right * Time.deltaTime * Speed));
                        else
                            rigid.MovePosition(transform.position + (Vector3.left * Time.deltaTime * Speed));
                    }
                }
                else if (IsFlyAttack)
                {
                    AttackColl.SetActive(true);
                    if (transform.localScale.x > 0)
                    {
                        rigid.velocity = Vector3.zero;
                        rigid.MovePosition(transform.position + ((Vector3.right + Vector3.down) * Time.deltaTime * Speed * 2));
                    }
                    else
                    {
                        rigid.velocity = Vector3.zero;
                        rigid.MovePosition(transform.position + ((Vector3.left + Vector3.down) * Time.deltaTime * Speed * 2));
                    }
                }
                else if (IsAttack)
                {
                    if (anime.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f)
                    {
                        anime.SetBool("IsAttack", false);
                        IsAttack = false;
                    }
                    else if (anime.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.4f)
                    {
                        AttackColl.SetActive(true);
                    }

                }
            }
        }
    }

    void Move() {
        AttackColl.SetActive(false);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            anime.SetBool("IsMove", true);
            transform.localScale = new Vector3(-4.3f, 4.3f, 4.3f);
            
            transform.position += (Vector3.left * Time.deltaTime * Speed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            anime.SetBool("IsMove", true);
            transform.localScale = new Vector3(4.3f, 4.3f, 4.3f);

            transform.position += (Vector3.right * Time.deltaTime * Speed);
        }
        else
        {
            anime.SetBool("IsMove", false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && !IsJump)
        {
            rigid.AddForce(Vector3.up * JumpSpeed, ForceMode2D.Impulse);
            IsJump = true;
        }
        else if (Input.GetKey(KeyCode.Z) && !IsJump)
        {
            anime.Rebind();
            anime.SetBool("IsSpin", true);
            IsSpin = true;
            SoundManager.instance.PlaySpin();
        }
        else if (Input.GetKeyDown(KeyCode.X) && !IsAttack)
        {
            if (IsJump)
            {
                anime.SetBool("IsFlyAttack", true);
                IsFlyAttack = true;
            }
            else
            {
                anime.Rebind();
                anime.SetBool("IsAttack", true);
                IsAttack = true;
                SoundManager.instance.PlayAxeSwing();
            }
        }
    }
}
