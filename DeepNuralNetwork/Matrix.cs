using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeepNuralNetworks;

public struct Matrix1D : IEnumerable<(float value, int pos)>
{
    private float[] data { get; }
    public int Size { get; }


    public float this[int i]
    {
        get => data[i];
        set => data[i] = value;
    }
    public Matrix1D(int size)
    {
        this.Size = size;
        this.data = new float[size];
    }
    public Matrix1D(params float[] data)
    {
        this.data = data;
        Size = data.Length;
    }

    public static Matrix1D HotVector(int len,int pos)
    {
        if (len <= pos)
        {
            throw new ArgumentException("The len should be bigger than the pos!");
        }
        var ans = new Matrix1D(len);
        ans[pos] = 1;

        return ans;
    }

    public static implicit operator Matrix2D(Matrix1D a)
    {
        return new Matrix2D(a.data);
    }

    public float DotProduct(Matrix1D matrix)
    {
        if (matrix.Size != Size)
        {
            throw new ArgumentException($"{nameof(matrix)} is not with the right size");
        }
        var t = this;
        return MathUtils.Sigma(0, Size - 1, (i) => (matrix[i] * t[i]));
    }
    public Matrix1D DotProduct(Matrix2D matrix2D)
    {
        if (matrix2D.Width != Size)
        {
            throw new ArgumentException("The matrices should be the same width!");
        }
        var ans = new Matrix1D(matrix2D.Height);

        for (int y = 0; y < matrix2D.Height; y++)
        {
            for (int x = 0; x < Size; x++)
            {
                ans[y] += this[x] * matrix2D[x, y];
            }
        }
        return ans;

    }

    public Matrix2D ToMatrix2D()
    {
        return new Matrix2D(data);
    }

    public override string ToString()
    {
        var ans = "[ ";

        foreach (var item in data)
        {
            ans += item + ", ";
        }
        ans += " ]";
        return ans;
    }

    public IEnumerator<(float value, int pos)> GetEnumerator()
    {
        for (int i = 0; i < Size; i++)
        {
            yield return (this[i], i);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static Matrix1D operator +(Matrix1D a) => a;
    public static Matrix1D operator -(Matrix1D a) => a.data.Select(a => -a).ToMatrix1D();


    public static Matrix1D operator +(Matrix1D a, Matrix1D b)
    {
        if (a.Size != b.Size)
        {
            throw new ArgumentException("The matrices should be at the same size.");
        }
        var ans = new Matrix1D(a.Size);
        for (int i = 0; i < a.Size; i++)
        {
            ans[i] = a[i] + b[i];
        }
        return ans;

    }
    public static Matrix1D operator -(Matrix1D a, Matrix1D b) => a + -b;
}

public struct Matrix2D : IEnumerable<(float value, int row, int column)>
{
    private float[,] data { get; }
    public int Width;
    public int Height;
    public float this[int y, int x]
    {
        get => data[y, x];
        set => data[y, x] = value;
    }
    public Matrix2D(float[,] data)
    {

        (Height, Width) = (data.GetLength(0), data.GetLength(1));

        //this.data = new float[Height,Width];

        this.data = data;
    }
    public Matrix2D(float[] data)
    {
        this.data = new float[data.Length, 1];
        (Width, Height) = (data.Length, 1);
        for (int i = 0; i < data.Length; i++)
        {
            this[0, i] = data[i];
        }
    }
    public Matrix2D(int sizeY, int sizeX)
    {
        (Height, Width) = (sizeY, sizeX);
        this.data = new float[Height, Width];
    }
    public Matrix2D((int, int) size) : this(size.Item1, size.Item2)
    {

    }
    public IEnumerable<Matrix1D> ToMatrices1D(bool is_row = true)
    {
        var t = this;
        if (!is_row)
        {
            t =Tranpose();
        }
        Matrix1D mat;
        for (int y = 0; y < Height; y++)
        {
            mat = new Matrix1D(Width);
            for (int x = 0; x < Width; x++)
            {
                mat[x] = t[y, x];
            }
            yield return mat;
        }

    }
    public Matrix2D(IEnumerable<Matrix1D> mats)
    {
        var l = mats.ToList();
        if (!mats.All(a => l[0].Size == a.Size))
        {
            throw new ArgumentException("the matrices aren't on the same size!");
        }
        (Width, Height) = ( l[0].Size, l.Count);


        data = new float[Height,Width];


        for (int y = 0;y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                data[y, x] = l[y][x];
            }
        }

    }


    public static Matrix2D operator *(Matrix2D a, Matrix2D b)
    {
        return a.Mult(b);
    }

    public Matrix2D Tranpose()
    {
        var ans = new Matrix2D(Width, Height);
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                ans[x, y] = this[y, x];
            }
        }
        return ans;
    }
    public static Matrix2D operator +(Matrix2D a, Matrix1D b)
    {
        if (a.Width != b.Size)
        {
            throw new ArgumentException("The matrices cant be added.");
        }

        var ans = new Matrix2D(a.Height, a.Width);
        for (int i = 0; i < a.Width; i++)
        {

            for (int y = 0; y < a.Height; y++)
            {
                ans[i, y] = a[i, y] + b[i];
            }
        }
        return ans;
    }

    public static Matrix2D operator -(Matrix2D a, Matrix1D b) => a + -b;


    public static Matrix2D operator +(Matrix2D a) => a;
    public static Matrix2D operator -(Matrix2D a)
    {
        var ans = new Matrix2D(a.Height, a.Width);

        for (int i = 0; i < a.Width; i++)
        {
            for (int j = 0; j < a.Height; j++)
            {
                ans[i, j] = -a[i, j];
            }
        }
        return ans;
    }

    public static Matrix2D operator +(Matrix2D a, Matrix2D b)
    {
        if (!((a.Width == b.Width && a.Height == b.Height) || ((b.Width == 1 && b.Height == a.Height) || (b.Height == 1 && b.Width == a.Width))))
        {
            throw new ArgumentException("Those matrices can't be added");
        }
        var ans = new Matrix2D((a.Height, a.Width));

        for (int x = 0; x < a.Width; x++)
        {
            for (int y = 0; y < a.Height; y++)
            {
                ans[y, x] = a[y, x] + b[x % b.Height, y % b.Width];
            }
        }

        return ans;

    }


    public Matrix2D Mult(Matrix2D other)
    {
        int rA = this.Height;
        int cA = this.Width;
        int rB = other.Height;
        int cB = other.Width;
        float temp = 0;
        Matrix2D kHasil = new Matrix2D(rA, cB);
        if (cA != rB)
        {
            throw new ArgumentException("matrix can't be multiplied !!");
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
                    kHasil[i, j] = temp;
                }
            }
            return kHasil;
        }
    }

    public override string ToString()
    {
        var ans = "[";
        for (int j = 0; j < Height; j++)
        {
            ans += ("[ ");
            for (int i = 0; i < Width; i++)
            {
                ans += this[j, i].ToString() + ", ";
            }
            ans += ("],");
            if (j != Height - 1)
            {
                ans += "\n";
            }
        }
        ans += "]";
        return ans;
    }

    public IEnumerator<(float value, int row, int column)> GetEnumerator()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                yield return (this[y, x], y, x);
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

