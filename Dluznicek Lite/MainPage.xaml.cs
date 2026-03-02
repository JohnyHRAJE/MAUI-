
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
        
    }

    private void OnPridat11(object sender, EventArgs e)
    {
        var tlacitko = (Button)sender;
        var kamarad = (Kamarad)tlacitko.CommandParameter; 
        kamarad.Piv11++; 
        
    }

    private void OnPridat12(object sender, EventArgs e)
    {
        var tlacitko = (Button)sender;
        var kamarad = (Kamarad)tlacitko.CommandParameter;
        kamarad.Piv12++;
        
    }
    
    
    
   

    

    

    

   
}