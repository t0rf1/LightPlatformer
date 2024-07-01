using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputChecker : MonoBehaviour
{
    [SerializeField] GameObject firstSelected;

    public InputMode currentInputMode;

    public enum InputMode
    {
        MouseAndKeyboard,
        Gamepad
    }

    private void Start()
    {
    }

    private void Update()
    {
        // Check for keyboard or mouse input
        if (Keyboard.current.anyKey.wasPressedThisFrame || MouseInputCheck())
        {
            SetMouseAndKeyboardMode();
        }

        // Sprawdzamy, czy przycisk na padzie zosta³ naciœniêty
        if (Gamepad.current != null)
        {
            if (Gamepad.current.allControls.Any(control => control is ButtonControl button && button.wasPressedThisFrame))
            {
                SetGamepadMode();
            }
        }
    }

    private bool MouseInputCheck()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame ||
            Mouse.current.rightButton.wasPressedThisFrame ||
            Mouse.current.middleButton.wasPressedThisFrame)
        {
            return true;
        }

        // Check for mouse movement
        else if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            return true;
        }

        // Check for scroll wheel input
        else if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            return true;
        }

        else { return false; }
    }

    void SetMouseAndKeyboardMode()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        EventSystem.current.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        currentInputMode = InputMode.MouseAndKeyboard;
    }

    void SetGamepadMode()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (Cursor.visible)
        {
            EventSystem.current.SetSelectedGameObject(firstSelected);
        }

        Cursor.visible = false;
        currentInputMode = InputMode.Gamepad;
    }
}