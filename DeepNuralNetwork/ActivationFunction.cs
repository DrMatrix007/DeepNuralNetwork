using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepNuralNetworks;

public class ActivationFunction
{
    public static readonly ActivationFunction ReLU = new ActivationFunction((a) => a.Select(val => MathF.Max(val.value, 0)).ToMatrix1D());

    public static readonly ActivationFunction SoftMax = new ActivationFunction((a) =>
   {
       var max = a.Max();
       float sum = 0;
       foreach (var item in a)
       {
           a[item.pos] -= max.value;
       }
       foreach (var item in a)
       {
           a[item.pos] = MathF.Exp(item.value);
       }
       sum = a.Select(val => val.value).Sum();
       foreach (var item in a)
       {
           a[item.pos] /= sum;
       }
       return a;
   });



    private readonly Func<Matrix1D, Matrix1D> func;

    public ActivationFunction(Func<Matrix1D, Matrix1D> func)
    {
        this.func = func;
    }

    public Matrix1D Calculate(Matrix1D f)
    {
        return func(f);
    }

    public Matrix2D Forward(Matrix2D val)
    {
        return new Matrix2D(val.ToMatrices1D().Select(func));
    }




}
