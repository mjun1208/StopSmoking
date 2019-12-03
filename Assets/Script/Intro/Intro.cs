using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public static Intro instance;

    public TalkBoxText TalkBox;
    public GameObject Enemys;
    public GameObject Players;

    public GameObject WhitePlayer;
    public GameObject Player;

    private Animator Whiteanime;

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
}
