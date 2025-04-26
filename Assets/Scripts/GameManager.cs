using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using GlobalNamespace;

class GameManager : MonoBehaviour
{

    // Menu Buttons
    public void StartGame()
    {
        
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Options()
    {
        Debug.Log("Options clicked, not implemented yet.");
    }
}