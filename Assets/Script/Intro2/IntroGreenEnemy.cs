using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroGreenEnemy : MonoBehaviour
{
    private Animator anime;
    public bool IsGround;
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y < -4)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -4f, 0);
            //IsGround = false;
        }
        else if (transform.localPosition.y > -4)
        {
            transform.localPosition += Vector3.down * 0.1f;
            anime.SetBool("IsJump", true);
            IsGround = false;
        }
        else
        {
            IsGround = true;
            anime.SetBool("IsJump", false);
        }
    }
}
