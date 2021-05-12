using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{

    int a = 0;
    public void Setup(int damageAmount)
    {
        gameObject.GetComponent<TextMeshPro>().SetText(damageAmount.ToString());
    }

    void Update()
    {
        a++;
        Vector3 pos = transform.position;
        pos.y += 0.01f;
        transform.position = pos;
        if (a==50)
        {
            Destroy(gameObject);
        }
    }
}
