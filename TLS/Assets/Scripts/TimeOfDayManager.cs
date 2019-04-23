using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeOfDayManager : MonoBehaviour
{
    public Text TimeOfDayText;
    private DateTime m_TimeOfDay;
    private float m_NextSecondTick;

    // Use this for initialization
    private void Start()
    {
        m_TimeOfDay = new DateTime(2019, 04, 23, 23, 59, 56);
        m_NextSecondTick = Time.fixedTime + 1;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.fixedTime > m_NextSecondTick)
        {
            // Debug.Log(Time.fixedTime.ToString());
            m_TimeOfDay = m_TimeOfDay.AddSeconds(1d);
            m_NextSecondTick = Time.fixedTime + 1f;
        }

        TimeOfDayText.text = m_TimeOfDay.ToLongDateString() + " " + m_TimeOfDay.ToLongTimeString();
    }
}