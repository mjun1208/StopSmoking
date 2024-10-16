﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    static public EnemyManager instance;
    public GameObject Player;
    private GameObject[] EnemyArray;
    private EnemyMovement[] EnemyMovementArray;

    bool IsClear;
    bool IsAlive;

    public int KillCount;


    private void Awake()
    {
        instance = this;

        EnemyArray = new GameObject[transform.transform.childCount];
        EnemyMovementArray = new EnemyMovement[transform.transform.childCount];
        for (int i = 0; i < EnemyArray.Length; i++)
        {
            EnemyArray[i] = transform.GetChild(i).gameObject;
            EnemyMovementArray[i] = EnemyArray[i].GetComponent<EnemyMovement>();
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
        IsClear = false;
        //KillCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (KillCount < 20)
        {
            IsAlive = true;
            for (int i = 0; i < EnemyArray.Length; i++)
            {
                if (EnemyArray[i].activeSelf)
                {
                    IsAlive = false;
                }
            }

            if (IsAlive && !IsClear)
            {
                IsClear = true;
                StartCoroutine(SpawnEnemy());
            }
        }
        else
        {
            Invoke("SceneChange", 1.5f);
            for (int i = 0; i < EnemyArray.Length; i++)
            {
                //EnemyArray[i].SetActive(false);
                EnemyMovementArray[i].Hp = 0;
            }
        }
    }

    private IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < EnemyArray.Length; i++)
        {
            EnemyArray[i].SetActive(true);
            EnemyMovementArray[i].EnemyReset();
            if (Random.Range(0, 2) == 0)
                EnemyArray[i].transform.position = new Vector3(16, -4, 0);
            else
                EnemyArray[i].transform.position = new Vector3(-16, -4, 0);
        }
        IsClear = false;
        yield return new WaitForSeconds(0.2f);
    }

    void SceneChange()
    {
        ChangeScene.instance.Change_Scene("Intro2");
    }
}
