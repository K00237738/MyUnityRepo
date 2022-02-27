using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    private Vector3 movement;
    private float speed;
    // Start is called before the first frame update
    void Awake()
    {
        movement = transform.position - GameObject.FindWithTag("Player").transform.position;
        speed = 2.0f* GameObject.Find("GameManager").GetComponent<GameManager>().GetLevel();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = movement*Time.deltaTime*speed;
    }
}
