using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour
{
    public int _band;
    public float _startScale, _scaleMultiplier;
    public bool _useBuffer;
    Material _material;

    // Start is called before the first frame update
    void Start()
    {
        // No need to find AudioHandler since we're accessing a static field
        _material = GetComponent<MeshRenderer>().materials [0];
    }

    // Update is called once per frame
    void Update()
    {
        if (_useBuffer)
        {
            // Accessing the static _frequencyBand directly from the AudioHandler class
            transform.localScale = new Vector3(
                transform.localScale.x,
                (AudioHandler._audioBandBuffer[_band] * _scaleMultiplier) + _startScale,
                transform.localScale.z);
                Color _color = new Color (AudioHandler._audioBandBuffer[_band], AudioHandler._audioBandBuffer[_band], AudioHandler._audioBandBuffer[_band]);
                _material.SetColor("_EmissionColor", _color);
        }
        if (!_useBuffer)
        {
            // Accessing the static _frequencyBand directly from the AudioHandler class
            transform.localScale = new Vector3(
                transform.localScale.x,
                (AudioHandler._audioBand[_band] * _scaleMultiplier) + _startScale,
                transform.localScale.z);
                Color _color = new Color (AudioHandler._audioBandBuffer[_band], AudioHandler._audioBandBuffer[_band], AudioHandler._audioBandBuffer[_band]);
                _material.SetColor("_EmissionColor", _color);
        }
        

    }
}
