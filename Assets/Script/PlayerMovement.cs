using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator anime;
    public GameObject Image;
    public GameObject AttackColl;

    public float Speed = 5;
    public float JumpSpeed = 6;

    [HideInInspector]public bool IsJump;
    private bool IsFlyAttack;
    private bool IsAttack;
    private bool IsSpin;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anime = GetComponentInChildren<Animator>();

        IsJump = false;
        IsFlyAttack = false;
        IsAttack = false;
        IsSpin = false;
    }

    // Update is called once per frame
    void Update()
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
        }
        else if (Input.GetKeyDown(KeyCode.X) && !IsAttack)
        {
            AttackColl.SetActive(true);
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
            }
        }
    }
}
