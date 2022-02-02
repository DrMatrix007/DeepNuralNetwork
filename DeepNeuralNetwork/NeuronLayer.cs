using NumSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepNeuralNetworks;

internal class NeuronLayer
{
    public ActivationFunction activationFunciton {get;}
    public NDArray Weights { get; set; }
    public NDArray Biases { get; set; }

    public NeuronLayer(int inputs, int neurons,ActivationFunction function)
    {
        Inputs = inputs;
        Neurons = neurons;
        Weights = np.random.randn(inputs, neurons)/10;
        Biases = np.zeros(1, neurons);
        activationFunciton = function;
    }

    public int Inputs { get; }
    public int Neurons { get; }

    public NDArray Forward(NDArray values)
    {
        return activationFunciton.Forward(np.dot(np.atleast_2d(values), Weights));
    }

    public NDArray Backward(NDArray values,NDArray deltas)
    {
        return deltas.dot(Weights.T)*activationFunciton.Backwords(values);
    }


}
