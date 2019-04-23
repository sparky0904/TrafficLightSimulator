﻿using UnityEngine;

public class CarController : MonoBehaviour
{
    public float Speed = 10f;
    public float MaxSpeed = 10f;

    public float DefaultAccelerationRate = 0.5f;
    public float DefaultBrakeRate = 0.4f;
    public float FastBrakeRate = 0.6f;

    public float AccelerationRate;
    public float BrakeRate;

    private Rigidbody m_RB;
    private CarStatus m_carStatus;
    private float m_TargetSpeed;

    private enum CarStatus
    {
        Stopped,
        Accelerating,
        SlowingDown,
        MaxSpeed
    }

    // Use this for initialization
    private void Start()
    {
        m_RB = GetComponent<Rigidbody>();
        m_carStatus = CarStatus.Accelerating;
        m_TargetSpeed = MaxSpeed;

        AccelerationRate = DefaultAccelerationRate;
        BrakeRate = DefaultBrakeRate;
    }

    // Update is called once per frame
    private void Update()
    {
        // Set the speed based on the status of the car
        switch (m_carStatus)
        {
            case CarStatus.Stopped:
                BrakeRate = DefaultBrakeRate;
                break;

            case CarStatus.Accelerating:
                Speed = Mathf.Lerp(Speed, m_TargetSpeed, Time.deltaTime * AccelerationRate);
                break;

            case CarStatus.SlowingDown:
                Speed = Mathf.Lerp(Speed, m_TargetSpeed, Time.deltaTime * BrakeRate);
                break;

            case CarStatus.MaxSpeed:
                AccelerationRate = DefaultAccelerationRate;
                break;

            default:
                break;
        }

        // Check if were at max speed
        if (Speed >= (MaxSpeed * 0.99f))
        {
            m_carStatus = CarStatus.MaxSpeed;
            Speed = MaxSpeed;
        }
        // Move the car, we use move position to keep the physics checks inside unity
        m_RB.MovePosition(transform.position + transform.forward * Time.deltaTime * Speed);
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
        m_carStatus = CarStatus.Accelerating;
        m_TargetSpeed = MaxSpeed;
    }

    private void HandleTriggerEvent(Collider other)
    {
        // decide what to do depending on what we have collided with
        switch (other.transform.tag)
        {
            case "TrafficLight":
                // Debug.Log("Hit a traffic light..");
                TrafficLightController theTLC = other.transform.GetComponent<TrafficLightController>();
                RespondToTrafficLight(theTLC.GetTrafficLightStatus());
                break;

            default:
                break;
        }
    }

    private void RespondToTrafficLight(TrafficLightController.TrafficLightState theState)
    {
        // Decide on what to depending on the status
        switch (theState)
        {
            case TrafficLightController.TrafficLightState.Green:
                m_carStatus = CarStatus.Accelerating;
                m_TargetSpeed = MaxSpeed;
                AccelerationRate = DefaultAccelerationRate;
                // Debug.Log("The traffic light is Green");
                break;

            case TrafficLightController.TrafficLightState.Red:
                m_carStatus = CarStatus.SlowingDown;
                m_TargetSpeed = 0;
                BrakeRate = FastBrakeRate;
                //  Debug.Log("The traffic light is Red");
                break;

            case TrafficLightController.TrafficLightState.Amber:
                m_carStatus = CarStatus.SlowingDown;
                BrakeRate = DefaultBrakeRate;
                m_TargetSpeed = MaxSpeed * .2f;
                // Debug.Log("The traffic light is Amber");
                break;

            default:
                break;
        }
    }
}