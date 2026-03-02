# Cvičení 5

## Checkboxi u lidí kteří zaplatili 

V XAMLu musíme vytvořit tlačítku u každáho ktere se po kliknutí zabarví zeleně tak aby bylo poznat že dotyčný již zaplatil a  ubere částku z celkové částky která je potřeba vybrat

```xaml
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

    <!--Zde přidej tlačítko-->
</Grid>
```

Do classy PolozkaUctenky musíme přidat tyto věci 

```csharp
public bool JeZaplaceno { get; set; }

private Color _barvaTextu = Colors.DarkRed; 
public Color BarvaTextu
{
    get => _barvaTextu;
    set
    {
        _barvaTextu = value;
        OnPropertyChanged(nameof(BarvaTextu)); 
    }
}
```

Dale pak musíme vytvořit funkci která po kliknutí na vytvořené tlačítko zmeni stav JeZaplaceno na True ubere CastkaKc od zbyvaVybrat a zmení  text tlačítka na "✔️" a jeho pozadí na světle zelenou a vypne možnost na něj kliknout pomocí ```tlacitko.IsEnabled = false; ``` jeste pak mužete zkusit upravit barvu castky 

Řešení:
<details>
  <summary>💡 Řešení úlohy</summary>
  C#:

  ```
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
 ```
</details>


Poslední vec kterou na této stránce musíme vytovřit je změna textu s celkovou částkou kterou budeme volat vždy když někoho zaklikneme, a taky ji přidáme do konstrukteru aby se častka zobrazila po přechodu na tuto obrazovku
## Zkuste vytvořit Label a kod pro tento objekt 

## Řešení 
  <details>
  <summary>💡 Řešení úlohy</summary>
  
  C#:
  ```csharp
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
```
XAML
```
<Label x:Name="CelkemVybranoLabel" 
       FontSize="22" 
       FontAttributes="Bold" 
       HorizontalOptions="Center" 
       TextColor="DarkGreen" />
```
</details>


