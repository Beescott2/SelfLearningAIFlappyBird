using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractFunction : MonoBehaviour
{
    /**
       * Compute sum of inputs
       *
       * @param inputs List of inputs
       * @return Y, the sum
       */
    public double computeY(List<double> inputs, Neuron neuron)
    {
        if (inputs.Count + 1 != neuron.getWeights().Count)
            print("Size miss match between inputs size and weights size");

        double sum = 0.0;
        int i;
        for (i = 0; i < inputs.Count; i++)
        {
            sum += inputs[i] * neuron.getWeights()[i];
        }
        // Accounting for bias
        sum += neuron.getWeights()[i];

        return sum;
    }

    public abstract double computeDerivative(double currentInput);

    public abstract double computeOutput(double y);
}
