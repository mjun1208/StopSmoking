using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpGauge : MonoBehaviour
{
    private Image HPGaugeImage;
    // Start is called before the first frame update
    void Start()
    {
        HPGaugeImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        HPGaugeImage.fillAmount = EnemyManager.instance.KillCount * 0.05f;
    }
}
