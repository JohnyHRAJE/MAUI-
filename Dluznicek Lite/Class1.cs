using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dluznicek_Lite
{
    //pridejte do classu piva 11 a 12 stejne jako pivo 10
    public class Kamarad : INotifyPropertyChanged
    {
        public string Jmeno { get; set; }

        private int _piv10;
        public int Piv10
        {
            get => _piv10;
            set
            {
                if (_piv10 != value)
                {
                    _piv10 = value;
                    OnPropertyChanged(nameof(Piv10));
                }
            }
        }
        


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    
    }
}
