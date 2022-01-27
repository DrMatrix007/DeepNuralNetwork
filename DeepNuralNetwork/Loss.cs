using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepNuralNetworks;

public class Loss
{
    public const float EPSILON = 0.000001f;

    public static readonly Loss CategoricalEntropy = new Loss((pred_val, true_val) =>
    {
        var samples = pred_val.Width;
        var ans = 0.0f;
        var counter = 0;
        var mult = (pred_val * true_val.Tranpose());
        var maxSize = pred_val.Width * pred_val.Height;
        foreach (var item in pred_val)
        {
            pred_val[item.row,item.column] = Math.Clamp(item.value,EPSILON, 1 - EPSILON);
        }
        
        foreach (var item in mult)
        {
            counter++;
            ans += - MathF.Log(item.value);
        }

        return ans/counter;

    });

    private readonly Func<Matrix2D, Matrix2D, float> func;

    public Loss(Func<Matrix2D,Matrix2D,float> func)
    {
        this.func = func;
    }

    public float Forward(Matrix2D pred_val,Matrix2D true_val)
    {
        return func(pred_val,true_val);
    }

}
