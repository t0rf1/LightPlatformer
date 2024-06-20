using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] GameObject popupWindow;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string colliderTag = collision.gameObject.tag;
        switch (colliderTag)
        {
            case "Player":
                popupWindow.SetActive(true);
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        string colliderTag = collision.gameObject.tag;
        switch (colliderTag)
        {
            case "Player":
                popupWindow.SetActive(false);
                break;
        }
    }
}
