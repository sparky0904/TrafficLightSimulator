using UnityEngine;

public class CarController : MonoBehaviour
{
    public float Speed = 10f;
    public float MaxSpeed = 10f;

    public float DefaultAccelerationRate = 0.5f;
    public float DefaultBrakeRate = 0.4f;
    public float FastBrakeRate = 0.6f;

    public float AccelerationRate;
    public float BrakeRate;

    private Rigidbody RB;
    private CarStatus carStatus;

    private enum CarStatus
    {
        Stopped,
        Accelerating,
        Stopping,
        MaxSpeed
    }

    // Use this for initialization
    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        carStatus = CarStatus.Accelerating;

        AccelerationRate = DefaultAccelerationRate;
        BrakeRate = DefaultBrakeRate;
    }

    // Update is called once per frame
    private void Update()
    {
        switch (carStatus)
        {
            case CarStatus.Stopped:
                BrakeRate = DefaultBrakeRate;
                break;

            case CarStatus.Accelerating:
                Speed = Mathf.Lerp(Speed, MaxSpeed, Time.deltaTime * AccelerationRate);
                break;

            case CarStatus.Stopping:
                Speed = Mathf.Lerp(Speed, 0, Time.deltaTime * BrakeRate);
                break;

            case CarStatus.MaxSpeed:
                AccelerationRate = DefaultAccelerationRate;
                break;

            default:
                break;
        }
        RB.MovePosition(transform.position + transform.forward * Time.deltaTime * Speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleTriggerEvent(other);
    }

    private void OnTriggerStay(Collider other)
    {
        HandleTriggerEvent(other);
    }

    private void OnTriggerExit(Collider other)
    {
        carStatus = CarStatus.Accelerating;
    }

    private void HandleTriggerEvent(Collider other)
    {
        // decide what to do depending on what we have collided with
        switch (other.transform.tag)
        {
            case "TrafficLight":
                Debug.Log("Hit a traffic light..");
                TrafficLightController tlc = other.transform.GetComponent<TrafficLightController>();
                RespondToTrafficLight(tlc.GetTrafficLightStatus());
                break;

            default:
                break;
        }
    }

    private void RespondToTrafficLight(TrafficLightController.TrafficLightState currentState)
    {
        // Decide on what to depending on the status
        switch (currentState)
        {
            case TrafficLightController.TrafficLightState.Green:
                carStatus = CarStatus.Accelerating;
                AccelerationRate = DefaultAccelerationRate;
                Debug.Log("The traffic light is Green");
                break;

            case TrafficLightController.TrafficLightState.Red:
                carStatus = CarStatus.Stopping;
                BrakeRate = FastBrakeRate;
                Debug.Log("The traffic light is Red");
                break;

            case TrafficLightController.TrafficLightState.Amber:
                carStatus = CarStatus.Stopping;
                BrakeRate = DefaultBrakeRate;
                Debug.Log("The traffic light is Amber");
                break;

            default:
                break;
        }
    }
}