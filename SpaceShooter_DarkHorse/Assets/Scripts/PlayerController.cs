using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float h_move, v_move, boundaryX, boundaryZtop, boundaryZbot;
    private Vector3 playerMotion, gun1, gun2;
    private int health;
    private bool gunReady;

    public GameObject lazershot;
    public float speed, shotdelay = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        boundaryX = GameObject.Find("GameManager").GetComponent<GameManager>().boundary;
        boundaryZbot = -20;
        boundaryZtop = 120;
        StartCoroutine(ShotDelay());
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();
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
        if (transform.position.x > boundaryX)
        {
            transform.position = new Vector3(boundaryX, 0, transform.position.z);
        }
        else if (transform.position.x < -boundaryX)
        {
            transform.position = new Vector3(-boundaryX, 0, transform.position.z);
        }

        if (transform.position.z > boundaryZtop)
        {
            transform.position = new Vector3(transform.position.x, 0, boundaryZtop);
        }
        else if (transform.position.z < boundaryZbot)
        {
            transform.position = new Vector3(transform.position.x, 0, boundaryZbot);
        }
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gunReady)
        {
            gun1 = GameObject.Find("Gun").transform.position;
            gun2 = GameObject.Find("Gun2").transform.position;
            Instantiate(lazershot, gun1, lazershot.transform.rotation);
            Instantiate(lazershot, gun2, lazershot.transform.rotation);
            StartCoroutine(ShotDelay());
        }
    }

    IEnumerator ShotDelay()
    {
        gunReady = false;
        yield return new WaitForSeconds(shotdelay);
        gunReady = true;
    }


}
