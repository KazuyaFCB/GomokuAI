using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _1712912
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        




        public MainWindow()
        {
            InitializeComponent();
           // Process.Start("rapfi.exe");

            //LaunchCommandLineApp();

            //Process p = new Process();
            //p.StartInfo.FileName = "rapfi.exe";
            //p.StartInfo.Arguments = "START";
            //p.Start();
            // p.WaitForExit();

            
        }

        

        //static void LaunchCommandLineApp()
        //{
        //    // For the example
        //    const string ex1 = "start";
        //    const string ex2 = "";

        //    // Use ProcessStartInfo class
        //    ProcessStartInfo startInfo = new ProcessStartInfo();
        //    startInfo.CreateNoWindow = true;
        //    startInfo.UseShellExecute = false;
            
        //    startInfo.FileName = "D:\\Vinh175\\Hoc\\Nam3\\LapTrinhWindows\\Game Caro\\1712912\\1712912\\pbrain-rapfi.exe";
        //    //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    //MessageBox.Show(startInfo.StandardOutputEncoding.ToString());
        //    //startInfo.Arguments = "-f j -o \"" + ex1 + "\" -z 1.0 -s y " + ex2;
        //    startInfo.RedirectStandardOutput = true;// false;
        //    startInfo.RedirectStandardError = true; // false;
        //    startInfo.RedirectStandardInput = true; // new

        //    Process exeProcess = Process.Start(startInfo);
        //    exeProcess.StandardInput.WriteLine("start");
        //    exeProcess.StandardInput.WriteLine("20");
        //    string s = exeProcess.StandardOutput.ReadLine();
        //    MessageBox.Show(s);
        //    exeProcess.StandardInput.WriteLine("begin");
        //    s = exeProcess.StandardOutput.ReadLine();
        //    MessageBox.Show(s);

        //    exeProcess.Kill();

        //    exeProcess.WaitForExit();

        //    //try
        //    //{
        //    //    // Start the process with the info we specified.
        //    //    // Call WaitForExit and then the using statement will close.

        //    //    using (Process exeProcess = Process.Start(startInfo))
        //    //    {
        //    //        string s = exeProcess.StandardOutput.ReadToEnd();
        //    //        Debug.WriteLine(s);
        //    //        exeProcess.WaitForExit();
        //    //    }
        //    //}
        //    //catch
        //    //{
        //    //    // Log error.
        //    //}
        //}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
             
        }

        private void Player1vsPlayer2Button_Click(object sender, RoutedEventArgs e)
        {
            Player1vsPlayer2 Player1vsPlayer2Window = new Player1vsPlayer2();
            Player1vsPlayer2Window.Show();
        }

        private void PlayervsComputerButton_Click(object sender, RoutedEventArgs e)
        {
            PlayervsComputer PlayervsComputerWindow = new PlayervsComputer();
            PlayervsComputerWindow.Show();
        }
    }

}
