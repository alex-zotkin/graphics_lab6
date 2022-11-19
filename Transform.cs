using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    class Transform
    {
        public double[,] Matrix = new double[4, 4];

        public Transform()
        {
            Matrix = Unit().Matrix;
        }

        public Transform(double[,] matrix)
        {
            Matrix = matrix;
        }

        public static Transform Unit()
        {
            return new Transform(
                new double[,] 
                {
                    { 1, 0, 0, 0 },
                    { 0, 1, 0, 0 },
                    { 0, 0, 1, 0 },
                    { 0, 0, 0, 1 }
                }
            );
        }

        public static Transform RotateX(double angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Transform(
                new double[,]
                {
                    { 1, 0, 0, 0 },
                    { 0, cos, -sin, 0 },
                    { 0, sin, cos, 0 },
                    { 0, 0, 0, 1 }
                });
        }

        public static Transform RotateY(double angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Transform(
                new double[,]
                {
                    { cos, 0, sin, 0 },
                    { 0, 1, 0, 0 },
                    { -sin, 0, cos, 0 },
                    { 0, 0, 0, 1 }
                });
        }

        public static Transform RotateZ(double angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Transform(
                new double[,]
                {
                    { cos, -sin, 0, 0 },
                    { sin, cos, 0, 0 },
                    { 0, 0, 1, 0 },
                    { 0, 0, 0, 1 }
                });
        }

        public static Transform RotateLine(Edge edge, double angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            double l = Math.Sign(edge.B.X - edge.A.X);
            double m = Math.Sign(edge.B.Y - edge.A.Y);
            double n = Math.Sign(edge.B.Z - edge.A.Z);

            double length = Math.Sqrt(l * l + m * m + n * n);
            l /= length; m /= length; n /= length;

            return new Transform(
                new double[,]
                {
                   { l * l + cos * (1 - l * l),   l * (1 - cos) * m + n * sin,   l * (1 - cos) * n - m * sin,  0  },
                   { l * (1 - cos) * m - n * sin,   m * m + cos * (1 - m * m),    m * (1 - cos) * n + l * sin,  0 },
                   { l * (1 - cos) * n + m * sin,  m * (1 - cos) * n - l * sin,    n * n + cos * (1 - n * n),   0 },
                   {            0,                            0,                             0,               1   }
                });

        }

        public static Transform Scale(double scale_x, double scale_y, double scale_z)
        {
            return new Transform(
                new double[,] {
                    { scale_x, 0,        0,    0 },
                    { 0,    scale_y,     0,    0 },
                    { 0,       0,     scale_z, 0 },
                    { 0,       0,        0,    1 }
                });
        }

        public static Transform Translate(double dx, double dy, double dz)
        {
            return new Transform(
                new double[,]
                {
                    { 1, 0, 0, 0 },
                    { 0, 1, 0, 0 },
                    { 0, 0, 1, 0 },
                    { dx, dy, dz, 1 },
                });
        }


        public static Transform ReflectX()
        {
            return Scale(-1, 1, 1);
        }

        public static Transform ReflectY()
        {
            return Scale(1, -1, 1);
        }

        public static Transform ReflectZ()
        {
            return Scale(1, 1, -1);
        }

        public static Transform OrthographicXYProjection()
        {
            return Unit();
        }

        public static Transform OrthographicXZProjection()
        {
            return Unit() * RotateX(-Math.PI / 2);
        }

        public static Transform OrthographicYZProjection()
        {
            return Unit() * RotateY(Math.PI / 2);
        }

        public static Transform IsometricProjection()
        {
            return Unit() * RotateY(Math.PI / 4) * RotateX(-Math.PI / 4);
        }

        public static Transform PerspectiveProjection()
        {
            return new Transform(
                new double[,] {
                    { 1, 0, 0, 0 },
                    { 0, 1, 0, 0 },
                    { 0, 0, 0, 2 },
                    { 0, 0, 0, 1 }
                });
        }

        public static Transform operator *(Transform t1, Transform t2)
        {
            double[,] new_matrix = new double[4, 4];

            for (int i = 0; i < 4; ++i)
                for (int j = 0; j < 4; ++j)
                {
                    new_matrix[i, j] = 0;
                    for (int k = 0; k < 4; ++k)
                        new_matrix[i, j] += t1.Matrix[i, k] * t2.Matrix[k, j];
                }
            return new Transform(new_matrix);
        }
    }

}

