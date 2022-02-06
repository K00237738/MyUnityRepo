using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Vector3 movement;
    public float speed;
    public bool isUpward;
    // Start is called before the first frame update
    void Start()
    {

    }

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

    protected void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Bullet")
        {
            Debug.Log("Player!");
            Destroy(gameObject);//probably remove, action should take place with their scripts
        }
        else if (other.gameObject.tag == "OOB")
        {
            Debug.Log("OOB!");
            Destroy(gameObject);//destory out of bounds
        }
        else
        {
            return;
        }
    }
}
