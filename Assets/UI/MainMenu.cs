using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
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
            if (!NetworkManager.Singleton.IsClient &&  !NetworkManager.Singleton.IsServer)
            {
                NetworkManager.Singleton.StartHost();
            }
            CloseMenu();
        };
        buttonClient.clicked += () => {
            if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            {
                NetworkManager.Singleton.StartClient();
            }
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
