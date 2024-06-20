using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioPlayer : MonoBehaviour
{
    public AudioSource audioSrc;
    public List<AudioClip> clipList1;
    public List<AudioClip> clipList2;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    public void PlayRandomSound(List<AudioClip> list)
    {
        AudioClip clipPlay = list[Random.Range(0, list.Count)];
        audioSrc.PlayOneShot(clipPlay);
    }

    public void LoopPlayToggle(int condition)
    {
        if (condition != 0)
        {
            if(!audioSrc.isPlaying)
            {
                Debug.Log("started playing");
                audioSrc.Play();
            }
        }
        else
        {
            Debug.Log("Stopped");
            audioSrc.Stop();
        }
    }

    public void PlaySoundRepeteadly(List<AudioClip> list, float interval)
    {
        StartCoroutine(PerformActionRepeatedly(list ,interval));
    }

    IEnumerator PerformActionRepeatedly(List<AudioClip> list, float interval)
    {
        while (true)
        {
            
            PlayRandomSound(list);

            // Odczekaj "interval" sekund przed ponownym wykonaniem akcji
            yield return new WaitForSeconds(interval);
        }
    }
}
