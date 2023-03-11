using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button buttonHost = root.Q<Button>("ButtonHost");
        Button buttonClient = root.Q<Button>("ButtonClient");
        Button buttonExit = root.Q<Button>("ButtonExit");


        buttonHost.clicked += () => { 
            Debug.Log("buttonHost");
            CloseMenu();
        };
        buttonClient.clicked += () => { 
            Debug.Log("buttonClient");
            CloseMenu();
        };
        buttonExit.clicked += () => { 
            Application.Quit();
            CloseMenu();
        };
    }

    private void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}
