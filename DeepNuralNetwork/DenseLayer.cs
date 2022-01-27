using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepNuralNetworks;

public class DenseLayer
{

    public Matrix2D Weights { get; }
    public Matrix2D Biases { get; }

    private Matrix2D _output;

    public Matrix2D Output { get =>_output; private set=>_output = value; }

    public DenseLayer(int inputs, int neurons)
    {
        Inputs = inputs;
        Neurons = neurons;
        Weights = new Matrix2D(inputs, neurons).ToRandom();
        Biases = new Matrix2D(1, neurons);
    }

    public int Inputs { get; }
    public int Neurons { get; }

    public Matrix2D Forward(Matrix2D values)
    {
        return _output = (values * Weights)+Biases ;
    }


}
