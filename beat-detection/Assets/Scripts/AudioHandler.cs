using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(AudioSource))]
public class AudioHandler : MonoBehaviour
{
    AudioSource _audioSource;
    public float[] _clone = new float[512];
    float[] _frequencyBand = new float[8];
    float[] _bufferBand = new float[8];
    float[] _bufferDecrease = new float[8];
    
    float[] _freqBandHighest = new float[8];
    public static float[] _audioBand = new float[8];
    public static float[] _audioBandBuffer = new float[8];
    
    // Change _Amplitude and _AmplitudeBuffer to floats, not arrays
    public static float _Amplitude, _AmplitudeBuffer;  
    float _AmplitudeHighest;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBand(); // Update frequency bands
        BandBuffer();
        CreateAudioBands();
        GetAmplitude(); // Get amplitude values
    }

    void GetAmplitude()
    {
        float _CurrentAmplitude = 0f;
        float _CurrentAmplitudeBuffer = 0f;
        for (int i = 0; i < 8; i++)
        {
            _CurrentAmplitude += _audioBand[i];
            _CurrentAmplitudeBuffer += _audioBandBuffer[i];
        }

        if (_CurrentAmplitude > _AmplitudeHighest)
        {
            _AmplitudeHighest = _CurrentAmplitude;
        }

        // Assign the calculated values to _Amplitude and _AmplitudeBuffer
        _Amplitude = _CurrentAmplitude / _AmplitudeHighest;
        _AmplitudeBuffer = _CurrentAmplitudeBuffer / _AmplitudeHighest;
    }

    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_frequencyBand[i] > _freqBandHighest[i])
            {
                _freqBandHighest[i] = _frequencyBand[i];
            }

            // Use individual float values for the audio bands and buffer
            _audioBand[i] = (_frequencyBand[i] / _freqBandHighest[i]);
            _audioBandBuffer[i] = (_bufferBand[i] / _freqBandHighest[i]);
        }
    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_clone, 0, FFTWindow.Blackman);
    }

    void BandBuffer()
    {
        for (int g = 0; g < 8; ++g)
        {
            if (_frequencyBand[g] > _bufferBand[g])
            {
                _bufferBand[g] = _frequencyBand[g];
                _bufferDecrease[g] = 0.005f;
            }
            if (_frequencyBand[g] < _bufferBand[g])
            {
                _bufferBand[g] -= _bufferDecrease[g];
                _bufferDecrease[g] *= 1.2f;
            }
        }
    }

    void MakeFrequencyBand()
    {
        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                if (count < _clone.Length) // Prevent out-of-bounds access
                {
                    average += _clone[count] * (count + 1);
                    count++;
                }
            }

            average /= sampleCount; // Divide by the number of samples, not count
            _frequencyBand[i] = average * 10;
        }
    }
}

