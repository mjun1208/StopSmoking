using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TalkBoxText4_1 : MonoBehaviour
{
    public string[] Texts;

    private TextMeshProUGUI textUI;
    //public TextMeshPro;
    private string NowText;
    [HideInInspector] public int i_NowText;

    private int textCount;
    private bool IsDone;
    private bool IsNext;

    public bool IsFinish;

    [HideInInspector] public bool CanNext;

    private void Awake()
    {
        Texts = new string[10];

        //Texts[0] = "아닛..? 어째서..?";
        Texts[0] = "매년 600만명 사망..";
        Texts[1] = "니코틴 중독..";
        Texts[2] = "4천 가지 이상의 화학물질 속 69가지는 발암물질..";
        Texts[3] = "폐암 위험, 4.6배";
        Texts[4] = "후두암 위험, 6.5배";
        Texts[5] = "구강암 위험, 4.6배";
        Texts[6] = "식도암 위험, 3.6배";
        Texts[7] = "방광암 위험, 2.6배";
        Texts[8] = "뇌졸중 위험, 4배";
        Texts[9] = "심장병 사망, 4배";
        //Texts[13] = "넌 이미 흡연으로 인해 약해져 있었다..";
        //Texts[14] = "크윽.. 내가 이렇게 위험한 것을 하고 있었다니..";
        //Texts[15] = "이제 깨달았으면 금연을 하여라..";
        //Texts[16] = "알겠습니다.. 흑흑";
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
        IsFinish = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //    ChangeScene.instance.Change_Scene("Ingame3");

        if (Input.anyKeyDown && IsDone)
        {
            IsNext = true;
        }

        if (IsNext && CanNext)
        {
            i_NowText++;

            NextText();
        }
        CanNext = true;
    }

    void NextText()
    {
        IsDone = false;
        IsNext = false;


        if (Texts.Length <= i_NowText)
        {
            IsFinish = true;
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

    IEnumerator Typing()
    {
        yield return new WaitForSeconds(0.1f);
        if (NowText.Length - textCount > 0)
        {

            //SoundManager.instance.PlayTalk_Sans();

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
