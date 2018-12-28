using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron : MonoBehaviour
{
    private const double LEARNING_RATE = 0.01;
    private List<double> weights;
    private AbstractFunction activationFunction;

    public Neuron(int weightsNumber, AbstractFunction activationFunction)
    {
        this.activationFunction = activationFunction;
        this.initWeights(weightsNumber);
    }


    /**
     * Generate random weights for the neuron
     * Take in account the bias
     * So weightsNumber must be equal to number of inputs + 1
     *
     * @param weightsNumber number of weights to generate, take bias in account
     */
    private void initWeights(int weightsNumber)
    {
        this.weights = new List<double>();
        for (int i = 0; i < weightsNumber; i++)
        {
            this.weights.Add((double)Random.Range(0f, 1f));
        }
    }

    public List<double> getWeights()
    {
        return this.weights;
    }
    public double computeOutput(List<double> inputs)
    {
        return this.activationFunction.computeOutput(this.activationFunction.computeY(inputs, this));
    }

    public double train(double expectedValue, List<double> inputs)
    {
        if (inputs.Count + 1 != this.getWeights().Count)
            throw new System.Exception("Size miss match between inputs and weights");

        int i;
        double deltaError = expectedValue - this.computeOutput(inputs);
        for (i = 0; i < inputs.Count; i++)
        {
            this.weights[i] = this.weights[i] + deltaError * Neuron.LEARNING_RATE * this.activationFunction.computeDerivative(inputs[i]);
        }
        // Bias
        this.weights[i] = this.weights[i] + deltaError * Neuron.LEARNING_RATE * this.activationFunction.computeDerivative(1.0);

        return deltaError;
    }

}
