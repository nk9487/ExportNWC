using System;
using System.Windows;

namespace ExportNWC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Check(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Check-1");
        }
    }
}
