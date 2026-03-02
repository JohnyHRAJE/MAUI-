
using System.Collections.ObjectModel;

namespace Dluznicek_Lite;

public partial class MainPage : ContentPage
{
    // Speciální seznam pro MAUI - když se do něj něco přidá, UI se hned překreslí
    public ObservableCollection<Kamarad> Pijaci { get; set; } = new ObservableCollection<Kamarad>();

    public MainPage()
    {
        InitializeComponent();
        // Řekneme CollectionView v XAMLu, odkud má brát data
        SeznamView.ItemsSource = Pijaci;
    }

    private void OnPridatKamarada(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(NoveJmenoEntry.Text))
        {
            Pijaci.Add(new Kamarad { Jmeno = NoveJmenoEntry.Text, Piv10 = 0,Piv11 = 0, Piv12 = 0 });
            NoveJmenoEntry.Text = ""; // Vyčistíme políčko pro dalšího
        }
    }
    private void OnPridat10(object sender, EventArgs e)
    {
        var tlacitko = (Button)sender;
        var kamarad = (Kamarad)tlacitko.CommandParameter; 
        kamarad.Piv10++; 
        PrepocitejCelkem();
    }

    private void OnPridat11(object sender, EventArgs e)
    {
        var tlacitko = (Button)sender;
        var kamarad = (Kamarad)tlacitko.CommandParameter; 
        kamarad.Piv11++; 
        PrepocitejCelkem();
    }

    private void OnPridat12(object sender, EventArgs e)
    {
        var tlacitko = (Button)sender;
        var kamarad = (Kamarad)tlacitko.CommandParameter;
        kamarad.Piv12++;
        PrepocitejCelkem();
    }
    
    private void OnCenaZmenena(object sender, TextChangedEventArgs e)
    {
        PrepocitejCelkem();
    }
    //přidej funkci která bude upraovat text <Labelu /> na hodnotu slideru dýška
    
    //uprav funkci PrepocitejCelkem() tak, aby vracela celkovou cenu  i s dýškem
    private void PrepocitejCelkem()
    {
        if (double.TryParse(Cena10Entry.Text, out double cena10)&&
            double.TryParse(Cena11Entry.Text, out double cena11) &&
             double.TryParse(Cena12Entry.Text, out double cena12))
        {
            double celkem = 0;
            foreach (var k in Pijaci)
            {
                celkem += (k.Piv10 * cena10) + (k.Piv11 * cena11) + (k.Piv12 * cena12);
            }
            CelkemStulLabel.Text = $"Celkem k zaplacení: {celkem} Kč";
            CelkemStulLabel.TextColor = Colors.Black;

        }
        else
        {
            CelkemStulLabel.Text = "Zadej platnou cenu piv!";
            CelkemStulLabel.TextColor = Colors.Red;
        }





    }

    

    

  
}