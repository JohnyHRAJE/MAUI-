# Cvičení 3

## V tomto cvičení přidáme slider kterým budeme upravovat dýško 

### Jak na to?

  Začneme tím že si v MainPage.xaml vytvoříme dva elementy ```<Label />``` ktery bude mit upravitelný text tak aby jsme do nej mohli psat velikost dýška v procentech
  potom potřebujeme neco čím dýško budeme upravovat to bude ```<Slider />``` ze ktereho potřebujeme číst hodnotu a ma ```Minimum = "0" Maximum = "30" Value="0"``` a ma promenou pro C# ```ValueChanged="OnDyskoZmeneno"```

### Dále se vrhneme do C#
  Otevreme MainPage.xaml.cs a vytvoříme novou funkci ```private void OnDyskoZmeneno(object sender, ValueChangedEventArgs e)``` která bude brát hodnotu dýška ze slideru zaoukrouhli ji pomoci  ```Math.Round()``` a přepíše jí do textu Labelu který jsme vytvořili v xaml
  jak string ```$"Spropitné: {dysko} %"``` nakonec musí také zavolat funkci prepocitaní celkove ceny stolu ```PrepocitejCelkem()``` kterou musíme upravit tak aby připočítava dýško k celkové ceně 

  ## Původní funkce:
  ```csharp
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

  ```

## Uprav funkci
  
  tak aby vracela 
  ```csharp
  CelkemStulLabel.Text = $"Celkem k zaplacení: {Math.Round(celkemSDyskem)} Kč";
  CelkemStulLabel.TextColor = Colors.Black;
  ```

  k dýšku se dostaneš takto: 
  ```csharp
  double dyskoProcenta = Math.Round(DyskoSlider?.Value ?? 0);
  ```
