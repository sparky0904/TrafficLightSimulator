using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeOfDayManager : MonoBehaviour
{
    public Text TimeOfDayText;
    private static DateTime m_TimeOfDay;
    private float m_NextSecondTick;

    public static DateTime TimeOfDay
    {
        get
        {
            return m_TimeOfDay;
        }

        set
        {
            m_TimeOfDay = value;
        }
    }

    // Use this for initialization
    private void Start()
    {
        m_TimeOfDay = new DateTime(2019, 04, 23, 23, 55, 00);
        m_NextSecondTick = Time.fixedTime + 1;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.fixedTime > m_NextSecondTick)
        {
            // Debug.Log(Time.fixedTime.ToString());
            m_TimeOfDay = m_TimeOfDay.AddMinutes(1);
            m_NextSecondTick = Time.fixedTime + 1f;
        }

        // TimeOfDayText.text = m_TimeOfDay.ToLongDateString() + " " + m_TimeOfDay.ToString("HH:mm");
        TimeOfDayText.text = m_TimeOfDay.ToString("ddd, d MMM HH:mm");
    }
}