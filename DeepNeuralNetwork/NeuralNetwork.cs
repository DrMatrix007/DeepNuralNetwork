using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepNeuralNetworks;

public class NeuralNetwork
{

    private Layer[] Layers;
    public Activation Activation { get; }
    public Normalizer Normalizer { get; }

    public NeuralNetwork(Activation activation,Normalizer normalizer,params int[] layers)
    {
        Activation = activation;
        Normalizer = normalizer;
        //Activation = activation ??= Activation.Relu;
        Layers = new Layer[layers.Length - 1];
        for (int i = 0; i < layers.Length - 1; i++)
        {
            Layers[i] = new Layer(layers[i], layers[i + 1]);
        }
    }


    public Matrix Predict(Matrix mat)
    {
        mat = Normalizer.NormalizeInput(mat);
        foreach (var item in Layers)
        {
            mat = Activation.Forward(item.Forward(mat));
        }
        return Normalizer.NormalizeOutput(mat);
    }


    public void Fit(Matrix X, Matrix Y, float LearningRate = 0.1f, int epochs = 100000)
    {
        X = Normalizer.NormalizeInput(X);
        Y = Normalizer.NormalizeOutput(Y);
        List<Matrix> values = new();
        List<Matrix> deltas = new();
        int i;
        int current;
        Matrix layer;
        Matrix delta;
        Matrix error;
        Matrix dot;
        for (int epoch = 0; epoch < epochs; epoch++)
        {
            current = Random.Shared.Next(X.Dimensions.Height);
            values.Clear();
            deltas.Clear();


            values.Add(X[current]);
            //Console.WriteLine(values.Last().ToString());
            foreach (var item in Layers)
            {
                values.Add(Activation.Forward(item.Forward(values.Last())));
            }

            error = values.Last()- Y[current];

            deltas.Add(error.SillyMult(Activation.Backwards(values.Last())));

            for ( i = values.Count-2; i>0; i--)
            {
                dot = (deltas.Last()*(Layers[i].Weights.T));
                deltas.Add((deltas.Last() * Layers[i].Weights.T).SillyMult(Activation.Backwards(values[i])) );
            }

            deltas.Reverse();

            for (i = 0; i < Layers.Length; i++)
            {
                layer = values[i];
                delta = deltas[i];
                Layers[i].Weights -= LearningRate * layer.T * delta;

                //Console.WriteLine(Layers[i].Weights);
            }


        }
    }


}
