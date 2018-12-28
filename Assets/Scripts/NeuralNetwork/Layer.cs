using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour
{
    List<Neuron> neurons;

    public Layer(int inputsNumber, int numberOfNeurons, AbstractFunction activationFunction)
    {
        for (int i = 0; i < numberOfNeurons; i++)
        {
            neurons.Add(new Neuron(inputsNumber, activationFunction));
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
}
