using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject Player;
    private GameObject[] EnemyArray;

    private void Awake()
    {
        EnemyArray = new GameObject[2];
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
            EnemyArray[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
