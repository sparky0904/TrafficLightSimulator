using UnityEngine;

[RequireComponent(typeof(TrafficSpawnPointConfig))]
public class TrafficSpawnPointController : MonoBehaviour
{
    private TrafficSpawnPointConfig m_trafficSpawnPointConfig;
    private float m_nextSpawnTime;

    // Use this for initialization
    private void Start()
    {
        m_trafficSpawnPointConfig = GetComponent<TrafficSpawnPointConfig>();
        m_nextSpawnTime = m_trafficSpawnPointConfig.SpawnTimeInMinutes;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.time > m_nextSpawnTime)
        {
            SpawnNewVehicle();
            m_nextSpawnTime = Time.time + (float)m_trafficSpawnPointConfig.SpawnTimeInMinutes;
        }
    }

    private void SpawnNewVehicle()
    {
        GameObject Car_Orange;
        Car_Orange = CodeMonkey.Assets.i.car_Orange;

        GameObject newVehicle = Instantiate(Car_Orange, this.transform.position, this.transform.rotation);
    }
}