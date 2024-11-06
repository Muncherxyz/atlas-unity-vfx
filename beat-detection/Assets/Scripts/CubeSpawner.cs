using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject _sampleCubePrefab;
    GameObject[] _sampleCube = new GameObject[512];
    public float _maxScale;
    private AudioHandler audioHandler;

    void Start()
    {
        audioHandler = FindObjectOfType<AudioHandler>(); // Find the AudioHandler in the scene
        for (int i = 0; i < 512; i++)
        {
            GameObject _instanceSampleCube = Instantiate(_sampleCubePrefab);
            _instanceSampleCube.transform.position = this.transform.position;
            _instanceSampleCube.transform.parent = this.transform;
            _instanceSampleCube.name = "SampleCube" + i;
            this.transform.eulerAngles = new Vector3(0, -0.703125f * i, 0);
            _instanceSampleCube.transform.position = Vector3.forward * 100;
            _sampleCube[i] = _instanceSampleCube;
        }
    }

    void Update()
    {
        for (int i = 0; i < 512; i++)
        {
            if (_sampleCube[i] != null) // Check if the cube is not null
            {
                _sampleCube[i].transform.localScale = new Vector3(.5f, (audioHandler._clone[i] * _maxScale) + 2, .5f);
            }
        }
    }
}
