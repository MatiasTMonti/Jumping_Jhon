using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlollow : MonoBehaviour
{
    [SerializeField] private Transform doodlePos;

    void LateUpdate()
    {
        if (doodlePos.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, doodlePos.position.y - 1, transform.position.z);
        } 
    }
}
