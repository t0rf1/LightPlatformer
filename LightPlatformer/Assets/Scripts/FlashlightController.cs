using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashlightController : MonoBehaviour
{
    bool canMove = true;
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


    private Camera mainCamera;
    private Vector3 mousePosition;

    public Light2D spotlight;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        if (canMove) {
            RotateToMouse();
        }
    }

    private void RotateToMouse()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePosition - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
