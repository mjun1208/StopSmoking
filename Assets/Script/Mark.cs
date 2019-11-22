using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Vector2 Dir;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Dir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        rigid.MovePosition(this.transform.position + (new Vector3(Dir.x, Dir.y , 0) * Time.deltaTime * 20));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 NormalVec = collision.contacts[0].normal;
        Dir = Vector2.Reflect(Dir, NormalVec);
    }
}
