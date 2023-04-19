using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class bubble : MonoBehaviour
{
    public float x_offset = 0.0f;
    public float x_amplitude = 10.0f;
    public float x_frequentie = 10.0f;
    public float x_phase = 0.0f;

    void Update()
    {
        float x_pos = x_offset + x_amplitude * Mathf.Sin(Time.time * x_frequentie + x_phase);
        transform.position += (new Vector3(x_pos, 10) - transform.position).normalized * Time.deltaTime * 1.5f;
    }
}
