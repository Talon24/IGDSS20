using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassRotation : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GetInstanceID());
        System.Random random = new System.Random(GetInstanceID());
        transform.Rotate(0, random.Next(0, 360), 0);
    }
}
