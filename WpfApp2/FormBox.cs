using System;
using System.Windows.Controls;

namespace WpfApp2
{
    internal class FormBox
    {
        public static implicit operator FormBox(ComboBox v)
        {
            throw new NotImplementedException();
        }
    }
}