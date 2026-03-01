
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
        var kamarad = (Kamarad)tlacitko.CommandParameter; // Zjistíme, koho jsme zaklikli
        kamarad.Piv10++; // Přidáme pivo
        PrepocitejCelkem();
    }

    private void OnPridat11(object sender, EventArgs e)
    {
        var tlacitko = (Button)sender;
        var kamarad = (Kamarad)tlacitko.CommandParameter; // Zjistíme, koho jsme zaklikli
        kamarad.Piv11++; // Přidáme pivo
        PrepocitejCelkem();
    }

    private void OnPridat12(object sender, EventArgs e)
    {
        var tlacitko = (Button)sender;
        var kamarad = (Kamarad)tlacitko.CommandParameter;
        kamarad.Piv12++;
        PrepocitejCelkem();
    }
    // Tato metoda se zavolá KDYKOLIV se změní číslo v políčku s cenou
    private void OnCenaZmenena(object sender, TextChangedEventArgs e)
    {
        PrepocitejCelkem();
    }
    private void OnDyskoZmeneno(object sender, ValueChangedEventArgs e)
    {
        double dysko = Math.Round(e.NewValue);
        DyskoLabel.Text = $"Spropitné: {dysko} %";
        PrepocitejCelkem(); // Hned přepočítáme celkovou útratu stolu
    }

    private void PrepocitejCelkem()
    {
        // Zkusíme přečíst ceny z políček. Pokud tam nic není, TryParse selže.
        if (double.TryParse(Cena11Entry.Text, out double cena11) &&
            double.TryParse(Cena12Entry.Text, out double cena12) &&
            double.TryParse(Cena10Entry.Text, out double cena10))
        {
            double celkem = 0;
            foreach (var k in Pijaci)
            {
                celkem += (k.Piv10 * cena10)+(k.Piv11 * cena11) + (k.Piv12 * cena12);
            }
            double dyskoProcenta = Math.Round(DyskoSlider?.Value ?? 0);
            double celkemSDyskem = celkem * (1 + (dyskoProcenta / 100));

            CelkemStulLabel.Text = $"Celkem k zaplacení: {Math.Round(celkemSDyskem)} Kč";
            CelkemStulLabel.TextColor = Colors.Black;
        }
        else
        {
            // Pokud někdo smaže cenu úplně, upozorníme ho
            CelkemStulLabel.Text = "Zadej platnou cenu piv!";
            CelkemStulLabel.TextColor = Colors.Red; // Zčervená to, aby si toho všiml
        }
    }

    

    

    private async void OnUkoncitKliknuto(object sender, EventArgs e)
    {
        // Zkontrolujeme, jestli máme správně zadané ceny
        if (double.TryParse(Cena10Entry.Text, out double cena10) &&
            double.TryParse(Cena11Entry.Text, out double cena11) &&
            double.TryParse(Cena12Entry.Text, out double cena12))
        {
            double dyskoProcenta = Math.Round(DyskoSlider.Value);
            // Přejdeme na druhou stránku a pošleme jí náš seznam lidí a ceny piv
            await Navigation.PushAsync(new SouhrnPage(Pijaci, cena10, cena11, cena12, dyskoProcenta));
        }
        else
        {
            await DisplayAlertAsync("Chyba", "Zkontroluj zadané ceny piv!", "OK");
        }
    }
}