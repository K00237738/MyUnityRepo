using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceDebri : Projectile
{
    // Update is called once per frame
    protected override void Update()
    {
        transform.Rotate(new Vector3(0, 15, 0)*Time.deltaTime*(speed/2));
        base.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
