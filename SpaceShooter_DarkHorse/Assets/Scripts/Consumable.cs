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
                case 1:

                    break;
                case 2:

                    break;
                case 3:

                    break;
                case 4:

                    break;
            }
            //should be a switch statement for type of consumable
        }
    }
}
