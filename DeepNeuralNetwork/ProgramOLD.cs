//using DeepNeuralNetworks;
//using NumSharp;
//public class Useless
//{
//    void Test()
//    {

//        var LearningRate = 0.1f;

//        (NDArray input, NDArray output) GetPlus(int size, int range)
//        {
//            var val1 = np.zeros(size, 2);
//            var val2 = np.zeros(size, 2);
//            float a, b;
//            for (int i = 0; i < size; i++)
//            {
//                a = Random.Shared.Next(i);
//                b = Random.Shared.Next(i);
//                val1[i] = np.array(a, b);
//                val2[i] = np.array(a > b ? 1 : 0, a > b ? 0 : 1);

//            }

//            return (val1, val2);

//        }

//        var values = GetPlus(5000, 50);
//        var layer1 = new NeuronLayer(2, 16);
//        var activation1 = ActivationFunction.Sigmoid;

//        var layer2 = new NeuronLayer(16, 2);
//        var activation2 = ActivationFunction.Sigmoid;
//        //var layer3 = new DenseLayer(16, 2);

//        for (var i = 0; i < 1000; i++)
//        {

//            var input = values.input;
//            var Z1 = layer1.Forward(input);
//            var A1 = activation1.Forward(Z1);

//            var Z2 = (layer2.Forward(A1));
//            var A2 = activation2.Forward(Z2);

//            //var Z3 = layer3.Forward(A2);
//            //var A3 = layer3.Forward(Z3);


//            var outputError = A2 - values.output;


//            var output_pd = outputError * A2 * (1 - A2);
//            //activation2.Backwords(A2);


//            var hiddenError = np.dot(output_pd, layer2.Weights.T);
//            var hidden_pd = hiddenError * (A1) * (1 - A1);



//            var final_layer2 = np.dot(A1.T, output_pd) / input.size;
//            var final_layer1 = np.dot(input.T, hidden_pd) / input.size;



//            layer1.Weights += -LearningRate * final_layer1;
//            layer2.Weights += -LearningRate * final_layer2;

//            //Console.WriteLine(outputError.ToString());
//            //Console.WriteLine((values.output - output).ToString());
//            Console.WriteLine((values.output - A2).mean().ToString());

//        }
//    }
//}