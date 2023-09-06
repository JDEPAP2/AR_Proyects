using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
public class ChangeCamera : MonoBehaviour
{
    public ARCameraManager a;
    public void ChngCamera()
    {
        a.requestedFacingDirection = a.currentFacingDirection == CameraFacingDirection.World? CameraFacingDirection.User: CameraFacingDirection.World;
    }
}
