using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulationHandler : MonoBehaviour {

    public int l_numberInitialPopulation;
    public Bird l_birdPrefab;
    public GameObject l_travelledDistance;
    public ObstacleGenerator l_og;

    public Text l_score;
    public Text l_generation;
    
    private List<Bird> m_birds;
    private Bird m_bird;

    private NeuralNetwork lastFitnessTop1BirdNeuralNetwork;
    private NeuralNetwork lastFitnessTop2BirdNeuralNetwork;

    private NeuralNetworkFactory l_neuralNetworkFactory;

    private double lastFitnessTop1Bird;
    private double lastFitnessTop2Bird;

    private bool populationAlive()
    {
        foreach (Bird bird in m_birds)
        {
            if (bird.isAlive())
                return true;
        }

        return false;
    }

    static int SortByFitness(Bird b1, Bird b2)
    {
        return -b1.getFitness().CompareTo(b2.getFitness());
    }

    void createPopulation()
    {
        m_birds = new List<Bird>();
        // Create a new population with random neural networks
        for (int i = 0; i < l_numberInitialPopulation; i++)
        {
            m_bird = Instantiate(l_birdPrefab);
            m_birds.Add(m_bird);
        }
    }

	// Use this for initialization
	void Start () {
        createPopulation();
        l_og.invokeGenerateObstacle();

        lastFitnessTop1Bird = 0;
        lastFitnessTop2Bird = 0;

        l_neuralNetworkFactory = new NeuralNetworkFactory();
    }

    void Update()
    {
        if (!populationAlive())
        {
            updateUI();

            removeObstacles();

            // Sort the birds by their fitness
            m_birds.Sort(SortByFitness);
            
            // If the two fittest birds are better than the last generations, then save them
            if (m_birds[0].getFitness() > lastFitnessTop1Bird)
            {
                lastFitnessTop1Bird = m_birds[0].getFitness();
                lastFitnessTop1BirdNeuralNetwork = m_birds[0].getNeuralNetwork();

                if (m_birds[1].getFitness() > lastFitnessTop2Bird)
                {
                    lastFitnessTop2Bird = m_birds[1].getFitness();
                    lastFitnessTop2BirdNeuralNetwork = m_birds[1].getNeuralNetwork();
                }
            }
            else if (m_birds[0].getFitness() > lastFitnessTop2Bird)
            {
                lastFitnessTop2Bird = m_birds[0].getFitness();
                lastFitnessTop2BirdNeuralNetwork = m_birds[0].getNeuralNetwork();
            }
            
            // Remove the population
            removePopulation();

            // Create the obstacles
            l_og.invokeGenerateObstacle();

            // Create 6 birds from breeding those two fittest
            for (int i = 0; i < 9; i++)
            {
                m_bird = Instantiate(l_birdPrefab);
                m_bird.setNeuralNetwork(
                    l_neuralNetworkFactory.breedNetworks(
                        lastFitnessTop1BirdNeuralNetwork,
                        lastFitnessTop2BirdNeuralNetwork
                        )
                );
                m_birds.Add(m_bird);
            }

            for (int i = 5; i < 7; i++)
            {
                m_birds[i].setNeuralNetwork(
                    l_neuralNetworkFactory.evolveNetwork(
                        m_birds[i].getNeuralNetwork()));
            }

            for (int i = 7; i < 9; i++)
            {
                m_birds[i].setNeuralNetwork(
                    l_neuralNetworkFactory.mutateNetwork(
                        m_birds[i].getNeuralNetwork()));
            }
        }
    }

    void removeObstacles()
    {
        List<GameObject> obstacles = new List<GameObject>();
        obstacles.AddRange(GameObject.FindGameObjectsWithTag("ObstaclePrefab"));

        for (int i=obstacles.Count-1; i>0; i--)
        {
            GameObject obstacle = obstacles[i];
            obstacles.Remove(obstacle);
            Destroy(obstacle);
        }
        l_og.resetObstacles();
    }

    void removePopulation()
    {
        List<GameObject> birds = new List<GameObject>();
        birds.AddRange(GameObject.FindGameObjectsWithTag("Bird"));

        for (int i = birds.Count - 1; i > 0; i--)
        {
            GameObject bird = birds[i];
            birds.Remove(bird);
            Destroy(bird);
        }

        m_birds = new List<Bird>();

        GameObject travelledDistance = GameObject.FindGameObjectWithTag("TravelledDistance");
        travelledDistance.transform.position = new Vector3(-8, travelledDistance.transform.position.y, travelledDistance.transform.position.z);
    }

    void updateUI()
    {
        int bestScore = int.Parse(l_score.text);
        foreach (Bird bird in m_birds)
        {
            if (bird.getFitness() > bestScore)
                bestScore = (int) bird.getFitness();

            l_score.text = bestScore.ToString();
        }

        l_generation.text = (int.Parse(l_generation.text) + 1).ToString();
    }
}
