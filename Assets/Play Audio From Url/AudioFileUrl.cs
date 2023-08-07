using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AudioFileUrl : MonoBehaviour
{
    AudioSource audioSource;
    AudioClip myClip;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(GetAudioClip());
        Debug.Log("Starting to download the audio...");
    }

    IEnumerator GetAudioClip()
    {
        using (UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip("https://storage.googleapis.com/addressable-deneme/AudioSources/5%20minute%20guided%20meditation%20music%20relax%20mind%20body%2C%205%20minute%20meditation.mp3", AudioType.MPEG))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                myClip = DownloadHandlerAudioClip.GetContent(request);
                audioSource.clip = myClip;
                audioSource.Play();
                Debug.Log("Audio is playing.");
            }
        }
    }


    public void PauseAudio()
    {
        audioSource.Pause();
    }

    public void PlayAudio()
    {
        audioSource.Play();
    }

    public void StopAudio()
    {
        audioSource.Stop();

    }
}
