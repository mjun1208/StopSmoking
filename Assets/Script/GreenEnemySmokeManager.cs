using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemySmokeManager : MonoBehaviour
{
    private GameObject[] Smokes;
    private Smoke[] Smokes_Script;

    public GameObject GreenEnemy;
    private void Awake()
    {
        Smokes = new GameObject[transform.childCount];
        Smokes_Script = new Smoke[transform.childCount];
        for (int i = 0; i < Smokes.Length; i++)
        {
            Smokes[i] = transform.GetChild(i).gameObject;
            Smokes_Script[i] = Smokes[i].GetComponent<Smoke>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnSmoke()
    {
        for (int i = 0; i < Smokes.Length; i++)
        {
            Smokes[i].gameObject.SetActive(true);
            Vector2 Pos =  new Vector2(GreenEnemy.transform.position.x , GreenEnemy.transform.position.y) + new Vector2(Random.Range(-4, 4), Random.Range(-2, 2));
            Smokes[i].transform.position = Pos;
            Smokes_Script[i].anime.Rebind();
        }
    }
}
