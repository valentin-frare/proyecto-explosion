using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraInit : MonoBehaviour
{
    public static CameraInit instance;

    private Transform referenceObject;
    private Transform player;
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Awake() {
        instance = this;

        referenceObject = new GameObject("CamReference").transform;
        GameEvents.OnPlayerSpawn += OnPlayerSpawn;
    }

    void OnPlayerSpawn(GameObject player)
    {
        this.player = player.transform;

        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();

        cinemachineVirtualCamera.Follow = referenceObject;
    }

    void LateUpdate()
    {
        if (player == null) return;

        referenceObject.position = player.position.ooZ();
    }

    public void Shake()
    {
        StartCoroutine(_ProcessShake());
    }

    private IEnumerator _ProcessShake(float shakeIntensity = 5f, float shakeTiming = 0.5f)
    {
        Noise(1, shakeIntensity);
        yield return new WaitForSeconds(shakeTiming);
        Noise(0, 0);
    }

    public void Noise(float amplitudeGain, float frequencyGain)
    {
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitudeGain;
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitudeGain;
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitudeGain;
            
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequencyGain;
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequencyGain;
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequencyGain;      
    }
}
