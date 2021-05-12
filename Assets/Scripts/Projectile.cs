using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    GameObject gameController;
    public JsonProjectile jsonProjectile;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position = new Vector3(jsonProjectile.x, jsonProjectile.y, 0);
    }
}
