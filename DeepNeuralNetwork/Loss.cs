//using NumSharp;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DeepNuralNetworks;

//public class Loss
//{
//    public const float EPSILON = 0.000001f;

//    public static readonly Loss CategoricalEntropy = new Loss((pred_val, true_val) =>
//    {
//        var ans = 0.0f;
//        var counter = 0;
//        for (int i = 0; i < pred_val.size; i++)
//        {

//            counter++;
//            ans += -MathF.Log();

//        }
//        return ans/counter;

//    });

//    private readonly Func<NDArray, NDArray, float> func;

//    public Loss(Func<NDArray, NDArray, float> func)
//    {
//        this.func = func;
//    }

//    public float Forward(NDArray pred_val, NDArray true_val)
//    {
//        return func(pred_val,true_val);
//    }

//}
