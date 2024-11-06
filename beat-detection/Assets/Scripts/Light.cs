using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Light))] // Ensure a Light component is attached
public class LightController : MonoBehaviour
{
    public int _band;            // The frequency band to control light intensity
    public float _minIntensity, _maxIntensity; // Min and max intensity values for the light

    private Light _light;

    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();  // Get the Light component attached to the GameObject
    }

    // Update is called once per frame
    void Update()
    {
        if (_light != null)
        {
            // Ensure the band value is within a valid range
            float bandValue = AudioHandler._audioBandBuffer[_band];

            // Calculate the light intensity based on the frequency band value
            _light.intensity = Mathf.Lerp(_minIntensity, _maxIntensity, bandValue);
        }
    }
}
