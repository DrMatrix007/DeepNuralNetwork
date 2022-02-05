using DeepNeuralNetworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//var input = new Matrix(new float[,]
//    {
//        {1,0 },{0,1},{0,0 },{1,1 }
//    });


var net = new NeuralNetwork(Activation.Relu, new Normalizer(0, 1, 0, 1), 2, 4, 2);

(Matrix X, Matrix Y) GenerateValues(int size)
{
    var x = new Matrix(2, size);
    var y = new Matrix(2, size);
    float a, b;
    for (int i = 0; i < size; i++)
    {
        a = Random.Shared.NextSingle();
        b = Random.Shared.NextSingle();

        x[i, 0] = a;
        x[i, 1] = b;


        y[i, 0] = a>b?0:1;
        y[i, 1] = a<b?0:1;
    }
    return (x, y);
}

var (x, y) = GenerateValues(10000);

net.Fit(x, y);

Console.WriteLine((net.Predict(x)-y).Avg());




















//var net = new NeuralNetwork(Activation.Tanh, new Normalizer(0, 6, 0, 1), 1,4, 4,4, 1);

//(Matrix X, Matrix Y) GenerateValues(int size)
//{
//    var x = new Matrix(1, size);
//    var y = new Matrix(1, size);
//    float a;
//    for (int i = 0; i < size; i++)
//    {
//        a = Random.Shared.NextSingle()*6;

//        x[i, 0] = a;


//        y[i, 0] = MathF.Sin(a);
//    }
//    return (x, y);

//}
//Matrix val = new Matrix();
//Matrix x, y;
//for (int i = 0; i < 1000; i++)
//{
//    (x, y) = GenerateValues(10000);

//    net.Fit(x, y, epochs: 1000, LearningRate: 0.01f);
//    //net.Fit(
//    //    new Matrix(new float[,]
//    //    {
//    //        {1,0 },{0,1},{0,0 },{1,1 }
//    //    }),
//    //    new Matrix(new float[,]
//    //    {
//    //        {0,1 },{1,0},{1,1 },{0,0}
//    //    }));

//    //Console.WriteLine((net.Predict(x)-y));
//    val = (net.Predict(x) - y);
//    Console.WriteLine($"{i}:{val.Avg()} max:{val.Max()} min: {val.Min()}");

//}
//(x, y) = GenerateValues(100);

//Console.WriteLine(net.Predict(x) - y);
//Console.WriteLine((net.Predict(x) - y).Avg());