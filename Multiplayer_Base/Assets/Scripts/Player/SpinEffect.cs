using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinEffect : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(Vector3.up, 1f);
    }
}
