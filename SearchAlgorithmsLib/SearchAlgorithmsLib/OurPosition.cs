using System;
using System.Collections.Generic;
using System.Text;

namespace SearchAlgorithmsLib
{
    class OurPosition
    {
        private int row { get; set; }
        private int col { get; set; }

        public bool Equals(OurPosition pos)
        {
            return ((this.row == pos.row) && (this.col == pos.col));
        }

        /*  public override bool Equals(object obj)
          {
              return (this.row == ((obj as Position).row) && (this.col == ((obj as Position).col)));
          }*/

        public override string ToString()
        {
            return row.ToString() + col.ToString();
        }
    }
}
