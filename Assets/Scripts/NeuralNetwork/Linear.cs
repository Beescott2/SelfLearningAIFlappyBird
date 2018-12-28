using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linear : AbstractFunction
{
    public override double computeDerivative(double currentInput)
    {
        return currentInput;
    }

    public override double computeOutput(double y)
    {
        return y;
    }


}
