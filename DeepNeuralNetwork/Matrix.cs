using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepNeuralNetworks;
public struct Dimensions
{

    public readonly int Width;
    public readonly int Height;

    public Dimensions(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public static bool operator ==(Dimensions a, Dimensions b)
    {
        return a.Width == b.Width && a.Height == b.Height;
    }
    public static bool operator !=(Dimensions a, Dimensions b)
    {
        return a.Width != b.Width || a.Height != b.Height;
    }

    public override bool Equals(object obj)
    {
        return (obj.GetType() == typeof(Dimensions)) && (Dimensions)obj == this;
    }
}
public struct Matrix
{

    public class MatrixNotSuitableException : Exception
    {
        public MatrixNotSuitableException() : base("This matrix is not suitable to use this way.") { }
    }

    public float Avg()
    {
        var sum = 0.0f;

        for (int i = 0; i < Dimensions.Width; i++)
        {
            for (int j = 0; j < Dimensions.Height; j++)
            {
                sum += this[j, i];
            }
        }
        sum/= Dimensions.Width*Dimensions.Height;

        return sum;
    }
    public float Max()
    {
        var ans = float.MinValue;

        for (int i = 0; i < Dimensions.Width; i++)
        {
            for (int j = 0; j < Dimensions.Height; j++)
            {
                ans = MathF.Max(ans, this[j, i]);
            }
        }
        return ans;
    }
    public float Min()
    {
        var ans = float.MaxValue;

        for (int i = 0; i < Dimensions.Width; i++)
        {
            for (int j = 0; j < Dimensions.Height; j++)
            {
                ans = MathF.Min(ans, this[j, i]);
            }
        }
        return ans;
    }

    public float this[int y, int x]
    {
        get => values[y, x];
        set => values[y, x] = value;
    }
    public Matrix this[int y]
    {
        get
        {
            Matrix ans = new Matrix(Dimensions.Width, 1);
            for (int i = 0; i < Dimensions.Width; i++)
            {
                ans[0, i] = values[y, i];
            }
            return ans;
        }
    }

    public Dimensions Dimensions => new Dimensions(values.GetLength(1), values.GetLength(0));

    private float[,] values;


    public static Matrix FromVertical(params float[] values)
    {
        var ans = new Matrix(1, values.Length);

        for (int i = 0; i < values.Length; i++)
        {
            ans[i, 0] = values[i];
        }

        return ans;
    }

    internal Matrix Dot(Matrix t)
    {
        if (t.Dimensions.Height == 1 && Dimensions.Height == 1)
        {
            return this.T * t;
        }
        return this * t;
    }

    public static Matrix FromHorizontal(params float[] values)
    {
        var ans = new Matrix(values.Length, 1);

        for (int i = 0; i < values.Length; i++)
        {
            ans[0, i] = values[i];
        }

        return ans;
    }

    //public Matrix(params float[] values)
    //{
    //    this.values = new float[1, values.Length];
    //    for (int i = 0; i < values.Length; i++)
    //    {
    //        this.values[0, i] = values[i];
    //    }
    //}
    public Matrix()
    {
        values = new float[1, 1];
    }
    public Matrix(float[,] values)
    {
        this.values = values;
    }

    public Matrix(int width, int height = 1)
    {
        values = new float[height, width];
    }
    public Matrix(Dimensions dims) : this(dims.Width, dims.Height)
    {

    }


    public static Matrix operator +(Matrix a, Matrix b)
    {
        return a.Add(b);
    }
    public static Matrix operator -(Matrix a, Matrix b)
    {
        return a.Subtract(b);
    }
    public static Matrix operator +(Matrix a)
    {
        return a;
    }
    public static Matrix operator -(Matrix a)
    {
        var ans = new Matrix(a.Dimensions);
        for (int i = 0; i < a.Dimensions.Height; i++)
        {
            for (int j = 0; j < a.Dimensions.Width; j++)
            {
                ans[i, j] = -a[i, j];
            }
        }
        return ans;
    }

