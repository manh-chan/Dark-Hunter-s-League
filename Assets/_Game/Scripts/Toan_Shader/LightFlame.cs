using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class LightFlame : MonoBehaviour
{
    public Light2D fireLight;  
    public float minIntensity = 1.5f;
    public float maxIntensity = 3.5f;
    public float flickerSpeed = 2f;

    void Update()
    {
        if (fireLight != null)
        {
            float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, 0.0f);
            fireLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
        }
    }
}
