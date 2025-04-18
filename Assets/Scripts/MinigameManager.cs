using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using GlobalNamespace;
class MinigameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] minigamePrefabs; // Array of minigame prefabs
    private GameObject currentMinigame; // The currently active minigame
    GlobalNamespace.Resources currentResource { public get; private set; }; // The resource currently being used in the minigame

    public void IronMinigame(){
        // Load the Iron minigame prefab from the Resources folder
        GameObject ironMinigamePrefab = Resources.Load<GameObject>("IronMinigamePrefab");
        if (ironMinigamePrefab != null)
        {
            // Instantiate the minigame and set it as the current one
            currentMinigame = Instantiate(ironMinigamePrefab);
            currentMinigame.SetActive(true);
            currentResource = GlobalNamespace.Resources.Iron; // Set the current resource to Iron
        }
        else
        {
            Debug.LogError("Iron minigame prefab not found in Resources folder.");
        }
    }

    public int MinigameEnd()
    {
        // Destroy the current minigame object
        if (currentMinigame != null)
        {
            Destroy(currentMinigame);
            currentMinigame = null;
        }

        // Return the resource used in the minigame
        return 600;
    }
}