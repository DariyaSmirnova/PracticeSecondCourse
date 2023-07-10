using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class IEqualityComparer :
        IEqualityComparer<int> // сравнение объектов (на данный момента типа int) на предмет равенства
    {
        private static IEqualityComparer? _comp;
        private IEqualityComparer()
        {
            //throw new NotImplementedException();

        }

        public static IEqualityComparer Comp =>
            _comp ??= new IEqualityComparer();

        public bool Equals(int x, int y)
        {
            if ((x == null) || (y == null)) 
            {
                return false;
            }
            else
            {
                return x == y;
            }
        }

        public int GetHashCode(int obj)
        {
            return obj.GetHashCode() * 23;
        }
    }
}