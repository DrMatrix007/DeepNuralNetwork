using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeepNeuralNetworks;
using NumSharp;

(NDArray input, NDArray output) GetPlus(int size, int range)
{
    var val1 = np.zeros(size, 2);
    var val2 = np.zeros(size, 3);
    int a, b;
    for (int i = 0; i < size; i++)
    {
        a = Random.Shared.Next(-range,range);
        b = Random.Shared.Next(-range,range);
        val1[i] = np.array(a, b);
        val2[i] = np.array(b,a,a+b);
        //val2[i] = np.array(0, a>b?1:0);

    }

    return (val1, val2);

}


var network = new NeuralNetwork(ActivationFunction.Relu,2,4,4,3);

var data = GetPlus(5000, 500);


network.Fit(data.input, data.output, 10f,1000);



Console.WriteLine((network.Predict(data.input)-data.output).mean().ToString());
//Console.WriteLine((data.output-network.Predict(data.input)).mean().ToString());
Console.WriteLine();











