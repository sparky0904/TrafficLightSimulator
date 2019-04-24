using System;
using UnityEngine;

public class TrafficLightConfig : MonoBehaviour
{
    private DateTime m_StartTime, m_EndTime;

    private float m_RedDelay, m_RedAmberDelay, m_GreenDelay, m_AmberDelay;

    #region PublicEncapsulatedFields

    public float RedDelay
    {
        get
        {
            return m_RedDelay;
        }

        set
        {
            m_RedDelay = value;
        }
    }

    public float RedAmberDelay
    {
        get
        {
            return m_RedAmberDelay;
        }

        set
        {
            m_RedAmberDelay = value;
        }
    }

    public float GreenDelay
    {
        get
        {
            return m_GreenDelay;
        }

        set
        {
            m_GreenDelay = value;
        }
    }

    public float AmberDelay
    {
        get
        {
            return m_AmberDelay;
        }

        set
        {
            m_AmberDelay = value;
        }
    }

    public DateTime StartTime
    {
        get
        {
            return m_StartTime;
        }

        set
        {
            m_StartTime = value;
        }
    }

    public DateTime EndTime
    {
        get
        {
            return m_EndTime;
        }

        set
        {
            m_EndTime = value;
        }
    }

    #endregion PublicEncapsulatedFields

    public TrafficLightConfig(DateTime theStartTime, DateTime theEndTime, float theRedDelay, float theRedAmberDelay, float theGreenDelay, float theAmberDelay)
    {
        StartTime = theStartTime;
        EndTime = theEndTime;
        RedDelay = theRedDelay;
        RedAmberDelay = theRedAmberDelay;
        GreenDelay = theGreenDelay;
        AmberDelay = theAmberDelay;
    }

    public TrafficLightConfig()
    {
        m_StartTime = new DateTime(1900, 1, 1, 00, 00, 00);
        m_EndTime = new DateTime(1900, 1, 1, 23, 59, 00);

        m_RedDelay = 5;
        m_RedAmberDelay = 3;
        m_GreenDelay = 5;
        m_AmberDelay = 3;
    }

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}