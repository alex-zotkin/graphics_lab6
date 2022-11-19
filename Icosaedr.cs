using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    class Icosaedr : Primitive
    {
        private List<Point3D> Vertexes = new List<Point3D>();
        private List<Face> Faces = new List<Face>();

        public Icosaedr()
        {
            double scale = 0.5;
            // радиус описанной сферы
            double R = (scale * Math.Sqrt(2.0 * (5.0 + Math.Sqrt(5.0)))) / 4;
            // радиус вписанной сферы
            double r = (scale * Math.Sqrt(3.0) * (3.0 + Math.Sqrt(5.0))) / 12;

            Vertexes = new List<Point3D>();

            for (int i = 0; i < 5; ++i)
            {
                Vertexes.Add(new Point3D(
                    r * Math.Cos(2 * Math.PI / 5 * i),
                    R / 2,
                    r * Math.Sin(2 * Math.PI / 5 * i)));
                Vertexes.Add(new Point3D(
                    r * Math.Cos(2 * Math.PI / 5 * i + 2 * Math.PI / 10),
                    -R / 2,
                    r * Math.Sin(2 * Math.PI / 5 * i + 2 * Math.PI / 10)));
            }

            Vertexes.Add(new Point3D(0, R, 0));
            Vertexes.Add(new Point3D(0, -R, 0));

            // середина
            for (int i = 0; i < 10; ++i)
                Faces.Add(new Face(new List<Point3D> { Vertexes[i], Vertexes[(i + 1) % 10], Vertexes[(i + 2) % 10] }));

            for (int i = 0; i < 5; ++i)
            {
                // верхняя часть
                Faces.Add(new Face(new List<Point3D> { Vertexes[2 * i], Vertexes[10], Vertexes[(2 * (i + 1)) % 10] }));
                // нижняя часть
                Faces.Add(new Face(new List<Point3D> { Vertexes[2 * i + 1], Vertexes[11], Vertexes[(2 * (i + 1) + 1) % 10] }));
            }
        }

        public Point3D Center()
        {
            Point3D p = new Point3D(0, 0, 0);

            foreach (Point3D v in Vertexes)
            {
                p.X += v.X;
                p.Y += v.Y;
                p.Z += v.Z;
            }

            p.X /= Vertexes.Count();
            p.Y /= Vertexes.Count();
            p.Z /= Vertexes.Count();
            return p;
        }

        public void Change(Transform t)
        {
            foreach (var point in Vertexes)
                point.Change(t);
        }

        public void Draw(Graphics g, Transform projection, int width, int height)
        {
            if (Vertexes.Count != 12) return;

            foreach (var Face in Faces)
                Face.Draw(g, projection, width, height);
        }
    }
}
