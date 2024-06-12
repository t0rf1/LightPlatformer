using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AudioTester : MonoBehaviour
{
    RandomAudioPlayer AudioScript;

    void Start()
    {
        AudioScript = GetComponent<RandomAudioPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        objectToMouse();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AudioScript.PlayRandomSound();
        }
    }

    void objectToMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0; // Ustaw z na 0, aby obiekt pozosta≥ w p≥aszczyünie 2D
        transform.position = mousePosition;
    }

}


