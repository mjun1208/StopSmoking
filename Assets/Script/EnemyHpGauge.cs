using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpGauge : MonoBehaviour
{
    private Image HPGaugeImage;
    public GreenEnemyMovement enemyMovement;
    // Start is called before the first frame update
    void Start()
    {
        HPGaugeImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyManager.instance.KillCount < 20)
            HPGaugeImage.fillAmount = EnemyManager.instance.KillCount * 0.05f;
        else
            HPGaugeImage.fillAmount = enemyMovement.Hp * 0.01f;
    }
}
