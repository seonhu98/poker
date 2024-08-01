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
using OpenCvSharp;

namespace pokerProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Mat k = new Mat();
            //k = Cv2.ImRead("C:\\Users\\lms\\source\\repos\\pokerProject\\Images\\KK.jpg");
            //Cv2.ImShow("C:\\Users\\lms\\source\\repos\\pokerProject\\Images\\KK.jpg",k);////opencv 테스트
        }
    }
}
