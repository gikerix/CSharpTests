using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTests
{
    public interface Interfasce1
    {
        void Method1();
    }

    public interface Interfasce2
    {
        void Method1();
    }
    class SimularInterfaces : Interfasce1, Interfasce2
    {
        public void Method1()
        {
        }
    }
}
