using System;
using System.Collections.Generic;
using System.Text;

namespace SockDll 
{
    sealed  class FormAccess
    {
        private  FormAccess()
        {
        }
        public static System.Resources.ResourceManager rm = new System.Resources.ResourceManager("SockDll.Properties.Resource", System.Reflection.Assembly.GetExecutingAssembly());
    }
}
