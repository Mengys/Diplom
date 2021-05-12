using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHPBar : MonoBehaviour
{

    Transform healthPositon;
    Transform hpText;

    private float maxHealth = 100;
    private static float currentHealth = 100;
    private void Awake()
    {
        healthPositon = transform.Find("HealthPosition");
        hpText = transform.Find("HPText");
    }

    // Update is called once per frame
    void Update()
    {
        RectTransform hppos = healthPositon.GetComponent<RectTransform>();
        hppos.localScale = new Vector3(currentHealth/ maxHealth, 1,1);
        TextMeshProUGUI hpTextTMP = hpText.GetComponent<TextMeshProUGUI>();
        hpTextTMP.SetText(((int)currentHealth).ToString());
    }

    public static void UpdateHealth(float health)
    {
        currentHealth = health;
    }
}
