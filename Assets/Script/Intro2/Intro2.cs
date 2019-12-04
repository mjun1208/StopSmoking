using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro2 : MonoBehaviour
{
    public static Intro2 instance;

    public TalkBoxText2 TalkBox;
    public GameObject Enemy;
    private IntroGreenEnemy Enemy_Script;

    private float DelayTime;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Enemy_Script = Enemy.GetComponentInChildren<IntroGreenEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Scene_1()
    {
        Enemy.SetActive(true);
        if (Enemy_Script.IsGround)
            TalkBox.CanNext = true;
    }
}
