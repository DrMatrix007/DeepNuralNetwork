using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepNeuralNetworks;

public class Normalizer
{

    public Normalizer(float minInput,float maxInput,float minOutput,float maxOutput)
    {
        MinInput = minInput;
        MaxInput = maxInput;
        MinOutput = minOutput;
        MaxOutput = maxOutput;
        InputSub = MaxInput - MinInput;
        OutputSub = MaxOutput - MinOutput;
    }

    public float MinInput { get; }
    public float MaxInput { get; }
    public float MinOutput { get; }
    public float MaxOutput { get; }
    public float InputSub { get; }
    public float OutputSub { get; }

    public float NormalizeInput(float a)
    {
        return Math.Clamp((a - MinInput) / InputSub, MinInput, MaxInput);

    }

    public Matrix NormalizeInput(Matrix a)
    {
        return a.OperatedFunction(a => NormalizeInput(a));
    }

    public float NormalizeOutput(float a)
    {
        return Math.Clamp((a - MinOutput) / OutputSub,MinOutput, MaxOutput);

    }

    public Matrix NormalizeOutput(Matrix a)
    {
        return a.OperatedFunction(a => NormalizeOutput(a));
    }


}
