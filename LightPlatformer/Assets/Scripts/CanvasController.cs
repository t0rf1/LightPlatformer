using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject gameUI;

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
        gameUI.SetActive(state == gameState.Game);
        deathScreen.SetActive(state == gameState.DeathScreen);
    }

    public void GoToScene(int sceneNumber)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneNumber);
    }

    public void CloseApplication()
    {
        Application.Quit();
    }
}
