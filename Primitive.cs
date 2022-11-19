using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    interface Primitive{
        void Draw(Graphics g, Transform projection, int w, int h);
        void Change(Transform t);
    }
}
