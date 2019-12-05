using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private bool IsHowTo;
    private bool IsInfo;
    public GameObject HowTo;
    // Start is called before the first frame update
    void Start()
    {
        IsHowTo = false;
        IsInfo = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton()
    {
        if(!IsHowTo && !IsInfo)
            ChangeScene.instance.Change_Scene("Intro");
    }

    public void HowToButton()
    {
        if (!IsHowTo && !IsInfo)
        {
            IsHowTo = true;
            HowTo.SetActive(true);
        }
    }

    public void InfoButton()
    {
        if (!IsHowTo && !IsInfo)
            IsInfo = true;
    }

    public void ExitButton()
    {
        if (!IsHowTo && !IsInfo)
            Application.Quit();
    }

    public void CloseHowTo()
    {
        if (IsHowTo)
        {
            IsHowTo = false;
            HowTo.SetActive(false);
        }
    }

    public void CloseInfo()
    {
        if (IsInfo)
            IsInfo = false;
    }
}
