using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    [HideInInspector]public Animator anime;
    public PlayerMovement playerMovement;

    private void Awake()
    {
        anime = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (anime.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !playerMovement.IsColl && !playerMovement.IsSpin)
        {
            SoundManager.instance.PlayHit2();
            playerMovement.Hp -= 5;
            playerMovement.rigid.velocity = Vector2.zero;
            if (Random.Range(0,2) == 0)
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
