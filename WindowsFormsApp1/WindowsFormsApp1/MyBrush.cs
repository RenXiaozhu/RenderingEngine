using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.Remoting;

namespace MyBrush
{
    public class Class2 : Brush
    {
        public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override ObjRef CreateObjRef(Type requestedType)
        {
            return base.CreateObjRef(requestedType);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override object InitializeLifetimeService()
        {
            return base.InitializeLifetimeService();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }

}
