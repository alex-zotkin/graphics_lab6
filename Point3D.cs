using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6{
    class Point3D : Primitive{

        private double[] coordinate_matrix = new double[] { 0, 0, 0, 1 };
        public double X { 
            get { return coordinate_matrix[0]; } 
            set { coordinate_matrix[0] = value; } 
        }
        public double Y { 
            get { return coordinate_matrix[1]; } 
            set { coordinate_matrix[1] = value; } 
        }
        public double Z { 
            get { return coordinate_matrix[2]; } 
            set { coordinate_matrix[2] = value; } 
        }

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3D(double[] arr)
        {
            coordinate_matrix = arr;
        }

        public static Point3D PointTo3D(Point point)
        {
            return new Point3D(point.X, point.Y, 0);
        }

        public void Change(Transform t)
        {
            double[] new_coordinates = new double[4];

            for (int i = 0; i < 4; i++)
            {
                new_coordinates[i] = 0;

                for (int j = 0; j < 4; j++)
                    new_coordinates[i] += coordinate_matrix[j] * t.Matrix[j, i];
            }

            coordinate_matrix = new_coordinates;
        }

        public Point3D Transform(Transform t)
        {
            var p = new Point3D(X, Y, Z);
            p.Change(t);
            return p;
        }

        public void Draw(Graphics g, Transform projection, int w, int h)
        {
            var projected = Transform(projection);
            var x = (projected.X + 1) / 2 * w;
            var y = (-projected.Y + 1) / 2 * h;
            g.DrawEllipse(new Pen(Color.Black, 1), (float)x, (float)y, 2, 2);
        }
         
        public Point3D ViewTransform(int w, int h)
        {
            var x = (X / coordinate_matrix[3] + 1) / 2 * w;
            var y = (-Y / coordinate_matrix[3] + 1) / 2 * h;
            return new Point3D(x, y, Z);
        }
    
    }
}
