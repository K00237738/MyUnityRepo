using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceDebri : Projectile
{
    private float health, damage;
    protected void Awake()
    {
        speed = speed * 1.5f;
        health = 5 * GameObject.Find("GameManager").GetComponent<GameManager>().GetLevel();
        damage = 2* GameObject.Find("GameManager").GetComponent<GameManager>().GetLevel();
    }
    // Update is called once per frame
    protected override void Update()
    {
        transform.Rotate(new Vector3(0, 15, 0)*Time.deltaTime*(speed/2));
        base.Update();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);//will destory if hits oob

        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);//destory if hits player
            other.gameObject.GetComponent<PlayerController>().HurtPlayer(damage);
        }
        else if (other.gameObject.tag == "GoodBullet")
        {
            Destroy(gameObject);//destory if player bullet hits
            Destroy(other.gameObject);
        }
        else if(other.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);//destory if enemy bullet hits
            Destroy(other.gameObject);
        }
        else
        {
            return;
        }
    }
}
