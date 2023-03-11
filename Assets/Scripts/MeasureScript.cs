using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureScript : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        Vector3 objectSize = Vector3.Scale(transform.localScale, this.GetComponent<MeshCollider>().bounds.size);
        Debug.Log(objectSize);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
