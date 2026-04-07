using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    public float moveSpeed = 5f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, -moveSpeed * Time.deltaTime);

        if (transform.position.z < -20f)
        {
            Destroy(gameObject);
        }
    }
}
