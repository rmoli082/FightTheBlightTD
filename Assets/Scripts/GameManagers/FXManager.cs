using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    AudioSource player;
    bool canPlay = true;
    bool bossPlay = true;

    private void Awake()
    {
        player = GetComponent<AudioSource>();
    }
    public void PlayAudio(AudioClip sound)
    {
        if (canPlay)
        {
            player.PlayOneShot(sound);
            canPlay = false;
            StartCoroutine(Resetter());
        }
    }

    public void PlayBossAudio(AudioClip sound)
    {
        if (bossPlay)
        {
            player.PlayOneShot(sound);
            bossPlay = false;
            StartCoroutine(BossResetter());
        }
    }

    private IEnumerator Resetter()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        canPlay = true;
    }

    private IEnumerator BossResetter()
    {
        yield return new WaitForSecondsRealtime(3.0f);
    }
}
