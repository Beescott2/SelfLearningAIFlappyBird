using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetworkFactory : MonoBehaviour
{
    private const double EVOLUTION_COEF = 0.1;
    private const double MUTATION_CHANCE = 0.1;
    private const double MUTATION_COEF = 0.5;


    public NeuralNetwork breedNetworks(NeuralNetwork fatherNetwork, NeuralNetwork motherNetwork)
    {

        NeuralNetwork bredNetwork = new NeuralNetwork();
        // For each layer select neurons randomly
        for (int layerIndex = 0; layerIndex < fatherNetwork.getLayers().Count; layerIndex++)
        {
            Layer constructedLayer = new Layer();
            List<Neuron> neurons = new List<Neuron>();

            // For each neuron
            for (int neuronIndex = 0; neuronIndex < fatherNetwork.getLayers()[layerIndex].getNeurons().Count; neuronIndex++)
            {
                // Coin flip to select a neuron
                if (UnityEngine.Random.Range(0f, 1f) > 0.5)
                {
                    // Copy father neuron
                    neurons.Add(new Neuron(fatherNetwork.getLayers()[layerIndex].getNeurons()[neuronIndex]));
                }
                else
                {
                    // Copy mother neuron
                    neurons.Add(new Neuron(motherNetwork.getLayers()[layerIndex].getNeurons()[neuronIndex]));
                }
            }
            constructedLayer.setNeurons(neurons);
            bredNetwork.addLayer(constructedLayer);
        }
        return bredNetwork;
    }

    // Add small modifications to each weights of a network
    public NeuralNetwork evolveNetwork(NeuralNetwork network)
    {
        // For each layer
        for (int layerIndex = 0; layerIndex < network.getLayers().Count; layerIndex++)
        {

            // For each neuron
            for (int neuronIndex = 0; neuronIndex < network.getLayers()[layerIndex].getNeurons().Count; neuronIndex++)
            {
                // For each weight
                for (int weightIndex = 0; weightIndex < network.getLayers()[layerIndex].getNeurons()[neuronIndex].getWeights().Count; weightIndex++)
                {
                    network.getLayers()[layerIndex].getNeurons()[neuronIndex].getWeights()[weightIndex] += (double)((UnityEngine.Random.Range(0f, 2f) - 1) * EVOLUTION_COEF);
                }
            }
        }
        return network;
    }

    public NeuralNetwork mutateNetwork(NeuralNetwork network)
    {
        // For each layer
        for (int layerIndex = 0; layerIndex < network.getLayers().Count; layerIndex++)
        {

            // For each neuron
            for (int neuronIndex = 0; neuronIndex < network.getLayers()[layerIndex].getNeurons().Count; neuronIndex++)
            {
                // For each weight
                for (int weightIndex = 0; weightIndex < network.getLayers()[layerIndex].getNeurons()[neuronIndex].getWeights().Count; weightIndex++)
                {
                    if ((double)UnityEngine.Random.Range(0f, 1f) <= MUTATION_CHANCE)
                        network.getLayers()[layerIndex].getNeurons()[neuronIndex].getWeights()[weightIndex] += (double)((UnityEngine.Random.Range(0f, 2f) - 1) * MUTATION_COEF);
                }
            }
        }
        return network;
    }
}
