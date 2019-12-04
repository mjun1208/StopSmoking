using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TalkBoxText4 : MonoBehaviour
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

    private bool IsClear;

    [HideInInspector] public bool CanNext;

    private void Awake()
    {
        Heads = new GameObject[transform.childCount];
        for (int i = 0; i < Heads.Length; i++)
        {
            Heads[i] = transform.GetChild(i).gameObject;

            // 0 = 플레이어
            // 1 = 적
        }

        Texts = new string[7];

        Texts[0] = "아닛..? 어째서..?";
        Texts[1] = "아직도 깨닫지 못한건가?";
        Texts[2] = "???!!!";
        //Texts[1] = "매년 600만명 사망..";
        //Texts[2] = "니코틴 중독..";
        //Texts[3] = "?!!";
        //Texts[4] = "4천 가지 이상의 화학물질 속 69가지는 발암물질..";
        //Texts[5] = "폐암 위험, 4.6배";
        //Texts[6] = "후두암 위험, 6.5배";
        //Texts[7] = "구강암 위험, 4.6배";
        //Texts[8] = "식도암 위험, 3.6배";
        //Texts[9] = "방광암 위험, 2.6배";
        //Texts[10] = "???!@#!@#!#??";
        //Texts[11] = "뇌졸중 위험, 4배";
        //Texts[12] = "심장병 사망, 4배";
        Texts[3] = "넌 이미 흡연으로 인해 약해져 있었다..";
        Texts[4] = "크윽.. 내가 이렇게 위험한 것을 하고 있었다니..";
        Texts[5] = "이제 깨달았으면 금연을 하여라..";
        Texts[6] = "알겠습니다.. 흑흑";
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
        IsClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //    ChangeScene.instance.Change_Scene("Ingame3");
        if (IsClear)
        {
            Intro4.instance.Scene_2();
        }
        else
        {
            if (Input.anyKeyDown && IsDone)
            {
                IsNext = true;
            }

            if (IsNext && CanNext)
            {
                i_NowText++;

                switch (i_NowText)
                {
                    case 0:
                    case 2:
                    case 4:
                    case 6:
                        ChangeHead(1);
                        break;
                    case 1:
                    case 3:
                    case 5:
                        ChangeHead(0);
                        break;
                }
                NextText();
            }
            else if (IsNext && !CanNext)
            {
                ActiveFalse();
                if (i_NowText == 2)
                    Intro4.instance.Scene_1();
            }
            if (i_NowText != 2)
                CanNext = true;
        }
    }

    void NextText()
    {
        IsDone = false;
        IsNext = false;

        if (Texts.Length <= i_NowText)
        {
            IsClear = true;
            //Intro4.instance.Scene_2();
            //ChangeScene.instance.Change_Scene("Ingame3");
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

    void ChangeHead(int HeadNum = -1)
    {
        TalkBox.SetActive(true);
        for (int i = 0; i < Heads.Length; i++)
        {
            Heads[i].gameObject.SetActive(false);
        }

        if (HeadNum != -1)
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
            if(Heads[0].activeSelf)
                SoundManager.instance.PlayTalk_Sans();
            else
                SoundManager.instance.PlayTalk_Pa();
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
