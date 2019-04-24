using UnityEngine;
using UnityEngine.UI;

public class InfoPanelManager : MonoBehaviour
{
    public Text InfoPanelTextBox;

    private TrafficLightController m_selectedTrafficLightController = null;

    public TrafficLightController SelectedTrafficLightController
    {
        get
        {
            return m_selectedTrafficLightController;
        }

        set
        {
            m_selectedTrafficLightController = value;
        }
    }

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_selectedTrafficLightController != null)
        {
            TrafficLightController m_tlc = m_selectedTrafficLightController;

            InfoPanelTextBox.text = "ID: " + m_tlc.GetInstanceID().ToString() + "/n";
            InfoPanelTextBox.text += "testuy";
        }
    }
}