using System;
using System.Windows;
using System.Windows.Controls;

namespace codemin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main() {
            var win = new Window();
            var app = new App();
            var tb = new TextBlock();
			
            tb.Text="Hello, World!";
            win.Content = tb;
			
            win.Show();
            app.MainWindow = win;
            app.Run();

        }
    }
}
