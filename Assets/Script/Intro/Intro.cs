using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public static Intro instance;

    public TalkBoxText TalkBox;
    public GameObject Enemys;
    public GameObject Smokes;
    public GameObject Players;

    public GameObject WhitePlayer;
    public GameObject Player;

    private Animator Whiteanime;

    private float DelayTime;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Whiteanime = WhitePlayer.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Scene_1()
    {
        if (!TalkBox.CanNext && Players.transform.localPosition.x < 12)
        {
            Whiteanime.SetBool("IsWalk", true);
            Players.transform.localPosition += new Vector3(1, 0, 0) * 0.1f;
        }
        else
        {
            Whiteanime.SetBool("IsWalk", false);
            TalkBox.CanNext = true;
        }
    }

    public void Scene_2()
    {
        IntroCamera.instance.Target = null;

        if (!TalkBox.CanNext && IntroCamera.instance.gameObject.transform.position.x < 4.5f)
            IntroCamera.instance.gameObject.transform.position += new Vector3(1, 0, 0) * 0.1f;
        else
        {
            TalkBox.CanNext = true;
        }
    }

    public void Scene_3()
    {
        if (Enemys.transform.position.x > WhitePlayer.transform.position.x)
        {
            Enemys.transform.position -= new Vector3(1, 0, 0) * 0.2f;
        }
        else
        {
            Scene_4();
        }
    }

    public void Scene_4()
    {
        Smokes.SetActive(true);

        DelayTime += Time.deltaTime;
        if (DelayTime > 2.5f)
        {
            Smokes.SetActive(false);
            TalkBox.CanNext = true;
            Player.SetActive(true);
        }
        else if (DelayTime > 2.0f)
        {
            WhitePlayer.SetActive(false);
            Player.SetActive(true);   
        }
    }
}
