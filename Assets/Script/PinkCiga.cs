using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkCiga : MonoBehaviour
{
    private Animator anime;
    public Vector2 Dir;
    private Rigidbody2D rigid;
    public float speed = 0.2f;

    public PlayerMovement playerMovement;
    private void Awake()
    {
        anime = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //anime = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        anime.Rebind();
    }

    // Update is called once per frame
    void Update()
    {
        if (Dir.x < 0)
            this.transform.localPosition += new Vector3(-1, 0 , 0) * speed;
        else
            this.transform.localPosition += new Vector3(1, 0, 0) * speed;

        if (transform.localPosition.y < -4)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -4f, 0);
        }
        else if (transform.localPosition.y > -4)
        {
            transform.localPosition += Vector3.down * 0.1f;
        }

        if (anime.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        anime.Rebind();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !playerMovement.IsColl && !playerMovement.IsSpin)
        {
            playerMovement.Hp -= 10;
            playerMovement.rigid.velocity = Vector2.zero;
            if (transform.localScale.x > 0)
            {
                playerMovement.rigid.AddForce(Vector2.right * 5, ForceMode2D.Impulse);
            }
            else
            {
                playerMovement.rigid.AddForce(Vector2.left * 5, ForceMode2D.Impulse);
            }
            playerMovement.IsColl = true;
        }
    }
}
