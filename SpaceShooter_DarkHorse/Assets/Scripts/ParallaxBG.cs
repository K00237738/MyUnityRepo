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
        transform.position = transform.position - Vector3.right * Time.deltaTime * speed;
        if (transform.position.x <= -218.0f)
        {
            transform.position = originalPosition;
        }
    }
}
