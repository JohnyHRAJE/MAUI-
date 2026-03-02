# Cvičení 4

## Tady si vyzkoušíme jak vytvořit navigaci na další stránku

## 1.  Vytvoř tlačítko

  V MainPage.xaml udělej tlačítko které ma ```Text="Zaplatit a rozpočítat" Clicked="OnUkoncitKliknuto```
  
## 2. ## Poté se přesuneme do MainPage.xaml.cs
   
  Zde vytvoříme funkci která nás po kliknutí tlačítka pošle na další stránku:
   ```csharp
   private async void OnUkoncitKliknuto(object sender, EventArgs e)
    {
        // Zkontrolujeme, jestli máme správně zadané ceny
        if (double.TryParse(Cena10Entry.Text, out double cena10) &&
            double.TryParse(Cena11Entry.Text, out double cena11) &&
            double.TryParse(Cena12Entry.Text, out double cena12))
        {
            double dyskoProcenta = Math.Round(DyskoSlider.Value);
  // await Navigation.PushAsync() nas pošle na novou stránku pokaždé ji ale vytvoří znovu takže se učet neuloži když se vratíme zpět
            await Navigation.PushAsync(new SouhrnPage(Pijaci, cena10, cena11, cena12, dyskoProcenta));
        }
        else
        {
            await DisplayAlertAsync("Chyba", "Zkontroluj zadané ceny piv!", "OK");
        }
    }
   ```
## 3.  Musíme si vytvořit novou stranku

  To uděláme tak že pravým tlačítkem klikneme na Dlužníček Lite najdeme přidat a vybereme Nová položka
  
  <img width="235" height="489" alt="image" src="https://github.com/user-attachments/assets/f78f8c42-5c2f-432d-a458-e2c92afc89c1" />

  <img width="661" height="796" alt="image" src="https://github.com/user-attachments/assets/6b2f29a1-4aab-471a-9279-6925f423a342" />
  
  Orevře se nám okno ve kterém najdeme  .NET MAUI ContentPage (XAML) kterou pojmenujeme SouhrnPage.xaml

  <img width="939" height="651" alt="image" src="https://github.com/user-attachments/assets/f1025351-7563-43ca-a2f5-3dc71bd7c8bd" />

  To nám vytvořilo novou stranku SouhrnPage.xaml kterou budeme dale upravovat 

## 4. Úprava SouhrnPage.xaml

  Zkuste na této stránce sami vytvořit nadpis v xaml s textem ``` Kdo co Platí ``` 

## 5. Jelikož stránka potřebuje konstrukter k tomu aby jsme se naní byly schopni přepnout tak se vrhneme do jejiho C# v SouhrnPage.xaml

  První si vytvoříme class podle ktereho budeme ukladat hodnoty do kolekce:
  ```csharp
      public class PolozkaUctenky : INotifyPropertyChanged
  {
      public string Jmeno { get; set; }
      public string Detail { get; set; }
      public double CastkaKc { get; set; }
      
  
      
  
      public event PropertyChangedEventHandler PropertyChanged;
      protected void OnPropertyChanged(string propertyName) =>
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  ``` 
  Dále pak potřebujeme vytvořit konstruktér stránky to uděláme takto:
  ```csharp
  public ObservableCollection<PolozkaUctenky> Uctenky { get; set; } = new ObservableCollection<PolozkaUctenky>();
  private double zbyvaVybrat = 0;
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

        
        string detailPiv = $"{k.Piv12}x 10°, {k.Piv11}x 11°, {k.Piv12}x 12°";
        Uctenky.Add(new PolozkaUctenky
        {
            Jmeno = k.Jmeno,
            Detail = detailPiv,
            CastkaKc = utrataSDyskem
        });
  ``` 
 Musíme náš seznam propojit s CollectionView v XAMLu a to uděláme takto:
 
```csharp
VysledkyView.ItemsSource = Uctenky;
zbyvaVybrat = celkemZaVsechny;
CelkemVybranoLabel.Text = $"Celková útrata: {celkemZaVsechny} Kč";
```

Teď se vrhneme na vytvoření CollectionView v XAMLu tedy:

```xaml
<CollectionView x:Name="VysledkyView" HeightRequest="400">
    <CollectionView.ItemTemplate>
        <DataTemplate>
            <Border Margin="0,5" Padding="15"  BackgroundColor="#F8F9FA">
                <Grid ColumnDefinitions="2*, Auto, Auto, Auto" ColumnSpacing="10">

                    <VerticalStackLayout Grid.Column="0" VerticalOptions="Center">
                        <Label Text="{Binding Jmeno}" FontSize="18" FontAttributes="Bold" />
                        <Label Text="{Binding Detail}" FontSize="14" TextColor="Gray" />
                    </VerticalStackLayout>

                    <Label Text="{Binding CastkaKc, StringFormat='{0} Kč'}" 
                            FontSize="20" 
                            FontAttributes="Bold" 
                            TextColor="{Binding BarvaTextu}" 
                            VerticalOptions="Center" 
                            HorizontalOptions="End" 
                            Grid.Column="2" />
                </Grid>
            </Border>
        </DataTemplate>
    </CollectionView.ItemTemplate>
</CollectionView>
```

Teď už jen zkuste přidat label který ukazuje kolik nám chybý vybrat od ostatních za účet

## Řešení pro ```<Label/> ```

```xaml
<Label x:Name="CelkemVybranoLabel" 
       FontSize="22" 
       FontAttributes="Bold" 
       HorizontalOptions="Center" 
       TextColor="DarkGreen" />
```


  




  
