using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR;

public class MoveScript : MonoBehaviour
{

    [SerializeField]
    private int velocity = 10;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Move()
    {
        var resultDir = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            resultDir.z = 1 * velocity;
        } else if (Input.GetKey(KeyCode.S))
        {
            resultDir.z = -1 * velocity;
        }

        if (Input.GetKey(KeyCode.D))
        {
            resultDir.x = 1 * velocity;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            resultDir.x = -1 * velocity;
        }
        resultDir.y = rb.velocity.y;

        rb.velocity = resultDir;

    }
}
