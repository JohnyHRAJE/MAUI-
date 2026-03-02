# **Cvičení 1**

Naklonujte si tento depozitář do svého visual studia, a poté začněte pracovat na těchto úkolech:

- ### Začneme přidání dalších druhů piva 11°,12° v souboru Class1.cs

    Podle této logiky pridejte do classu piva 11 a 12
    ```csharp
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
    ```

- ### Dále se posuneme do Souboru MainPage.xaml

  Zde do těchto dvou částí kodu přidáme pivo 11 a 12
  ```xaml
  <HorizontalStackLayout Spacing="10" HorizontalOptions="Center" >
      <Label Text="Cena 10°:" VerticalOptions="Center"/>
      <Entry x:Name="Cena10Entry" Text="40" Keyboard="Numeric" WidthRequest="20" />
        <!-- zde doplňte cenu pro pivo 11 a 12 -->
  </HorizontalStackLayout>
  ```
  ```xaml
  <Grid ColumnDefinitions="2*, 1*, 1*, 1*, 1*,1*,1*">
    <Label Text="{Binding Jmeno}" FontSize="18" FontAttributes="Bold" VerticalOptions="Center" Grid.Column="0"/>

    <Button Text="+ 10" Clicked="OnPridat10" CommandParameter="{Binding .}" Grid.Column="1" Padding="0"/>
    <Label Text="{Binding Piv10}" FontSize="20" VerticalOptions="Center" HorizontalOptions="Center" Grid.Column="2"/>

    <!--zde doplňte tlačítka a labely pro 11° a 12° pivo tak aby se zobrazovaly v dalších sloupcívh-->
  </Grid>
  ```
- ### Stále nam ale chybí logika za tlačítky

    takže jí zkus v MainPage.xaml.cs pridat
  
    tento kod ti možná napoví
    ```csharp
    private void OnPridat10(object sender, EventArgs e)
    {
      var tlacitko = (Button)sender;
      var kamarad = (Kamarad)tlacitko.CommandParameter; 
      kamarad.Piv10++; 
    }
    ```
    



