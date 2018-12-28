using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour {

    public GameObject l_obstacle;

    private List<GameObject> m_obstacles;
    private GameObject m_obstacle;

	// Use this for initialization
	void Start () {
        InvokeRepeating("generateObstacle", 0.0f, 1.5f);
        m_obstacles = new List<GameObject>();
	}

    void Update()
    {
        for (int i=m_obstacles.Count - 1; i>0; i--)
        {
            if (m_obstacles[i].transform.position.x < -20)
            {
                GameObject obstacle = m_obstacles[i];
                m_obstacles.Remove(obstacle);
                Destroy(obstacle);
            }
        }
    }

    void generateObstacle()
    {
        m_obstacle = Instantiate(l_obstacle);
        m_obstacle.transform.position = new Vector3(15f, Random.Range(2.5f, -3.5f), -1f);
        m_obstacles.Add(m_obstacle);
    }
}
