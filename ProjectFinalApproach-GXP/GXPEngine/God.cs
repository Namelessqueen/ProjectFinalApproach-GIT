using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine
{
    internal class God : AnimationSprite
    {
        public God(string imageFile, int cols, int rows, TiledObject obj = null) : base(imageFile, cols, rows)
        {

        }
    }
}
