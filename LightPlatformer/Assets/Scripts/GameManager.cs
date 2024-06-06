using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public gameState State;

    public static event Action<gameState> OnGameStateChanged;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(gameState.Game);
    }

    public void UpdateGameState(gameState newState)
    {
        State = newState;

        switch (State)
        {
            case gameState.Game:
                break;
            case gameState.DeathScreen:
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }
}
public enum gameState
{
    Game,
    DeathScreen
}
