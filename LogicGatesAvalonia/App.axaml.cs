using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using LogicGatesAvalonia.ViewModels;
using LogicGatesAvalonia.Views;

namespace LogicGatesAvalonia
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var vm = new MainWindowViewModel();
                desktop.MainWindow = new MainWindow(vm)
                {
                    DataContext = vm,
                    
                };
            }

            

            base.OnFrameworkInitializationCompleted();
        }
    }
}