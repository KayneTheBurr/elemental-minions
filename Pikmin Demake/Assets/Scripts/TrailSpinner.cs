using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSpinner : MonoBehaviour
{
    public float degreesPerSecond;

    //rotate to create objective marker trail
    void Update()
    {
        this.transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
    }
}
