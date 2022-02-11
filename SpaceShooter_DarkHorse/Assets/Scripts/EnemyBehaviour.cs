using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : Projectile
{
    private bool simpletravel, fireDelay, isTargeting, directionSwitch, hunterLaunching;
    private float swayDistance, startingXposition;

    public Vector3 enemyMovmentVector, directionVec;
    public bool isSeeker, isHunter, isEasy;
    public GameObject enemyShot, gun;
    public float fireRate, launchDelay;
    // Start is called before the first frame update
    void Start()
    {
        if(!isSeeker)
        {
            swayDistance = Random.Range(15, 25);
            startingXposition = transform.position.x;
        }
        simpletravel = false;
        isTargeting = false;
        directionSwitch = false;
        hunterLaunching = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        float playerDistance = Vector3.Distance(transform.position, GameObject.Find("Player").transform.position);
        if (isSeeker)
        {
            if(!isEasy)
            {
                if (playerDistance < 25 && simpletravel)
                {
                    simpletravel = false;
                    isTargeting = true;//stop periodic firing
                    directionVec = (GameObject.Find("Player").transform.position - transform.position);
                    transform.LookAt(GameObject.Find("Player").transform.position);
                    //transform.Rotate(0, 180, 0);//the droid enemy fighter weirdly does not turn right (reverses)
                }
                else
                {
                    SeekerDiveBomb();
                }
                if (simpletravel)
                {
                    base.Update();//normal movement of a projectile
                }
            }
        }
        else//enemy is a hunter type
        {
            if(!isTargeting && simpletravel)//while they are not targeting and simply travelling down screen
            {//two booleans to ensure this is entered when needed and not when uneeded (also, one boolena is used later on)
                if(directionSwitch)
                {//sway to the right
                    transform.position = transform.position + Vector3.right * Time.deltaTime * speed;
                    if(transform.position.x >= (transform.position.x + swayDistance))
                    {
                        directionSwitch = false;
                    }
                }
                else
                {//sway to the left
                    transform.position = transform.position + Vector3.left * Time.deltaTime * speed;
                    if (transform.position.x <= (transform.position.x - swayDistance))
                    {
                        directionSwitch = true;
                    }
                }
                base.Update();//still travel down
                if(playerDistance < 25)
                {
                    isTargeting = true;//break out of loop
                    hunterLaunching = true;
                    simpletravel = false;
                }
            }

            else//hunter is targeting player
            {
                //stop and aim for a few seconds then launch
                if(hunterLaunching)
                {//positon in direction of player for set amount of time
                    StartCoroutine(HunterLaunch());
                    directionVec = (GameObject.Find("Player").transform.position - transform.position);
                    transform.LookAt(GameObject.Find("Player").transform.position);
                }
                else
                {//laucnh
                    SeekerDiveBomb();
                }
            }
        }

    }

    protected void SeekerDiveBomb()
    {
        enemyMovmentVector = transform.position + directionVec * Time.deltaTime * 1.5f;
        transform.position = enemyMovmentVector;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.tag == "Player")//specifically collides with player
        {
            //Destroy(gameObject);//hit the player
            gameObject.SetActive(false);
        }
    }

    IEnumerator EnemyShoot()
    {
        if(fireDelay && !isTargeting)
        {//is it ok to fire and are they not in a targeting phase(dive bombing)
            if(isSeeker)
            {//fire periodically if a seeker and not currently targeting
                fireDelay = false;
                Instantiate(enemyShot, gun.transform.position, enemyShot.transform.rotation);
                yield return new WaitForSeconds(fireRate);
            }
            else
            {
                if((transform.position.x == GameObject.Find("Player").transform.position.y) && !isTargeting)
                {//if enemy alligns and not currently targeting
                    fireDelay = false;
                    Instantiate(enemyShot, gun.transform.position, enemyShot.transform.rotation);
                    yield return new WaitForSeconds(fireRate);
                }//else don't fire
            }
        }
    }

    IEnumerator HunterLaunch()
    {
        isTargeting = true;
        yield return new WaitForSeconds(launchDelay);
        hunterLaunching = false;
    }
}
