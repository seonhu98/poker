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
//시작 페이지
namespace pokerProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Start : Page
    {
        public Start()
        {
            InitializeComponent();
            //Mat k = new Mat();
            //k = Cv2.ImRead("C:\\Users\\lms\\source\\repos\\pokerProject\\Images\\KK.jpg");
            //Cv2.ImShow("C:\\Users\\lms\\source\\repos\\pokerProject\\Images\\KK.jpg",k);////opencv 테스트
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Window CP = new Cam_();
            CP.Show();
        }
    }
}