using System.Collections.ObjectModel;
using System.ComponentModel;
namespace Dluznicek_Lite; // Zkontroluj svůj název projektu!

public partial class SouhrnPage : ContentPage
{
    // Sem uložíme hotové výpočty pro zobrazení
    public ObservableCollection<PolozkaUctenky> Uctenky { get; set; } = new ObservableCollection<PolozkaUctenky>();

    private double zbyvaVybrat = 0;
    // Všimni si, že jsme do závorek přidali parametry!
    public SouhrnPage(ObservableCollection<Kamarad> pijaci, double cena10, double cena11, double cena12, double dyskoProcenta)
    {
        InitializeComponent();

        double celkemZaVsechny = 0;

        // Projdeme každého kamaráda a spočítáme mu útratu
        foreach (var k in pijaci)
        {
            // Pokud neměl nic, přeskočíme ho, ať neukazujeme nuly
            if (k.Piv10 == 0 && k.Piv11 == 0 && k.Piv12 == 0)
                continue;

            double utrata = (k.Piv10 * cena10) + (k.Piv11 * cena11) + (k.Piv12 * cena12);
            double utrataSDyskem = Math.Round(utrata * (1 + (dyskoProcenta / 100)));
            celkemZaVsechny += utrataSDyskem;

            // Vytvoříme detail pro zobrazení (např. "3x 11°, 2x 12°")
            string detailPiv = $"{k.Piv12}x 10°, {k.Piv11}x 11°, {k.Piv12}x 12°";

            // Přidáme hotový záznam do našeho seznamu
            Uctenky.Add(new PolozkaUctenky
            {
                Jmeno = k.Jmeno,
                Detail = detailPiv,
                CastkaKc = utrataSDyskem
            });
        }


        // Propojíme náš seznam s CollectionView v XAMLu
        VysledkyView.ItemsSource = Uctenky;
        zbyvaVybrat = celkemZaVsechny;
        AktualizujZbyvaVybratLabel();
        // Zobrazíme celkový součet dole pod čarou
        CelkemVybranoLabel.Text = $"Celková útrata: {celkemZaVsechny} Kč";
    }

    private void AktualizujZbyvaVybratLabel()
    {
        if (zbyvaVybrat > 0)
        {
            CelkemVybranoLabel.Text = $"Zbývá vybrat: {zbyvaVybrat} Kč";
            CelkemVybranoLabel.TextColor = Colors.Red;
        }
        else
        {
            CelkemVybranoLabel.Text = "Vše zaplaceno! 🎉";
            CelkemVybranoLabel.TextColor = Colors.Green;
        }
    }
    private void OnZaplatilKliknuto(object sender, EventArgs e)
    {
        // Zjistíme, na které tlačítko se kliklo
        var tlacitko = (Button)sender;

        // Zjistíme, ke kterému kamarádovi toto tlačítko patří
        var polozka = (PolozkaUctenky)tlacitko.CommandParameter;


        if (!polozka.JeZaplaceno)
        {
            // 1. Změníme stav na zaplaceno
            polozka.JeZaplaceno = true;

            // 2. Odečteme jeho útratu z celkového dluhu
            zbyvaVybrat -= polozka.CastkaKc;

            // 3. Vizuálně upravíme tlačítko (zezelená a už nepůjde kliknout)
            tlacitko.Text = "✔️";
            tlacitko.BackgroundColor = Colors.LightGreen;
            tlacitko.IsEnabled = false;
            polozka.BarvaTextu = Colors.DarkGreen;

            // 4. Přepíšeme text dole na obrazovce
            AktualizujZbyvaVybratLabel();
        }
    }
}

// Pomocná třída jen pro zobrazení dat na této stránce
public class PolozkaUctenky : INotifyPropertyChanged
{
    public string Jmeno { get; set; }
    public string Detail { get; set; }
    public double CastkaKc { get; set; }
    public bool JeZaplaceno { get; set; }

    private Color _barvaTextu = Colors.DarkRed; // Výchozí barva je červená
    public Color BarvaTextu
    {
        get => _barvaTextu;
        set
        {
            _barvaTextu = value;
            OnPropertyChanged(nameof(BarvaTextu)); // Řekneme UI, ať se překreslí
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
