using UnityEngine;
using UnityEngine.UI;

public class ForkliftController : MonoBehaviour
{
    public WheelCollider frontRightWheel;
    public WheelCollider frontLeftWheel;
    public WheelCollider backRightWheel;
    public WheelCollider backLeftWheel;

    public Transform frontRightWheelTransform;
    public Transform frontLeftWheelTransform;
    public Transform backRightWheelTransform;
    public Transform backLeftWheelTransform;

    Rigidbody rb;

    public Vector3 centerOfMassPosition;
    public Vector3 defaultCenterOfMassPosition = Vector3.zero;

    public GameObject inGameMenu;
    public GameObject steeringWheelUI;
    public GameObject gearUI;
    public GameObject gearUI_D;
    public GameObject gearUI_R;
    public GameObject lift;
    public Slider currentFuelSlider;

    public AnimationCurve carAccelerationCurve;
    public AnimationCurve carSpeedCurve;
    Keyframe[] ks;

    public float maxEngineTorque;
    public float carSpeed;
    public float carSpeedHasChanged;
    public float acceleration;
    public float accelerationHasChanged;
    public float brakeForce;
    
    public float currentFuel;
    public float maxFuelSlider;
    
    private float currentAcceleration;
    private float currentBrakeForce;

    private float currentWheelTurnAngle;
    private float maxWheelTurnAngle = 40f;

    private bool liftUpState = false;
    private bool liftDownState = false;
    
    private bool gearState;
    
    private bool fuelState;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMassPosition;
        maxFuelSlider = currentFuel;

        InspectorAnimationCurve();
    }

    private void Update()
    {
        FuelCountDownSystem();
        carSpeed = rb.velocity.magnitude;
    }

    void FixedUpdate()
    {
        TurnWheel(true);
        TurnWheelRotation(frontRightWheel, frontRightWheelTransform);
        TurnWheelRotation(frontLeftWheel, frontLeftWheelTransform);
        TurnWheelRotation(backRightWheel, backRightWheelTransform);
        TurnWheelRotation(backLeftWheel, backLeftWheelTransform);

        if (liftUpState)
        {
            lift.transform.localPosition = new Vector3(lift.transform.localPosition.x,
                Mathf.Clamp(lift.transform.localPosition.y + Time.deltaTime, 2.2f, 9.0f), lift.transform.localPosition.z);
        }
        if (liftDownState)
        {
            lift.transform.localPosition = new Vector3(lift.transform.localPosition.x,
                Mathf.Clamp(lift.transform.localPosition.y - Time.deltaTime, 2.2f, 9.0f), lift.transform.localPosition.z);
        }
    }

    public void InspectorAnimationCurve()
    {
        ks = new Keyframe[1];
        for (var i = 0; i < ks.Length; i++)
        {
            ks[i] = new Keyframe(i, Mathf.Sin(i));
        }
        carSpeedCurve = new AnimationCurve(ks);
        carAccelerationCurve = new AnimationCurve(ks);
        accelerationHasChanged = acceleration;
        carSpeedHasChanged = carSpeed;
    }

    public void Acceleration(bool check)
    {
        if (!gearState)
        {
            if (check)
            {
                currentAcceleration = acceleration;
            }
            else
            {
                currentAcceleration = 0f;
            }
        }
        else
        {
            if (check)
            {
                currentAcceleration = -acceleration;
            }
            else
            {
                currentAcceleration = 0f;
            }
        }

        frontRightWheel.motorTorque = currentAcceleration;
        frontLeftWheel.motorTorque = currentAcceleration;
        backRightWheel.motorTorque = currentAcceleration;
        backLeftWheel.motorTorque = currentAcceleration;
    }

    public void BrakeForce(bool check)
    {
        if (check)
        {
            currentBrakeForce = brakeForce;
        }
        else
        {
            currentBrakeForce = 0f;
        }

        frontRightWheel.brakeTorque = currentBrakeForce * Time.deltaTime;
        frontLeftWheel.brakeTorque = currentBrakeForce * Time.deltaTime;
    }

    void TurnWheel(bool check)
    {
        currentWheelTurnAngle = maxWheelTurnAngle * Input.GetAxis("Horizontal");   //Editor test controller (A - D)
        //currentWheelTurnAngle = maxWheelTurnAngle * steeringWheelUI.GetComponent<SteeringWheelUIController>().GetClampedValue();
        backRightWheel.steerAngle = -currentWheelTurnAngle;
        backLeftWheel.steerAngle = -currentWheelTurnAngle;

        if (check)
        {
            currentWheelTurnAngle = maxWheelTurnAngle * Time.deltaTime;
        }
    }

    void TurnWheelRotation(WheelCollider wheelCollider, Transform transform)
    {
        wheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);

        transform.SetPositionAndRotation(position, rotation);
    }

    public void CheckGear_R(bool check)
    {
        if (!check && gearState == false)
        {
            gearState = true;

            gearUI.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (!check && gearState == true)
        {
            gearState = false;

            gearUI.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void LiftUp(bool pressed)
    {
        liftUpState = pressed;
    }

    public void LiftDown(bool pressed)
    {
        liftDownState = pressed;
    }

    void FuelCountDownSystem()
    {
        if (currentFuel > 0f)
        {
            if (currentFuelSlider.IsActive())
            {
                currentFuel -= 1f * Time.deltaTime;
                currentFuelSlider.value = currentFuel / maxFuelSlider;
                fuelState = true;
            }
        }
        else if (currentFuel <= 0f && fuelState == true)
        {
            inGameMenu.GetComponent<InGameMenuEdit>().GameLoseMenu("YAKITIN BÝTTÝ!");

            fuelState = false;
        }
    }
}
