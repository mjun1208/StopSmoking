using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpGauge : MonoBehaviour
{
    public PlayerMovement Player;
    private Image HPGaugeImage; 
    // Start is called before the first frame update
    void Start()
    {
        HPGaugeImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        HPGaugeImage.fillAmount = Player.Hp * 0.01f;
        //Player.Hp;
    }
}
