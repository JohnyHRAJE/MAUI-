using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dluznicek_Lite
{
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
        private int _piv11;
        public int Piv11
            {
            get => _piv11;
            set
            {
                if (_piv11 != value)
                {
                    _piv11 = value;
                    OnPropertyChanged(nameof(Piv11));
                }
            }
        }
        private int _piv12;
        public int Piv12
        {
            get => _piv12;
            set
            {
                if (_piv12 != value)
                {

                    _piv12 = value;
                    OnPropertyChanged(nameof(Piv12));
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
