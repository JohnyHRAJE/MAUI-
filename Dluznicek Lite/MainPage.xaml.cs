
using System.Collections.ObjectModel;

namespace Dluznicek_Lite;

public partial class MainPage : ContentPage
{
    // Speciální seznam pro MAUI - když se do něj něco přidá, UI se hned překreslí
    public ObservableCollection<Kamarad> Pijaci { get; set; } = new ObservableCollection<Kamarad>();

    public MainPage()
    {
        InitializeComponent();
        SeznamView.ItemsSource = Pijaci;
    }

    private void OnPridatKamarada(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(NoveJmenoEntry.Text))
        {
            Pijaci.Add(new Kamarad { Jmeno = NoveJmenoEntry.Text, Piv10 = 0 });
            NoveJmenoEntry.Text = ""; // Vyčistíme políčko pro dalšího
        }
    }
    private void OnPridat10(object sender, EventArgs e)
    {
        var tlacitko = (Button)sender;
        var kamarad = (Kamarad)tlacitko.CommandParameter; // Zjistíme, koho jsme zaklikli
        kamarad.Piv10++; // Přidáme pivo
    }
    // Zde vytvořte stejnou logiku  pro  pivo 11 a 12
    
}