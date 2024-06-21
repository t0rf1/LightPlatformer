using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class RandomAudioPlayer : MonoBehaviour
{
    public AudioSource audioSrc;
    public List<AudioClip> clipList1;
    public List<AudioClip> clipList2;

    bool canPlay = true;

    DieStopper dieStopper;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        dieStopper = GetComponent<DieStopper>();
    }

    private void Update()
    {
        if (!dieStopper.canMove)
        {
            audioSrc.Stop();
        }
    }

    public void PlayRandomSound(List<AudioClip> list)
    {
        if (dieStopper.canMove)
        {
            AudioClip clipPlay = list[Random.Range(0, list.Count - 1)];
            audioSrc.PlayOneShot(clipPlay);
        }
    }


    public void PlaySound(List<AudioClip> list)
    {
        audioSrc.PlayOneShot(list[0]);
    }




    public void PlayRandomSoundAtRandomTime(List<AudioClip> list, float minTime, float maxTime)
    {
        if (canPlay && dieStopper.canMove)
        {
            float interval = Random.Range(minTime, maxTime);
            StartCoroutine(PerformActionRandomly(list, interval));
            canPlay = false;
        }
    }

    IEnumerator PerformActionRandomly(List<AudioClip> list, float interval)
    {
        yield return new WaitForSeconds(interval);

        PlayRandomSound(list);
        canPlay = true;
    }



    //-----------------------------------------------------------------------------------------

    public void LoopCLip(List<AudioClip> list)
    {
        if (canPlay && dieStopper.canMove)
        {
            StartCoroutine(LoopClipCoroutine(list));
            canPlay = false;
        }
    }

    IEnumerator LoopClipCoroutine(List<AudioClip> list)
    {

        audioSrc.PlayOneShot(list[0]);

        yield return new WaitForSeconds(list[0].length);
        canPlay = true;
    }
}
