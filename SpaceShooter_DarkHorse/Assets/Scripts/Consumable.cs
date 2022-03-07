using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Projectile
{
    public int consumableType;
    // Update is called once per frame
    protected override void Update()
    {
        transform.Rotate(new Vector3(0, 15, 0) * Time.deltaTime * (speed / 10));
        base.Update();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);//destory if hits player
            Debug.Log("healthUp!");
            switch(consumableType)
            {
                case 1://heal
                    other.GetComponent<PlayerController>().HealPlayer(20);
                    break;
                case 2://firerate
                    other.GetComponent<PlayerController>().IncreaseFireRate();
                    break;
                case 3://damage
                    other.GetComponent<PlayerController>().IncreaseDameage();
                    break;
                case 4://invulnerability

                    break;
            }
            //should be a switch statement for type of consumable
        }
    }
}
