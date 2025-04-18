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
    private GameState currentState;
    private List<Kingdom> kingdoms = new List<Kingdom>();
    private Player player;

    void Start()
    {
        currentState = GameState.MainMenu;
    }
}