using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashlightController : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 mousePosition;

    public Light2D spotlight;

    DieStopper dieStopper;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        dieStopper = GetComponent<DieStopper>();
    }

    void Update()
    {
        if (dieStopper.canMove)
        {
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
