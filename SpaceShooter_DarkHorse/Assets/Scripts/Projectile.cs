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

    protected void Movement()
    {
        if(isUpward != true)
        {
            movement = transform.position + Vector3.left * Time.deltaTime * speed;
        }
        else
        {
            movement = transform.position + Vector3.right * Time.deltaTime * speed;
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
        if(gameObject.tag == "Bullet")//check if current gameobject is a bullet of sorts
        {
            if(other.gameObject.tag == "Player")
            {
                Destroy(gameObject);//hit the player
            }
        }
        else if (gameObject.tag == "GoodBullet")
        {
            if (other.gameObject.tag == "Enemy")//an enemy
            {
                other.gameObject.GetComponent<EnemyBehaviour>().HurtEnemy((int)GameObject.FindWithTag("Player").GetComponent<PlayerController>().GetDamage());//hit the player
            }
        }
    }

}
