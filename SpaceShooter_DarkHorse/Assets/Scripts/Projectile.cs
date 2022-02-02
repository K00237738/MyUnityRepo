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
    void Update()
    {
        Movement();
    }

    protected void Movement()
    {
        movement = transform.position + Vector3.forward * Time.deltaTime * speed;
        if(isUpward != true)
        {
            movement *= -1;
        }
        transform.position = movement;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Bullet")
        {
            return;
        }
        Destroy(gameObject);
    }
}
