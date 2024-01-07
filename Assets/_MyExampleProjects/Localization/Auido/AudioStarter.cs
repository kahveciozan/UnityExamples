using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStarter : MonoBehaviour
{
    public AudioSource soundFX;

    public void PlaySound()
    {
        soundFX.Play();
    }
 
}
