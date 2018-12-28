using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sigmoid : AbstractFunction
{
    public override double computeDerivative(double currentInput)
    {
        throw new System.NotImplementedException();
    }

    public override double computeOutput(double y)
    {
        return 1.0 / (1.0 + Mathf.Pow(2.72f, (float)-y));
    }
}
