using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightAttack : MonoBehaviour
{
    [SerializeField] GameObject light;

    private PolygonCollider2D collider2d;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Attacked");
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
    }
}
