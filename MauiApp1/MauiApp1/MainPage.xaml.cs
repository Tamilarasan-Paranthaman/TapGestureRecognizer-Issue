namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            CustomControl customControl = new CustomControl();
            this.Content = customControl;
        }


    }
}
