using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI
{
    interface ISettingsMenuModel : IMenuModel
    {
        string ServerIP { get; set; }
        int ServerPort { get; set; }
        int SearchAlgorithm { get; set; }
    }
}
