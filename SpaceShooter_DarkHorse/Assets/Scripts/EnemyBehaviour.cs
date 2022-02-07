using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : Projectile
{
    private bool simpletravel;
    private Vector3 enemyMovmentVector;
    // Start is called before the first frame update
    void Start()
    {
        simpletravel = true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        //base.Update();
        Movement();
    }

    protected override void Movement()
    {
        float playerDistance = Vector3.Distance(transform.position, GameObject.Find("Player").transform.position);
        transform.rotation = Quaternion.FromToRotation(transform.position, GameObject.Find("Player").transform.position);
        //enemyMovmentVector = (GameObject.Find("Player").transform.position - transform.position) * Time.deltaTime * speed;
        //transform.Translate(enemyMovmentVector);

        //base.Movement();
        //transform.position = (GameObject.Find("Player").transform.position - transform.position) * Time.deltaTime * speed;
        //if (/*playerDistance < 21*/simpletravel)
        //{
        //    simpletravel=false;
        //    transform.rotation = Quaternion.FromToRotation(transform.position, GameObject.Find("Player").transform.position);
        //    transform.position = (GameObject.Find("Player").transform.position - transform.position) * Time.deltaTime * 1;
        //}
        //else
        //{
        //}
        //base.Movement();
    }
}
