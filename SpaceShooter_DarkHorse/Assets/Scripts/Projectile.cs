using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Vector3 movement;
    public float speed;
    public bool isUpward;

    //This class will be the main bullet class and other moveable objects in game will inherit from this.

    // Update is called once per frame
    protected virtual void Update()
    {
        Movement();
    }

    protected virtual void Movement()
    {
        if(isUpward != true)
        {
            movement = transform.position + Vector3.back * Time.deltaTime * speed;
        }
        else
        {
            movement = transform.position + Vector3.forward * Time.deltaTime * speed;
        }
        transform.position = movement;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        string objectTag = gameObject.tag;
        if (other.gameObject.tag == "OOB")
        {
            Destroy(gameObject);//destory out of bounds
        }
        if(gameObject.tag == "Bullet"/* || gameObject.tag == "GoodBullet"*/)//check if current gameobject is a bullet of sorts
        {
            if(other.gameObject.tag == "Player")
            {
                Destroy(gameObject);//hit the player
            }
        }
    }

}
