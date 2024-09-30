using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    
    //make ui text face camera
    void Update()
    {
        this.transform.LookAt(Camera.main.transform);
    }
}
