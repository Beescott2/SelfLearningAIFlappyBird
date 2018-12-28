using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

    public GameObject l_distanceTravelled;

    private bool m_isAlive;
    private Rigidbody2D m_rb;
    private float m_fitness;
    private float m_score;

    // Use this for initialization
    void Start () {
        m_rb = GetComponent<Rigidbody2D>();
        m_isAlive = true;
        m_fitness = 0;
        m_score = 0;
    }
	
	// Update is called once per frame
	void Update () {
        // Change velocity only if Neural Network Out > 0.5
        if (Input.GetKeyDown(KeyCode.Space))
            m_rb.velocity = new Vector2(0f, 5f);

        float yPosition = Camera.main.WorldToViewportPoint(transform.position).y;
        if (yPosition > 1 || yPosition < 0)
            m_isAlive = false;
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
        {
            print("Collision");
            m_isAlive = false;
            m_score = transform.position.x - l_distanceTravelled.transform.position.x;
        }
    }

    public bool isAlive()
    {
        return m_isAlive;
    }

    public float getFitness()
    {
        return m_fitness;
    }

    public void setFitness(float fitness)
    {
        m_fitness = fitness;
    }
}
