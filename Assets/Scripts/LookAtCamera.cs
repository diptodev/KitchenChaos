using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
     private enum Mode
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraInverted
        
    }
    [SerializeField] private Mode mode;
    private void Update()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                Vector3 lookAtDir = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + lookAtDir);
                break;

            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraInverted:
                transform.forward = -Camera.main.transform.forward;
                break;

        }
    }

}
