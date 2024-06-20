using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieStopper : MonoBehaviour
{
    public bool canMove = true;
    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(gameState state)
    {
        canMove = (state == gameState.Game);
    }
}
