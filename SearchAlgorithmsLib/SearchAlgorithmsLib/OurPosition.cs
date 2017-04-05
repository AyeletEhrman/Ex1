using System;
using System.Collections.Generic;
using System.Text;

namespace SearchAlgorithmsLib
{
    class OurPosition
    {
        private int Row { get; set; }
        private int Col { get; set; }

        public bool Equals(OurPosition pos)
        {
            return ((this.Row == pos.Row) && (this.Col == pos.Col));
        }

        /*  public override bool Equals(object obj)
          {
              return (this.row == ((obj as Position).row) && (this.col == ((obj as Position).col)));
          }*/

        public override string ToString()
        {
            return Row.ToString() + Col.ToString();
        }
    }
}
