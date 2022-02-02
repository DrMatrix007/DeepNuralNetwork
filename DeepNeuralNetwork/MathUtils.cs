using NumSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepNeuralNetworks;

public static class MathUtils
{
    
    public static float Sigma(int start, int end, Func<int, float> func)
    {
        var ans = 0.0f;
        for (int i = start; i < end; i++)
        {
            ans += func(i);
        }
        return ans;
    }


    //public static Matrix1D ToMatrix1D(this IEnumerable<float> f)
    //{
    //    return new Matrix1D(f.ToArray());
    //}

    //public static Matrix2D ToRandom(this Matrix2D m,Random? r =null)
    //{
    //    r ??= Random.Shared;

    //    var ans = new Matrix2D(m.Height,m.Width);

    //    for(int i = 0; i < ans.Width; i++)
    //    {
    //        for(int j = 0; j < ans.Height; j++)
    //        {
    //            ans[j, i] = r.Next(0,1000)/1000.0f ;
    //        }
    //    }

    //    return ans;

    //}

}
