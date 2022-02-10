using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : Projectile
{
    private bool simpletravel;
    public Vector3 enemyMovmentVector, directionVec;
    // Start is called before the first frame update
    void Start()
    {
        simpletravel = true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        float playerDistance = Vector3.Distance(transform.position, GameObject.Find("Player").transform.position);
        if (playerDistance < 25 && simpletravel)
        {
            simpletravel = false;
            directionVec = (GameObject.Find("Player").transform.position - transform.position);
            //transform.rotation = Quaternion.FromToRotation(transform.position, GameObject.Find("Player").transform.position);
            //transform.Rotate(0, Quaternion.FromToRotation(transform.position, GameObject.Find("Player").transform.position).y, 0);
            transform.LookAt(GameObject.Find("Player").transform.position);
            transform.Rotate(0, 180, 0);
        }
        else
        {
            EnemyMovement();
        }
        if (simpletravel)
        {
            base.Update();
        }

    }

    protected void EnemyMovement()
    {
        //float yRotate = Quaternion.FromToRotation(transform.position, GameObject.Find("Player").transform.position).y;
        //transform.Rotate(new Vector3(0, yRotate, 0));
        enemyMovmentVector = transform.position + directionVec * Time.deltaTime * 1.5f;
        transform.position = enemyMovmentVector;
    }

    protected void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.tag == "Player")//specifically collides with player
        {
            //Destroy(gameObject);//hit the player
            gameObject.SetActive(false);
        }

    }
}
