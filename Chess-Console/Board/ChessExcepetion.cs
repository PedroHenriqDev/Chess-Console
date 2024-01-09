using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board
{
    class ChessExcepetion : Exception
    {
        public ChessExcepetion(string message) : base(message)
        {
        }
    }
}
