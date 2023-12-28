using UnityEngine;

public class CameraChangeController : MonoBehaviour
{
    public GameObject mainCameraTarget;

    Vector3 mainCameraLocation;
    Vector3 mainCameraTargetLocation;

    private bool cameraState;

    public void MoveCamera(bool pressed)
    {
        cameraState = pressed;

        mainCameraLocation = transform.position;
        mainCameraTargetLocation = mainCameraTarget.transform.position;

        if (!cameraState)
        {
            transform.position = new Vector3(mainCameraTargetLocation.x, mainCameraTargetLocation.y, mainCameraTargetLocation.z) ;
            mainCameraTarget.transform.position = new Vector3(mainCameraLocation.x, mainCameraLocation.y, mainCameraLocation.z);
        }
    }
}
