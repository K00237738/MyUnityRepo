using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float h_move, v_move, boundaryX, boundaryZtop, boundaryZbot;
    private Vector3 playerMotion, gun1, gun2;
    private int health, max_health;
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
        //h_move = Input.GetAxis("Horizontal");
        //v_move = Input.GetAxis("Vertical");
        //transform.position = transform.position + new Vector3(h_move, 0, v_move) * Time.deltaTime * speed;


        // Track a single touch as a direction control.
        if (Input.touchCount > 0)
        {
            Touch playerInput = Input.GetTouch(0);
            // Handle finger movements based on touch phase.
            switch (playerInput.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    h_move = playerInput.position.x;
                    v_move = playerInput.position.y;
                    break;
            }
        }
        transform.position = transform.position + new Vector3(h_move, 0, v_move) * Time.deltaTime * speed;

        CheckBoundaries();
    }

    private void CheckBoundaries()
    {//keep player within screen
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
        {//if key input and gun is ready, shoot
            gun1 = GameObject.Find("Gun").transform.position;
            gun2 = GameObject.Find("Gun2").transform.position;
            Instantiate(lazershot, gun1, lazershot.transform.rotation);
            Instantiate(lazershot, gun2, lazershot.transform.rotation);
            StartCoroutine(ShotDelay());
        }
    }

    IEnumerator ShotDelay()
    {//fire delay
        gunReady = false;
        yield return new WaitForSeconds(shotdelay);
        gunReady = true;
    }

    //methods for modifying player stats e.g. health and damage
    public void HurtPlayer(int damage)
    {
        health-=damage;
    }

    public void HealPlayer(int addhealth)
    {
        health += addhealth;
    }

    public void IncreaseFireRate()
    {
        shotdelay *= 0.75f;//decrease by 75 percent
    }

    public void IncreaseDameage()
    {

    }
    public void InitializeLevelStats()
    {//initialize stats for level
        health = 25*GameObject.Find("GameManage").GetComponent<GameManager>().level;
    }


}
