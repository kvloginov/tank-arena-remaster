using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR;

public class MoveScript : MonoBehaviour
{

    [SerializeField]
    private int _velocity = 10;

    [SerializeField]
    private Transform _tower;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        RotateTower();
    }



    void RotateTower()
    {
        Plane plane = new Plane(Vector3.up, 0);

        // Use Raycast to check where the mouse intersects with the plane
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var mouseWorldPosition = Vector3.zero;
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            // Use the intersection point as the projected position of the mouse on the plane
            Vector3 projectedMousePosition = ray.GetPoint(distance);
            mouseWorldPosition = projectedMousePosition;


            // Calculate the direction from the object's position to the mouse position
            Vector3 direction = Vector3.Normalize(mouseWorldPosition - _tower.position);

            // Create a rotation that points in the direction of the mouse
            Quaternion rotation = Quaternion.LookRotation(direction, _tower.up);

            // Apply the rotation to the object's transform

            _tower.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
            //_tower.rotation.SetEulerAngles(0, rotation.eulerAngles.y, 0);
        }

      
    }

    void Move()
    {
        var resultDir = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            resultDir.z = 1 * _velocity;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            resultDir.z = -1 * _velocity;
        }

        if (Input.GetKey(KeyCode.D))
        {
            resultDir.x = 1 * _velocity;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            resultDir.x = -1 * _velocity;
        }
        resultDir.y = _rb.velocity.y;

        _rb.velocity = resultDir;

    }
}
