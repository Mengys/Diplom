using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Mob : MonoBehaviour
{
    [SerializeField]
    GameObject hpbar;
    [SerializeField]
    GameObject dmgPopup;

    GameObject gameController;
    public JsonMob jsonMob;
    public bool updated = false;
    private float health;


    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        health = jsonMob.health;
    }

    void Update()
    {

        Move();

        UpdateHealth();
    }

    void Move()
    {
        transform.position = new Vector3(jsonMob.x, jsonMob.y, 0);
    }

    void UpdateHealth()
    {
        Vector3 scale = hpbar.transform.localScale;
        scale.x = jsonMob.health / 100;
        hpbar.transform.localScale = scale;

        if (health > jsonMob.health)
        {
            GetComponent<AudioSource>().Play();

            dmgPopup.GetComponent<DamagePopup>().Setup((int)Math.Round(health - jsonMob.health));

            Instantiate(dmgPopup, new Vector3(jsonMob.x + 1.45f, jsonMob.y + 1f, 0), Quaternion.identity);
        }
        health = jsonMob.health;
    }
}
