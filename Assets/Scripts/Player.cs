using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Player : MonoBehaviour
{

    [SerializeField] GameObject hpbar;

    [SerializeField] GameObject dmgPopup;

    GameObject gameController;
    public JsonPlayer jsonPlayer;
    private float health;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        health = jsonPlayer.health;
    }

    void Update()
    {

        Move();

        UpdateHealthBar();

    }

    void Move()
    {
        transform.position = new Vector3(jsonPlayer.x, jsonPlayer.y, 0);
    }

    void UpdateHealthBar()
    {
        Vector3 scale = hpbar.transform.localScale;
        scale.x = jsonPlayer.health / 100;
        hpbar.transform.localScale = scale;

        if (health > jsonPlayer.health)
        {
            dmgPopup.GetComponent<DamagePopup>().Setup((int)Math.Round(health - jsonPlayer.health));

            Instantiate(dmgPopup, new Vector3(jsonPlayer.x + 1.45f, jsonPlayer.y + 1f, 0), Quaternion.identity);
        }
        health = jsonPlayer.health;
    }
}
