using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasted : MonoBehaviour
{
    private float Delay;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlayPlayerDead();
        Delay = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Delay += Time.deltaTime;
        if (Delay > 5.0f)
            ChangeScene.instance.Scene_Restart();
    }
}
