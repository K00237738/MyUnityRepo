using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    private Vector3 originalPosition;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position - Vector3.forward * Time.deltaTime * speed;
        if (transform.position.z <= -100.0f)
        {
            transform.position = originalPosition;
        }
    }
}
