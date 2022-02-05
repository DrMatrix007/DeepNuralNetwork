using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepNeuralNetworks;

public class Layer
{
    public Matrix Weights;


    public Layer(int input,int output)
    {
        Weights = new Matrix(output,input);
        Weights = Weights.OperatedFunction(a => Random.Shared.NextSingle());
    }

    public Matrix Forward(Matrix mat)
    {
        return mat*Weights;
    }

}
