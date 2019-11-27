using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject Player;
    public float Speed = 5;
    private Vector2 Target;
    private Vector2 Dir;

    private Rigidbody2D rigid;

    private enum EnemyState
    {
        Walk,
        Attack,
        Jump
    };

    private EnemyState State;
    // Start is called before the first frame update
    void Start()
    {
        State = EnemyState.Walk;
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y != -4)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -4f, 0);
        }
        else
        {

        }

        switch (State)
        {
            case EnemyState.Walk:
                Target = Player.transform.position - transform.position;
                Target.Normalize();
                rigid.MovePosition(transform.position + (new Vector3(Target.x , 0 , 0) * Speed * Time.deltaTime));


                //Dir = Mathf.Atan2(Target.y, Target.x) * Mathf.Rad2Deg;

                break;
            case EnemyState.Attack:
                break;
        }
    }
}
