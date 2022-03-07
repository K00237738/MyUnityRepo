using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    private Vector3 movement;
    private float speed;
    private int damage;
    // Start is called before the first frame update
    void Awake()
    {
        movement = transform.position - GameObject.FindWithTag("Player").transform.position;
        speed = 2.0f* GameObject.Find("GameManager").GetComponent<GameManager>().GetLevel();
        damage = 14;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position+movement * Time.deltaTime*speed;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().HurtPlayer(damage);
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "OOB")
        {
            Destroy(gameObject);
        }
    }
}
