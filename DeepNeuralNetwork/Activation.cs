using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepNeuralNetworks;

public class Activation
{

    public static readonly Activation Relu = new Activation(a => MathF.Max(a, 0), a => a > 0 ? 1 : 0);

    public static readonly Activation Sigmoid = new Activation(a => 1/(1+MathF.Exp(a)), a => (1- 1 / (1 + MathF.Exp(a)))*(1 / (1 + MathF.Exp(a))));


    public static readonly Activation Tanh = new Activation(a => (MathF.Exp(a) - MathF.Exp(-a)) / (MathF.Exp(a) + MathF.Exp(-a)),a=>1-Tanh!.Forward(a));

    private Func<float, float> f;

    private Func<float, float> der;


    public Matrix Forward(Matrix m) => m.OperatedFunction(f);

    public Matrix Backwards(Matrix m) => m.OperatedFunction(der);
    public float Forward(float m) => f(m);

    public float Backwards(float m) => der(m);



    public Activation(Func<float, float> f, Func<float, float> der)
    {
        this.f = f;
        this.der = der;
    }
}
