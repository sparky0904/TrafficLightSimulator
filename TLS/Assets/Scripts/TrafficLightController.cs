using System;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    public enum TrafficLightState
    {
        Green,
        Amber,
        Red,
        RedAmber,
    }

    public Material GreenMaterial, AmberMaterial, RedMaterial;
    public UnityEngine.UI.Text InfoPanel;
    public bool Selected = false;

    [SerializeField]
    private List<TrafficLightConfig> m_Config = new List<TrafficLightConfig>();

    [SerializeField]
    private TrafficLightState m_CurrentState;

    private MeshRenderer m_MeshRen;

    [SerializeField]
    private float m_currentTimer;

    [SerializeField]
    private float m_currentDelay;

    [SerializeField]
    private TrafficLightConfig m_currentConfig;

    // Use this for initialization
    private void Start()
    {
        // CurrentState = TrafficLightState.Red;
        m_MeshRen = GetComponent<MeshRenderer>();

        // m_Config.Add(new TrafficLightConfig());  // Create a default config line (is created by the default constructor)
        m_Config.Add(new TrafficLightConfig(new DateTime(1900, 1, 1, 00, 00, 00), new DateTime(1900, 1, 1, 00, 05, 00), 5, 3, 10, 5));
        m_Config.Add(new TrafficLightConfig(new DateTime(1900, 1, 1, 00, 06, 00), new DateTime(1900, 1, 1, 00, 10, 00), 5, 3, 10, 5));
        m_Config.Add(new TrafficLightConfig(new DateTime(1900, 1, 1, 00, 11, 00), new DateTime(1900, 1, 1, 23, 59, 00), 8, 5, 5, 8));

        m_currentConfig = m_Config[0]; // Set the config to the first one, will get updated on first update to the right config based on time of day
    }

    private void UpdateInforPanel()
    {
        if (Selected && InfoPanel != null)
        {
            string theText = "";

            theText = "ID: " + this.GetInstanceID().ToString() + " - " + this.name + "\n";
            theText += "Status: " + m_CurrentState.ToString() + "\n";
            theText += "Current Config: " + m_currentConfig.StartTime.ToString("HH:mm") + " - " + m_currentConfig.EndTime.ToString("HH:mm") + "\n";
            theText += String.Format(" Red: {0} \n RedAmber {1}\n Green {2}\n Amber {3}\n", m_currentConfig.RedDelay, m_currentConfig.RedAmberDelay, m_currentConfig.GreenDelay, m_currentConfig.AmberDelay);

            theText += "\nConfigs setup: \n";

            foreach (TrafficLightConfig item in m_Config)
            {
                theText += " - " + item.StartTime.ToString("HH:mm") + " - " + item.EndTime.ToString("HH:mm") + "\n";
            }

            theText += "\n";

            InfoPanel.text = theText;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // if selected then tell the info panel manager
        UpdateInforPanel();

        // Check if we need to change the status of the traffic light
        if (m_currentTimer > m_currentDelay)
        {
            // Reset the timer
            m_currentTimer = 0;

            // check if the config has changed due to the time of day
            CheckIfConfigChanged();

            // Set the current state of traffic light
            switch (m_CurrentState)
            {
                case TrafficLightState.Green:
                    m_CurrentState = TrafficLightState.Amber;
                    m_currentDelay = m_currentConfig.AmberDelay;
                    break;

                case TrafficLightState.Amber:
                    m_CurrentState = TrafficLightState.Red;
                    m_currentDelay = m_currentConfig.RedDelay;
                    break;

                case TrafficLightState.Red:
                    m_CurrentState = TrafficLightState.RedAmber;
                    m_currentDelay = m_currentConfig.RedAmberDelay;
                    break;

                case TrafficLightState.RedAmber:
                    m_CurrentState = TrafficLightState.Green;
                    m_currentDelay = m_currentConfig.GreenDelay;
                    break;

                default:
                    break;
            }

            // Set the colour depending on the status
            SetColourOfTrafficLight();
        }

        m_currentTimer += Time.deltaTime;
    }

    private void CheckIfConfigChanged()
    {
        if (GetTimestamp(TimeOfDayManager.TimeOfDay) > GetTimestamp(m_currentConfig.EndTime))
        {
            Debug.Log("We have gone past end of the current config time");
        }
    }

    private DateTime GetTimestamp(DateTime theTimestamp)
    {
        DateTime mTimestamp;

        // We always return the year/month/day from time of day manager as were only interesd inthe HH:MM for comparison
        mTimestamp = new DateTime(TimeOfDayManager.TimeOfDay.Year, TimeOfDayManager.TimeOfDay.Month, TimeOfDayManager.TimeOfDay.Day, theTimestamp.Hour, theTimestamp.Minute, 0);

        return mTimestamp;
    }

    private void SetColourOfTrafficLight()
    {
        // Set the colour depending on the status
        switch (m_CurrentState)
        {
            case TrafficLightState.Green:
                m_MeshRen.material = GreenMaterial;
                break;

            case TrafficLightState.Red:
                m_MeshRen.material = RedMaterial;
                break;

            case TrafficLightState.Amber:
                m_MeshRen.material = AmberMaterial;
                break;

            default:
                break;
        }
    }

    #region Public Methods

    public void SetTrafficLightStatus(TrafficLightState NewState)
    {
        m_CurrentState = NewState;
    }

    public TrafficLightState GetTrafficLightStatus()
    {
        return m_CurrentState;
    }

    #endregion Public Methods
}