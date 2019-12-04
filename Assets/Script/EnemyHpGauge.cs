using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        if (EnemyManager.instance != null && EnemyManager.instance.KillCount < 20)
            HPGaugeImage.fillAmount = EnemyManager.instance.KillCount * 0.05f;
        else if (enemyMovement != null)
        {
            if (SceneManager.GetActiveScene().name == "Ingame2")
                HPGaugeImage.fillAmount = enemyMovement.Hp * 0.01f;
            else if (SceneManager.GetActiveScene().name == "Ingame3")
                HPGaugeImage.fillAmount = enemyMovement.Hp * 0.05f;
        }
        else
            HPGaugeImage.fillAmount = EnemyManager.instance.KillCount * 0.05f;
    }
}
