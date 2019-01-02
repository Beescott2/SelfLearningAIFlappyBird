using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour {

    public GameObject l_obstacle;

    private List<GameObject> m_obstacles;
    private GameObject m_obstacle;

    private int obstacleId;

	// Use this for initialization
	void Start () {
        m_obstacles = new List<GameObject>();
        obstacleId = 0;
	}

    void Update()
    {
        for (int i=m_obstacles.Count - 1; i>0; i--)
        {
            if (m_obstacles[i].transform.position.x < -20 && m_obstacles[i] != null)
            {
                GameObject obstacle = m_obstacles[i];
                m_obstacles.Remove(obstacle);
                Destroy(obstacle);
            }
        }
    }
    
    public void resetObstacles()
    {
        m_obstacles.Clear();
    }

    public void invokeGenerateObstacle()
    {
        CancelInvoke("generateObstacle");
        InvokeRepeating("generateObstacle", 0.0f, 1.5f);
    }

    void generateObstacle()
    {
        m_obstacle = Instantiate(l_obstacle);
        m_obstacle.transform.position = new Vector3(7f, Random.Range(3.5f, -2.5f), -1f);
        m_obstacles.Add(m_obstacle);
        m_obstacle.name = "Obstacle" + obstacleId;
        obstacleId++;
    }
}
