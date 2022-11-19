using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    class Face : Primitive{
        List<Point3D> Vertexes = new List<Point3D>();

        public Face(List<Point3D> vert)
        {
            Vertexes = vert;
        }

        public void Change(Transform t)
        {
            foreach(Point3D v in Vertexes)
                v.Change(t);
        }

        public void AddVertex(Point3D v)
        {
            Vertexes.Add(v);
        }


        public void Draw(Graphics g, Transform projection, int width, int height)
        {
            if (Vertexes.Count == 1)
                Vertexes[0].Draw(g, projection, width, height);
            else
            {
                for (int i = 0; i < Vertexes.Count - 1; ++i)
                {
                    var line = new Edge(Vertexes[i], Vertexes[i + 1]);
                    line.Draw(g, projection, width, height);
                }
                (new Edge(Vertexes[Vertexes.Count - 1], Vertexes[0])).Draw(g, projection, width, height);
            }
        }
    }
}
