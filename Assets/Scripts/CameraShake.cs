using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    // Defines the camera
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    // Defines the noise profile
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
       // Defines the shake duration
    private float shakeDuration;
    // Defines the shake intensity - controls the amplitude gain
    private float shakeIntensity;

    private void Awake()
    {
        // Get the camera
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        // Get the noise profile
        cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        StopShake();
    }

    public void Shake(float shakeIntensity, float shakeDuration)
    {
        // Set the amplitude gain to the shake intensity
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = shakeIntensity;
        // Start coroutine
        StartCoroutine(WaitTime(shakeDuration));
    }

    IEnumerator WaitTime(float shakeDuration)
    {
        yield return new WaitForSeconds(shakeDuration);
        StopShake();
    }

    void StopShake()
    {
        // Set the amplitude gain to 0
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
    }

}
