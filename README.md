# **Cvičení 2**

## V tomto cvičení přidáme do naší aplikace počítadlo celkové útraty

## **Jak na to**

- ### Začneme vytvořením textu v MainPage.xaml

  Přidáme nový <Label /> ze ktereho si udelame promenou tak aby jsme do nej mohli psat x:Name 
  ```xaml
    <Label x:Name="..." Text="Celkem k zaplacení: 0 Kč" FontSize="22" FontAttributes="Bold" HorizontalOptions="Center"/>
  ```
- ### Pokračujeme do MainPage.xaml.cs
  
  Presuneme se do MainPage.xaml.cs kde vytvoříme funkci private ```private void PrepocitejCelkem()```
  naše funkce první musí zmenit cenu piva ze stringu ```Cena10Entry.Text``` na double a uložit ji do proměné ->

   ```csharp
  if (double.TryParse(Cena10Entry.Text, out double cena10)&&
    double.TryParse(Cena11Entry.Text, out double cena11) &&
     double.TryParse(Cena12Entry.Text, out double cena12))
   {
     logika pro výpočet
   }
   else{  CelkemStulLabel.Text = "Zadej platnou cenu piv!";}
   ```
   poté v loopu   ```foreach(var k in Pijaci) ``` vezmeme počet piv  každého uživatele a jejich cenu  ```(k.Piv10*cena10)+(...) ``` a vložíme je do proměné ```celkem```

  Dále pošleme tento promenou ```celkem``` ve stringu  do xaml aby se změnil ```NazevPromene.Text = $"Celkem k zaplacení: {celkem} Kč;```

- ### Zkusme funkci zavolat pokaždé kdy je potřeba přepočítat celkovou útratu

  Nápověda bude potřeba přidat novou funkci ```private void OnCenaZmena(object sender, TextChangedEventArgs e)``` která se zavolá při změně textu ceny piva 
  Bude potřeba přidat ```TextChanged="OnCenaZmena"``` nekam do tohoto kusu kodu v MainPage.xaml
  ```xaml
  <HorizontalStackLayout Spacing="10" HorizontalOptions="Center" >
    <Label Text="Cena 10°:" VerticalOptions="Center"/>
    <Entry x:Name="Cena10Entry" Text="40" Keyboard="Numeric" WidthRequest="20" />
    <Label Text="Cena 11°:" VerticalOptions="Center"/>
    <Entry x:Name="Cena11Entry" Text="50" Keyboard="Numeric" WidthRequest="20" />
    <Label Text="Cena 12°:" VerticalOptions="Center"/>
    <Entry x:Name="Cena12Entry" Text="60" Keyboard="Numeric" WidthRequest="10" />
  </HorizontalStackLayout>
  ```
