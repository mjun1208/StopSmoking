using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TalkBoxText : MonoBehaviour
{
    public string[] Texts;
    public GameObject[] Heads;

    public GameObject TalkBox;

    private TextMeshProUGUI textUI;
    //public TextMeshPro;
    private string NowText;
    [HideInInspector] public int i_NowText;

    private int textCount;
    private bool IsDone;
    private bool IsNext;

    [HideInInspector] public bool CanNext;

    private void Awake()
    {
        Heads = new GameObject[transform.childCount];
        for (int i = 0; i < Heads.Length; i++)
        {
            Heads[i] = transform.GetChild(i).gameObject;

            // 0 = 화이트
            // 1 = 플레이어
            // 2 = 적
        }

        Texts = new string[8];

        // NowScene 0 0
        Texts[0] = "하하 나는 화이트맨, 담배를 싫어하지.";
        Texts[1] = "오늘도 길거리에서 담배를 피는 사람을 혼내줘야겠군.";

        // NowScene 1 0
        Texts[2] = "앗 바로 발견...!";

        // NowScene 2 2
        Texts[3] = "낄낄 흡연너무 좋아!";

        // NowScene 3 0
        Texts[4] = "감히 길거리에서 담배를 피우다니 용서하지 않겠다!!";

        // NowScene 4 2
        Texts[5] = "낄낄 너는 우리를 막을 수 없다!!";

        // NowScene 5 2
        Texts[6] = "간접흡연으로 까맣게 만들어주지.. 낄낄";

        // NowScene 6 1
        Texts[7] = "으악!! 감히...! 용서못해..!!";
        textCount = 0;

    }
    // Start is called before the first frame update
    void Start()
    {
        textUI = GetComponent<TextMeshProUGUI>();
        i_NowText = 0;
        NowText = Texts[i_NowText];
        textCount = 0;
        if (NowText.Length > 0)
        {
            textUI.text = "";
            textUI.text += NowText[textCount++];

            StartCoroutine(Typing());
        }
        IsDone = false;
        CanNext = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && IsDone)
        {
            IsNext = true;
        }

        Debug.Log(i_NowText);

        if (IsNext && CanNext)
        {
            i_NowText++;

            switch (i_NowText)
            {
                case 0:
                case 1:
                case 2:
                case 4:
                    ChangeHead(0);
                    break;
                case 3:  
                case 5: 
                case 6:
                    ChangeHead(2);
                    break;
                case 7:
                    ChangeHead(1);
                    break;
            }
            NextText();
        }
        else if (IsNext && !CanNext)
        {
            ActiveFalse();

            if (i_NowText == 1)
                Intro.instance.Scene_1();
            if (i_NowText == 2)
                Intro.instance.Scene_2();
        }

       if (i_NowText == 0 || i_NowText == 3 || i_NowText == 4 || i_NowText == 5)
           CanNext = true;
    }

    void NextText()
    {
        IsDone = false;
        IsNext = false;

        if (Texts.Length <= i_NowText)
        {
            ChangeScene.instance.Change_Scene("Ingame");
            Debug.Log("OK");
        }
        else
        {
            textCount = 0;
        
            NowText = Texts[i_NowText];
            textUI.text = "";
            textUI.text += NowText[textCount++];
            StartCoroutine(Typing());
        }
    }

    void ChangeHead(int HeadNum)
    {
        TalkBox.SetActive(true);
        for (int i = 0; i < Heads.Length; i++)
        {
            Heads[i].gameObject.SetActive(false);
        }

        Heads[HeadNum].gameObject.SetActive(true);
    }

    void ActiveFalse()
    {
        TalkBox.SetActive(false);
        for (int i = 0; i < Heads.Length; i++)
        {
            Heads[i].gameObject.SetActive(false);
        }
        textUI.text = "";
    }

    IEnumerator Typing()
    {
        yield return new WaitForSeconds(0.1f);
        if (NowText.Length - textCount > 0)
        {
            textUI.text += NowText[textCount++];
            StartCoroutine(Typing());
        }
        else
        {
            IsDone = true;
            CanNext = false;
        }
    }
}
