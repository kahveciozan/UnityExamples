using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TestingCinemachine : MonoBehaviour
{
    private void Awake()
    {
        CinemachineVirtualCamera cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineVirtualCamera.m_Lens.FieldOfView = 10;
        cinemachineVirtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraSide = 0;

        Volume volume = GetComponent<Volume>();
        volume.profile.TryGet<Bloom>(out Bloom bloom);
        bloom.intensity.SetValue(new MinFloatParameter(10f, 10f));
    }
}
