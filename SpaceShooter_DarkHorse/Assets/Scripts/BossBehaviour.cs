using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    private float health, shotDelay;
    private bool coolDown;

    public GameObject bossbullet;
    // Start is called before the first frame update
    void Start()
    {
        health = 1;
        shotDelay = 5;
        coolDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(BossShoot());
        //if(health <= 0)
        //{
        //    gameObject.SetActive(false);
        //}
    }

    IEnumerator BossShoot()
    {
        if(coolDown == false)
        {
            coolDown = true;
            Instantiate(bossbullet, transform.position, bossbullet.transform.rotation);
            yield return new WaitForSeconds(shotDelay);
            coolDown = false;
        }
    }

    public void ResetBoss()
    {
        gameObject.SetActive(true);
        health = 25 * GameObject.Find("GameManager").GetComponent<GameManager>().GetLevel();
        shotDelay = (9 / GameObject.Find("GameManager").GetComponent<GameManager>().GetLevel()) * 0.75f;
        coolDown = false;
    }

    public bool isBossDead()
    {
        if(health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GoodBullet")
        {//bullet from player
            health -= GameObject.FindWithTag("Player").GetComponent<PlayerController>().GetDamage();
            //if(health <= 0)
            //{
            //    gameObject.SetActive(false);
            //}
        }
    }
}
