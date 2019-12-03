using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroEnemy : MonoBehaviour
{
    public GameObject Target;
    public TalkBoxText Text;

    private Vector2 Dir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Text.i_NowText >= 5 && Text.i_NowText <= 6)
        {
            Dir = Target.transform.position - transform.position;
            Dir.Normalize();

            if (Dir.x < 0)
                transform.localScale = new Vector3(-4.3f, 4.3f, 4.3f);
            else
                transform.localScale = new Vector3(4.3f, 4.3f, 4.3f);
        }
    }
}
