using NumSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepNeuralNetworks;

public class ActivationFunction
{
    public static readonly ActivationFunction Sigmoid = new ActivationFunction((a) => 1 / (1 + np.exp(-1*a)), (a) => Sigmoid!.Forward(a)*(1- Sigmoid.Forward(a)));

    public static readonly ActivationFunction Linear = new ActivationFunction(a => a, a => np.ones(a.shape));

    public static readonly ActivationFunction LeakyRelu = new ActivationFunction((a) =>
    {
        var shape = a.shape;
        var newa = a.reshape(a.size);
        for (int i = 0; i < a.size; i++)
        {
            newa[i] = newa[i].GetValue<float>() > 0 ? newa[i] : newa[i]*0.1f;
        }
        return newa.reshape(shape);
    }, (a) =>
    {
        var shape = a.shape;
        var newa = a.reshape(a.size);
        for (int i = 0; i < a.size; i++)
        {
            newa[i] = newa[i].GetValue<float>() > 0 ? 1 : 0.1f;
        }
        return newa.reshape(shape);
    });

    public static readonly ActivationFunction SoftMax = new ActivationFunction((a) =>
   {
       a = a.copy();
       var max = np.max(a);
       float sum = 0;
       for (int i = 0; i < a.size; i++)
       {
           a[i] -= max;

       }

       for (int i = 0; i < a.size; i++)
       {
           a[i] = MathF.Exp(a[i]);
       }
       sum = np.sum(a);
       for (int i = 0; i < a.size; i++)
       {
           a[i] /= sum;
       }
       return a;
   },(a)=>throw new NotImplementedException("no derivative for softmax!"));
    public static readonly ActivationFunction Relu = new ActivationFunction((a) => np.maximum(a, np.zeros(a.shape)), (a) =>
    {
        return np.maximum(np.sign(a), 0);
    });


    private readonly Func<NDArray, NDArray> func;
    private readonly Func<NDArray, NDArray> derivative;
    public ActivationFunction(Func<NDArray, NDArray> func,Func<NDArray,NDArray> d)
    {
        this.func = func;
        derivative = d;
    }

    public NDArray Forward(NDArray f)
    {
        return func(f);
    }
    public NDArray Backwords(NDArray f)
    {
        return derivative(f);
    }




}
