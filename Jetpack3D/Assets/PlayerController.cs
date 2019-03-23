﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jetpackForce;
    public float movingForce;

    public int score;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, jetpackForce, 0));
        }

        this.GetComponent<Rigidbody>().velocity = new
        Vector3(movingForce * Time.deltaTime, //this is done by frame rate as update is called every frame. It will make it move same speed no matter the frame rate
         this.GetComponent<Rigidbody>().velocity.y,
         this.GetComponent<Rigidbody>().velocity.z);
    }

    private void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.GetComponent<ObstacleController>() != null)
        {
            GameObject.Destroy(this.gameObject);
        }

        if (c.gameObject.GetComponent<CoinController>() != null)
        {
            GameObject.Destroy(c.gameObject);
            if(c.gameObject.GetComponent<CoinController>().coinType == 1)
            {
                score += 5;
            }
            else if (c.gameObject.GetComponent<CoinController>().coinType == 2)
            {
                score += 20;
            }


        }
    }

 
}