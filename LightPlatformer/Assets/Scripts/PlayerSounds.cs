using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    RandomAudioPlayer AudioScript;

    private void Start()
    {
        AudioScript = GetComponent<RandomAudioPlayer>();
    }

    public void PlaySound(int which)
    {
        switch (which)
        {
            case 1:
                AudioScript.PlaySound(AudioScript.clipList1);
                break;
            case 2:
                AudioScript.PlaySound(AudioScript.clipList2);
                break;
        }
    }
}
