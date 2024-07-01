using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class FlashlightController : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 mousePosition;

    public Light2D spotlight;

    DieStopper dieStopper;

    GameObject inputChecker_obj;

    Vector2 thumbstickPosition;
    float thumbstickRotation;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        dieStopper = GetComponent<DieStopper>();
        inputChecker_obj = GameObject.Find("InputChecker");
        
    }

    void Update()
    {
        if (dieStopper.canMove)
        {
            switch (inputChecker_obj.GetComponent<InputChecker>().currentInputMode)
            {
                case InputChecker.InputMode.MouseAndKeyboard:
                    RotateToMouse();
                    break;
                case InputChecker.InputMode.Gamepad:
                    break;
            }

        }
    }

    public void Look(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            thumbstickPosition = context.ReadValue<Vector2>();
            thumbstickRotation = Mathf.Atan2(thumbstickPosition.x, thumbstickPosition.y);

            Debug.Log((thumbstickRotation * Mathf.Rad2Deg - 90)*-1);

            transform.rotation = Quaternion.Euler(0, 0, (thumbstickRotation * Mathf.Rad2Deg - 90)*-1);
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
