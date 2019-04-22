using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    public enum TrafficLightState
    {
        Green,
        Red,
        Amber,
    }

    [SerializeField]
    private TrafficLightState CurrentState;

    private MeshRenderer MeshRen;

    public Material GreenMaterial, AmberMaterial, RedMaterial;

    // Use this for initialization
    private void Start()
    {
        // CurrentState = TrafficLightState.Red;
        MeshRen = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        switch (CurrentState)
        {
            case TrafficLightState.Green:
                MeshRen.material = GreenMaterial;
                break;

            case TrafficLightState.Red:
                MeshRen.material = RedMaterial;
                break;

            case TrafficLightState.Amber:
                MeshRen.material = AmberMaterial;
                break;

            default:
                break;
        }
    }

    public void SetTrafficLightStatus(TrafficLightState NewState)
    {
        CurrentState = NewState;
    }

    public TrafficLightState GetTrafficLightStatus()
    {
        return CurrentState;
    }
}