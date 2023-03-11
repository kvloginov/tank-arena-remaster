using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotKeysHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject menuObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (menuObject.activeInHierarchy)
            {
                menuObject.SetActive(false);
            }
            else
            {
                menuObject.SetActive(true);
            }
        }
    }
}
