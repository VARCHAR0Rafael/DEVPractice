using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    //This is to avoid halth bar rotation.
    public Transform cam;
    void LatedUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
