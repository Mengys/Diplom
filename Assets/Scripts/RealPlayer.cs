using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class RealPlayer : MonoBehaviour
{

    [SerializeField] GameObject hpbar;
    [SerializeField] GameObject dmgPopup;
    
    GameObject serverController;
    GameObject gameController;
    public JsonPlayer jsonPlayer;
    private float health;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        serverController = GameObject.FindGameObjectWithTag("ServerController");
        health = jsonPlayer.health;
    }

    void Update()
    {

        Move();

        MoveCamera();

        UpdateHealthBar();

    }

    void Move()
    {
        transform.position = new Vector3(jsonPlayer.x, jsonPlayer.y, 0);

        /*        float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");

                float speed = 10f;
                if (horizontalInput != 0f || verticalInput != 0f)
                {
                    Vector3 pos = transform.position;
                    pos.x += horizontalInput * Time.deltaTime * speed;
                    pos.y += verticalInput * Time.deltaTime * speed;
                    transform.position = pos;
                }*/
    }

    void MoveCamera()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        Vector3 pos;
        pos.x = transform.position.x;
        pos.y = transform.position.y;
        pos.z = camera.transform.position.z;
        camera.transform.position = pos;
    }

    void UpdateHealthBar()
    {
        UIHPBar.UpdateHealth(jsonPlayer.health);

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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        WorldItem worldItem = collider.GetComponent<WorldItem>();
        if (worldItem != null)
        {
            ServerController.PickUpItem(worldItem.drop.id);

            Debug.Log("pickup");

/*            inventory.AddItem(worldItem.GetItem());
*/            worldItem.DestroySelf();

        }
    }
}
