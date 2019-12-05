using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro4 : MonoBehaviour
{
    public static Intro4 instance;
    public TalkBoxText4 TalkBox;
    public TalkBoxText4_1 TalkBox2;

    public Image BackGroundCanvers;
    public GameObject Monologue;

    public GameObject Credit;

    private float Alpha = 0;
    private bool First;

    private float DelayTime;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        First = false;
        DelayTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Scene_1()
    {
        if (TalkBox2.IsFinish)
        {
            //for (int i = 0; i < TalkBox2.Cancers.Count; i++)
            //    TalkBox2.Cancers[i].SetActive(false);
            TalkBox2.Cancer.SetActive(false);
            BackGroundCanvers.color = new Color(0, 0, 0, 0);
            TalkBox.CanNext = true;
            Monologue.SetActive(false);
        }
        else
        {
            BackGroundCanvers.color = new Color(0, 0, 0, Alpha);
            Alpha += 0.01f;

            if (BackGroundCanvers.color.a >= 1)
            {
                Monologue.SetActive(true);
            }
        }
    }
    public void Scene_2()
    {
        if (!First)
        {
            BackGroundCanvers.color = new Color(0, 0, 0, 0);
            Alpha = 0;
            First = true;
        }
        if (Alpha >= 1)
        {
            BackGroundCanvers.color = new Color(0, 0, 0, 1);
            if (Credit.GetComponent<RectTransform>().localPosition.y < 2350)
            Credit.transform.position += Vector3.up * 0.05f;
            else
            {
                DelayTime += Time.deltaTime;
                if (DelayTime > 2.0f)
                {
                    ChangeScene.instance.Change_Scene("Title");
                }
            }
        }
        else
        {
            BackGroundCanvers.color = new Color(0, 0, 0, Alpha);
            Alpha += 0.01f;
        }

    }
}
