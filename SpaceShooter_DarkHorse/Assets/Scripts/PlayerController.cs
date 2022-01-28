using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float h_move, v_move;
    private Vector3 playerMotion;
    private int health;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        h_move = Input.GetAxis("Horizontal");
        v_move = Input.GetAxis("Vertical");
        transform.position = transform.position + new Vector3(h_move, 0, v_move) * Time.deltaTime * speed;
        CheckBoundaries();
    }

    private void CheckBoundaries()
    {
        if (transform.position.x > 28)
        {
            transform.position = new Vector3(28, 0, transform.position.z);
        }
        else if (transform.position.x < -28)
        {
            transform.position = new Vector3(-28, 0, transform.position.z);
        }

        if (transform.position.z > 25)
        {
            transform.position = new Vector3(transform.position.x, 0, 25);
        }
        else if (transform.position.z < -35)
        {
            transform.position = new Vector3(transform.position.x, 0, -35);
        }
    }
}
