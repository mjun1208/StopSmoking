using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 Dir;
    private Rigidbody2D rigid;
    public float speed = 0.2f;

    public PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Dir);
        if (Dir.x < 0)
            transform.localScale = new Vector2(-4.3f,4.3f);
        else
            transform.localScale = new Vector2(4.3f,4.3f);
        if (Dir.x < 0)
            this.transform.localPosition += new Vector3(-1, 0, 0) * speed;
        else
            this.transform.localPosition += new Vector3(1, 0, 0) * speed;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
            this.gameObject.SetActive(false);
        if (collision.gameObject.tag == "Player" && !playerMovement.IsColl && !playerMovement.IsSpin)
        {
            SoundManager.instance.PlayHit2();
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

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player" && !playerMovement.IsColl && !playerMovement.IsSpin)
    //    {
    //        playerMovement.Hp -= 10;
    //        playerMovement.rigid.velocity = Vector2.zero;
    //        if (transform.localScale.x > 0)
    //        {
    //            playerMovement.rigid.AddForce(Vector2.right * 5, ForceMode2D.Impulse);
    //        }
    //        else
    //        {
    //            playerMovement.rigid.AddForce(Vector2.left * 5, ForceMode2D.Impulse);
    //        }
    //        playerMovement.IsColl = true;
    //    }
    //}
}
