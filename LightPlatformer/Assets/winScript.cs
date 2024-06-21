using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winScript : MonoBehaviour
{
    public void GoToScene(int sceneNumber)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneNumber);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string colliderTag = collision.gameObject.tag;
        switch (colliderTag)
        {
            case "Player":
                GoToScene(2);
                break;
        }
    }
}
