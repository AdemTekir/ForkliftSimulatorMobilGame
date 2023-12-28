using UnityEngine;

public class SteeringWheelModelController : MonoBehaviour
{
    public Transform steeringWheelUI;
    public Transform steeringWheelModel;
    public Transform forklift;

    void Update()
    {
        float forkliftRotationX = forklift.rotation.eulerAngles.x;
        float forkliftRotationY = forklift.rotation.eulerAngles.y;
        float steeringWheelUIRotationZ = steeringWheelUI.rotation.eulerAngles.z;

        steeringWheelModel.rotation = Quaternion.Euler(forkliftRotationX, forkliftRotationY, steeringWheelUIRotationZ);
    }
}
