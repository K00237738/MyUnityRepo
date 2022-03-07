using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float h_move, v_move, boundaryX, boundaryZtop, boundaryZbot, damage, health, max_health;
    private Vector3 playerMotion;
    private bool gunReady;

    public GameObject lazershot, Gun1, Gun2;
    public float speed, shotdelay = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        boundaryZbot = -20;
        boundaryZtop = 120;
        StartCoroutine(ShotDelay());
        health = 30 * GameObject.Find("GameManager").GetComponent<GameManager>().GetLevel();
        damage = 2 * GameObject.Find("GameManager").GetComponent<GameManager>().GetLevel();
        boundaryX = GameObject.Find("GameManager").GetComponent<GameManager>().boundary;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();
        if(health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void Movement()
    {
#if UNITY_STANDALONE || UNITY_EDITOR 
        h_move = Input.GetAxis("Horizontal");
        v_move = Input.GetAxis("Vertical");
        transform.position = transform.position + new Vector3(h_move, 0, v_move) * Time.deltaTime * speed;

#else
        //touch controls
        if (Input.touchCount > 0)
        {
            Touch playerInput = Input.GetTouch(0);
            if (playerInput.phase == TouchPhase.Began)
            switch (playerInput.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    h_move = playerInput.position.x;
                    v_move = playerInput.position.y;//get its x and y(z) position on screen
                    break;
            }
            playerMotion = transform.position-new Vector3(h_move, 0, v_move);
        }
        //transform.position = transform.position + new Vector3(h_move, 0, v_move) * Time.deltaTime * speed;
        transform.position = transform.position + playerMotion * Time.deltaTime * speed;

#endif

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

    public void Shoot()
    {//for keyboard
        if (Input.GetKeyDown(KeyCode.Space) && gunReady)
        {//if key input and gun is ready, shoot
            Instantiate(lazershot, Gun1.transform.position, lazershot.transform.rotation);
            Instantiate(lazershot, Gun2.transform.position, lazershot.transform.rotation);
            StartCoroutine(ShotDelay());
        }
    }

    public void ShootTouch()
    {//for touch screen, can be called from other scripts
        if (gunReady)
        {//if key input and gun is ready, shoot
            Instantiate(lazershot, Gun1.transform.position, lazershot.transform.rotation);
            Instantiate(lazershot, Gun2.transform.position, lazershot.transform.rotation);
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
    public void HurtPlayer(float damage)
    {
        health-=damage;
    }

    public void HealPlayer(float addhealth)
    {
        health += addhealth;
    }

    public void IncreaseFireRate()
    {
        shotdelay *= 0.75f;//decrease by 75 percent
    }

    public void IncreaseDameage()
    {
        damage *= 1.25f;
    }
    public void InitializeLevelStats()
    {//initialize stats for level
        transform.position -= transform.position;//set to 0,0,0
        health = 30 * GameObject.Find("GameManager").GetComponent<GameManager>().GetLevel();
        damage = 1 * GameObject.Find("GameManager").GetComponent<GameManager>().GetLevel();
    }

    public float GetDamage()
    { return damage; }

    public bool IsDead()
    {
        if (health <= 0)
        {
            return true;
        }
        else return false;
    }

}
