using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI
{
    interface ISingleModel : IModel
    {
        string Name { get; set;}
    }
}
