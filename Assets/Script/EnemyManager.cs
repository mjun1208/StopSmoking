using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject Player;
    private GameObject[] EnemyArray;

    bool IsClear;
    bool IsAlive;

    private void Awake()
    {
        EnemyArray = new GameObject[transform.transform.childCount];
        for (int i = 0; i < EnemyArray.Length; i++)
        {
            EnemyArray[i] = transform.GetChild(i).gameObject;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < EnemyArray.Length; i++)
        {
            EnemyArray[i].SetActive(true);
            if (Random.Range(0, 2) == 0)
                EnemyArray[i].transform.position = new Vector3(16, -4, 0);
            else
                EnemyArray[i].transform.position = new Vector3(-16, -4, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        IsAlive = true;
        for (int i = 0; i < EnemyArray.Length; i++)
        {
            if (EnemyArray[i].activeSelf)
            {
                IsAlive = false;
            }
        }
    }

    private IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < EnemyArray.Length; i++)
        {
            EnemyArray[i].SetActive(true);
            if (Random.Range(0, 2) == 0)
                EnemyArray[i].transform.position = new Vector3(16, -4, 0);
            else
                EnemyArray[i].transform.position = new Vector3(-16, -4, 0);
        }
        yield return new WaitForSeconds(0.2f);
    } 
}
