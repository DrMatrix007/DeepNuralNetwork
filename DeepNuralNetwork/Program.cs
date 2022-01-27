using DeepNuralNetworks;

//var X = new Matrix2D(new float[,]
//{
//    {1, 2, 3, 2.5f },
//    {2.0f,5.0f,-1.0f,2.0f },
//    {-1.5f,2.7f,3.3f,-0.8f }
//});

//var layer1 = new DenseLayer(4,2);
//var layer2 = new DenseLayer(2,16);


//layer1.Forward(X);
//Console.WriteLine(layer1.Output);
//layer2.Forward(layer1.Output);
//Console.WriteLine(layer1.Output);

List<((float a, float b), int c)> GetPlus(int size, int range)
{
    float val1, val2;
    var l = new List<((float a, float b), int c)>();
    for (int i = 0; i < range; i++)
    {
        val1 = Random.Shared.Next(i);
        val2 = Random.Shared.Next(i);
        l.Add(((val1, val2), val2 + val1>0?1:0));
    }

    return l;

}

var values = GetPlus(100000, 50);




var layer1 = new DenseLayer(2, 3);

var output = layer1.Forward(new Matrix2D(values.Select(a => new Matrix1D(a.Item1.a, a.Item1.b))));

var activation1 = ActivationFunction.ReLU;

output = activation1.Forward(output);

var layer2 = new DenseLayer(3, 2);

output = layer2.Forward(output);
var activation2 = ActivationFunction.SoftMax;

output = activation2.Forward(output);

Console.WriteLine(output);
var loss = Loss.CategoricalEntropy.Forward(output, new Matrix2D(values.Select((a,i) =>
{
    // the len of the last layer is 2
    var ans = new Matrix1D(2);

    ans[a.c] = 1;

    return ans;
})));

Console.WriteLine(loss);