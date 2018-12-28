using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationHandler : MonoBehaviour {

    public int l_numberInitialPopulation;
    public Bird l_birdPrefab;
    public GameObject l_travelledDistance;

    private List<Bird> m_birds;
    private Bird m_bird;

    private bool populationAlive()
    {
        foreach (Bird bird in m_birds)
        {
            if (bird.isAlive())
                return true;
        }

        return false;
    }

    private float distanceClosestObstacle()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        float minDist = float.MaxValue;

        foreach (GameObject obstacle in obstacles)
        {
            if (obstacle.transform.position.x - transform.position.x < minDist)
            {
                minDist = obstacle.transform.position.x;
            }
        }

        return minDist;
    }

    static int SortByFitness(Bird b1, Bird b2)
    {
        return b1.getFitness().CompareTo(b2.getFitness());
    }

    void createPopulation()
    {
        m_birds = new List<Bird>();
        // Create a new population with random neural networks
        for (int i = 0; i < l_numberInitialPopulation; i++)
        {
            m_bird = Instantiate(l_birdPrefab);
            m_birds.Add(m_bird);
            // random neural networks
            // TODO ...
        }
    }

	// Use this for initialization
	void Start () {
        createPopulation();
	}
	
	// Update is called once per frame
	void Update () {
		// Use neural network for all the population
        foreach (Bird bird in m_birds)
        {
            // Only once
            if (bird.isAlive())
            {
                // Calculate fitness
                // Compute total distance travelled
                float totalDistance = transform.position.x - l_travelledDistance.transform.position.x;
                float fitness = totalDistance - distanceClosestObstacle();

                bird.setFitness(fitness);
            }
        }

        // If all the population is dead
        if (!populationAlive())
        {
            // Evaluate the current population to the next one
            // Sort the birds by their fitness
            m_birds.Sort(SortByFitness);

            // Select the top 4 birds
            List<Bird> newBreed = new List<Bird>();
            newBreed.Add(m_birds[0]);
            newBreed.Add(m_birds[1]);
            newBreed.Add(m_birds[2]);
            newBreed.Add(m_birds[3]);

            // Create 1 offspring (crossover) producted from the first two
            // TODO ...

            // Create 3 offspring (crossover) producted from two random winners
            int randomWinner1 = Random.Range(0, 3);
            int randomWinner2;
            do
            {
                randomWinner2 = Random.Range(0, 3);
            } while (randomWinner2 != randomWinner1);
            // TODO ...

            // Create 2 offspring, copy of two randoml winners

        }
	}
}
