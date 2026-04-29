using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GrpcClientLibrary;

namespace GrpcClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    // im almost done with this lab cool currently page 7/8 ok works now had to edit some settings so it corrently read the library for grpc
    // ok I am now finished with this lab 
    public partial class MainWindow : Window
    {
        private readonly RemoteCalculator remoteCalc = new RemoteCalculator();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            int responseValue = await remoteCalc.Add(
                int.Parse(ValueATb.Text),
                int.Parse(ValueBTb.Text)
            );

            ResponseValueTb.Text = responseValue.ToString();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int responseValue = await remoteCalc.Multiply(
                int.Parse(ValueATb.Text),
                int.Parse(ValueBTb.Text)
            );

            ResponseValueTb.Text = responseValue.ToString();
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            float responseValue = await remoteCalc.Divide(
                int.Parse(ValueATb.Text),
                int.Parse(ValueBTb.Text)
            );

            ResponseValueTb.Text = responseValue.ToString();
        }
    }
}