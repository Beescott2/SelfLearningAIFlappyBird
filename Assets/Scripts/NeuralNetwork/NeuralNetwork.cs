using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork
{
    List<Layer> layers;

    public NeuralNetwork(int inputsNumber, int numberOfHiddenLayers, int hiddenLayersSize, int numberOfOutput, AbstractFunction activationFunction)
    {
        // first layer - inputs layer
        // For now there will a neuron for each input
        layers = new List<Layer>();
        this.layers.Add(new Layer(inputsNumber, inputsNumber, activationFunction));

        // Hidden layers
        for (int i = 0; i < numberOfHiddenLayers; i++)
        {
            this.layers.Add(new Layer(this.layers[i].getNeurons().Count, hiddenLayersSize, activationFunction));
        }

        // Output layer
        // Number of inputs is based on the preceding layer (if hiddenLayerSize is 0, the preceding layer will be the first layer)
        this.layers.Add(new Layer(this.layers[numberOfHiddenLayers].getNeurons().Count, numberOfOutput, activationFunction));
    }

    public List<double> computeOutputs(List<double> inputs)
    {
        List<List<double>> layersOutputs = new List<List<double>>();

        // first layer
        layersOutputs.Add(this.layers[0].computeOutputs(inputs));

        // Rest of the layers
        for (int i = 1; i < this.layers.Count; i++)
        {
            layersOutputs.Add(this.layers[i].computeOutputs(layersOutputs[i - 1]));
        }

        return layersOutputs[this.layers.Count - 1];
    }
}
