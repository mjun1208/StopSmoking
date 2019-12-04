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

        Texts = new string[5];

        Texts[0] = "아닛..? 어째서..?";
        Texts[1] = "니코틴 중독..";
        Texts[2] = "?!!";
        Texts[3] = "후두암 위험, 최대 16배";
        Texts[4] = "?????";
        Texts[5] = "뇌졸중 위험, 최대 4배";
        Texts[6] = "심장병 사망, 최대 4배";
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
        if (Input.GetKeyDown(KeyCode.Escape))
            ChangeScene.instance.Change_Scene("Ingame3");

        if (Input.anyKeyDown && IsDone)
        {
            IsNext = true;
            if (i_NowText == 7)
                ChangeScene.instance.Change_Scene("Ingame3");
        }

        if (IsNext && CanNext)
        {
            i_NowText++;

            switch (i_NowText)
            {
                case 0:
                case 2:
                case 4:
                    ChangeHead(0);
                    break;
                case 1:
                case 3:
                    ChangeHead(1);
                    break;
            }
            NextText();
        }
        else if (IsNext && !CanNext)
        {
            ActiveFalse();
        }
        CanNext = true;
    }

    void NextText()
    {
        IsDone = false;
        IsNext = false;

        if (Texts.Length <= i_NowText)
        {
            ChangeScene.instance.Change_Scene("Ingame3");
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
