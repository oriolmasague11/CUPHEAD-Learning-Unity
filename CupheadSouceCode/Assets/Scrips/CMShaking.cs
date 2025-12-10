using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMShaking : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    private float _timer;

    // Update is called once per frame
    void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;    // deltaTime es la diferencia de tiempo entre dos llamadas update (depende del timerate)
            if (_timer < 0)
            {
                cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
                cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
            }
        }
    }

    public void ScreenShake(float amplitude, float freq, float timer)
    {
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = freq;
        _timer = timer;
    }

}