    public static Matrix operator *(Matrix a, Matrix b)
    {
        //if (a.Dimensions.Width == a.Dimensions.Height && a.Dimensions.Height == 1)
        //{
        //    return a[0, 0] * b;
        //}
        return a.Mult(b);
    }
    public static Matrix operator *(float a, Matrix b)
    {
        return b.OperatedFunction(d => d * a);
    }
    public static Matrix operator *(Matrix b, float a)
    {
        return b.OperatedFunction(d => d * a);
    }
    public static Matrix operator /(Matrix b, float a)
    {
        return b.OperatedFunction(d => d / a);
    }

    public Matrix SillyMult(Matrix other)
    {
        if (other.Dimensions != Dimensions)
        {
            throw new MatrixNotSuitableException();
        }
        var ans = new Matrix(Dimensions);

        for (int i = 0; i < ans.Dimensions.Height; i++)
        {
            for (int j = 0; j < ans.Dimensions.Width; j++)
            {
                ans[i,j] = this[i,j] * other[i,j];
            }
        }
        return ans;

    }

    public Matrix Add(Matrix other)
    {
        if (other.Dimensions != this.Dimensions)
        {
            throw new MatrixNotSuitableException();
        }
        var ans = new Matrix(this.Dimensions);
        for (int i = 0; i < Dimensions.Height; i++)
        {
            for (int j = 0; j < Dimensions.Width; j++)
            {
                ans[i, j] = this[i, j] + other[i, j];
            }
        }
        return ans;
    }
    public Matrix Subtract(Matrix other)
    {
        if (other.Dimensions != this.Dimensions)
        {
            throw new MatrixNotSuitableException();
        }
        var ans = new Matrix(this.Dimensions);
        for (int i = 0; i < Dimensions.Height; i++)
        {
            for (int j = 0; j < Dimensions.Width; j++)
            {
                ans[i, j] = this[i, j] - other[i, j];
            }
        }
        return ans;
    }


    public override string ToString()
    {
        var ans = "";
        ans += "[";
        for (int i = 0; i < Dimensions.Height; i++)
        {
            ans += "[";
            for (int j = 0; j < Dimensions.Width; j++)
            {
                ans += this[i, j].ToString() + (j != Dimensions.Width - 1 ? ", " : "");
            }
            ans += "]";
            if (i != Dimensions.Height - 1)
            {
                ans += ",\n";
            }
        }
        ans += "]";
        return ans;
    }
    public Matrix Mult(Matrix other)
    {
        int rA = this.Dimensions.Height;
        int cA = this.Dimensions.Width; 
        int rB = other.Dimensions.Height;
        int cB = other.Dimensions.Width;
        float temp = 0;
        var ans = new Matrix(cB, rA);
        if (cA != rB)
        {
            throw new MatrixNotSuitableException();
        }
        else
        {
            for (int i = 0; i < rA; i++)
            {
                for (int j = 0; j < cB; j++)
                {
                    temp = 0;
                    for (int k = 0; k < cA; k++)
                    {
                        temp += this[i, k] * other[k, j];
                    }
                    ans[i, j] = temp;
                }
            }
            return ans;
        }
    }


    public Matrix OperatedFunction(Func<float, float> func)
    {
        var ans = new Matrix(Dimensions);

        for (int i = 0; i < ans.Dimensions.Width; i++)
        {
            for (int j = 0; j < ans.Dimensions.Height; j++)
            {
                ans[j, i] = func(this[j, i]);
            }
        }
        return ans;

    }

    public Matrix Transpose()
    {
        var ans = new Matrix(Dimensions.Height, Dimensions.Width);
        for (int i = 0; i < Dimensions.Width; i++)
        {
            for (int j = 0; j < Dimensions.Height; j++)
            {
                ans[i, j] = this[j, i];
            }
        }
        return ans;
    }

    public Matrix T => Transpose();


    public Matrix AddToAllRows(Matrix mat)
    {
        if(mat.Dimensions.Height!=1 || mat.Dimensions.Width != this.Dimensions.Width)
        {
            throw new MatrixNotSuitableException();
        }
        var ans = new Matrix(this.Dimensions);

        for (int i = 0; i < Dimensions.Width; i++)
        {
            for (int j = 0; j < Dimensions.Height; j++)
            {
                ans[i, j] +=mat[0,i];
            }
        }

        return ans;
    }


}
