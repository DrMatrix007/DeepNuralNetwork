using NumSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepNeuralNetworks;

public class NeuralNetwork
{

    private NeuronLayer[] _layers;

    private ActivationFunction activationFunction;

    //public IEnumerable<NeuronLayer> Layers => _layers;
    public NeuralNetwork(ActivationFunction function,params int[] layers) : this(layers, function)
    {

    }

    public NeuralNetwork(int[] layers, ActivationFunction function)
    {
        _layers = new NeuronLayer[layers.Length-1];
        for (int i = 0; i < layers.Length-1; i++)
        {
            _layers[i] = new NeuronLayer(layers[i], layers[i + 1], function);
        }
        activationFunction = function;
    }


    public NDArray Predict(NDArray values)
    {
        var X = values.copy();
        for (int i = 0;i < _layers.Length; i++)
        {
            X = _layers[i].Forward(X);
        }
        return X;
    }
    public void Fit(NDArray X, NDArray Y,float learningRate ,uint repeats = 10000)
    {

        
        for (int repeat = 0; repeat < repeats; repeat++)
        {
            NDArray error;
            int current;
            List<NDArray> a = new();
            List<NDArray> deltas = new();
            NDArray layer;
            NDArray delta;
            a.Clear();
            deltas.Clear();
            
            current = Random.Shared.Next(X.shape[0]);

            a.Add(np.array(X[current]));

            for (int l = 0; l < _layers.Length; l++)
            {
                a.Add(_layers[l].Forward(a[l]));
            }

            error = -1*(Y[current] - a.Last());

            deltas.Add(error*activationFunction.Backwords(a.Last()));


            for (int l = a.Count-2; l > 0; l--)
            {
                deltas.Add(deltas.Last().dot(_layers[l].Weights.T) * activationFunction.Backwords(a[l]));
            }

            deltas.Reverse();


            for (int i = 0; i < _layers.Length; i++)
            {
                layer = np.atleast_2d(a[i]);
                delta = np.atleast_2d(deltas[i]);
                _layers[i].Weights += learningRate*layer.T.dot(delta);
            }
            //Console.WriteLine(error.mean().ToString());

            if (repeat % 1000 == 0)
            {
                Console.WriteLine("repeat "+repeat);
            }

        }
    }



}
