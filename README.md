# **Cvičení 1**
- Začneme přidání dalších druhů piva 11° 12° v souboru s názvem Class1.cs
  Zde podle logiky kterou je pivo definováno pridamé další 2 poté
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
-
-  se přesuneme do souboru MainPage.xaml ve kterém zase přidáme pivo 11 a 12 poté jen doděláme logiku v souboru MainPage.xaml.cs
