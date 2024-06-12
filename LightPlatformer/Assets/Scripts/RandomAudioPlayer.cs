using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioPlayer : MonoBehaviour
{
    public AudioSource audioSrc;
    public List<AudioClip> clipList;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    public void PlayRandomSound()
    {
        AudioClip clipPlay = clipList[Random.Range(0, clipList.Count)];
        Debug.Log(clipPlay);
        audioSrc.PlayOneShot(clipPlay);
    }
}
