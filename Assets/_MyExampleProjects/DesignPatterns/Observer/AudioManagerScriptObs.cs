using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScriptObs : MonoBehaviour
{
    public AudioClip ballHitAudioClip;

    void OnEnable()
    {
        BallScript.OnBallHit += PlayBallHitAudio;
    }

    private void OnDisable()
    {
        BallScript.OnBallHit -= PlayBallHitAudio;
    }

    #region handlers

    private void PlayBallHitAudio(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(ballHitAudioClip, position);
    }
    #endregion
}
