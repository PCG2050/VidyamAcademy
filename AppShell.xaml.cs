namespace VidyamAcademy
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            this.BindingContext = new AppShellViewModel();
        }
    }
}
