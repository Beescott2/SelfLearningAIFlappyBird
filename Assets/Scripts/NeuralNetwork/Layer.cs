using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer
{
    List<Neuron> neurons;

    // Used for the factory
    public Layer()
    {
        this.neurons = new List<Neuron>();
    }

    public Layer(int inputsNumber, int numberOfNeurons, AbstractFunction activationFunction)
    {
        neurons = new List<Neuron>();
        for (int i = 0; i < numberOfNeurons; i++)
        {
            neurons.Add(new Neuron(inputsNumber + 1, activationFunction));
        }
    }

    public List<double> computeOutputs(List<double> inputs)
    {
        List<double> layerOuputs = new List<double>();

        for(int i = 0; i < this.neurons.Count; i++)
        {
            layerOuputs.Add(this.neurons[i].computeOutput(inputs));
        }

        return layerOuputs;
    }

    public List<Neuron> getNeurons()
    {
        return this.neurons;
    }

    public void setNeurons(List<Neuron> neurons)
    {
        this.neurons = neurons;
    }
}
