using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

    private GameObject l_distanceTravelled;

    private bool m_isAlive;
    private Rigidbody2D m_rb;
    private float m_fitness;
    private float m_score;
    private NeuralNetwork m_neuralNetwork;
    private Vector3 deathPosition;

    GameObject closestObstacle()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("ObstaclePrefab");
        float minDist = float.MaxValue;
        GameObject resObstacle = null;

        foreach (GameObject obstacle in obstacles)
        {
            if (obstacle.transform.position.x - transform.position.x < minDist &&
                obstacle.transform.position.x >= transform.position.x)
            {
                minDist = obstacle.transform.position.x;
                resObstacle = obstacle;
            }
        }

        return resObstacle;
    }

    double heightDistanceClosestObstacle()
    {
        GameObject closestObst = closestObstacle();
        return closestObst.transform.position.y - transform.position.y;
    }

    double widthDistanceClosestObstacle()
    {
        GameObject closestObst = closestObstacle();
        return closestObst.transform.position.x - transform.position.x;
    }

    void computeFitness()
    {
        m_fitness = transform.position.x - l_distanceTravelled.transform.position.x;
    }

    void death()
    {
        l_distanceTravelled = GameObject.FindGameObjectWithTag("TravelledDistance");
        m_score = transform.position.x - l_distanceTravelled.transform.position.x;
        computeFitness();
        m_isAlive = false;
        deathPosition = transform.position;
        m_rb.velocity = new Vector2(-5.0f, 0.0f);
    }

    // Use this for initialization
    void Start () {
        m_rb = GetComponent<Rigidbody2D>();
        m_isAlive = true;
        m_fitness = 0;
        m_score = 0;

        m_neuralNetwork = new NeuralNetwork(2, 1, 6, 1, new Linear());
    }
	
	// Update is called once per frame
	void Update () {
        if (m_isAlive)
        {
            List<double> neural_inputs = new List<double>();
            neural_inputs.Add(heightDistanceClosestObstacle());
            neural_inputs.Add(widthDistanceClosestObstacle());
            

            double neuralOut = m_neuralNetwork.computeOutputs(neural_inputs)[0];

            // Change velocity only if Neural Network Out > 0.5
            if (neuralOut > 0.5)
                m_rb.velocity = new Vector2(0f, 5f);

            float yPosition = Camera.main.WorldToViewportPoint(transform.position).y;
            if (yPosition > 1 || yPosition < 0)
                death();
        }
        else
        {
            transform.position = new Vector3(transform.position.x, deathPosition.y, -1);
        }
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
        {
            if (m_isAlive)
                death();
        }
    }

    public bool isAlive()
    {
        return m_isAlive;
    }

    public double getFitness() { return m_fitness; }
}
