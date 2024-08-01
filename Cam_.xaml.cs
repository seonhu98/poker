using System;
using System.Collections.Generic;
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


// OpenCV 사용을 위한 using
using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
using Tesseract;

// Timer 사용을 위한 using
using System.Windows.Threading;
using pokerProject.imageSR;
using pokerProject;
using System.Net;
using System.Security.Cryptography;
using pokerProject.Network;

namespace pokerProject
{
    // OpenCvSharp 설치 시 Window를 명시적으로 사용해 주어야 함 (window -> System.Windows.Window)

    public partial class Cam_ : System.Windows.Window
    {
        // 필요한 변수 선언
        VideoCapture cam;
        Mat frame;
        DispatcherTimer timer;
        bool is_initCam, is_initTimer;
        //Ractangle rac = new Ractangle();

        //List<OpenCvSharp.Mat> mats = new List<Mat>();
        public Cam_()
        {
            InitializeComponent();
        }

        private void windows_loaded(object sender, RoutedEventArgs e)
        {
            // 카메라, 타이머(0.01ms 간격) 초기화
            is_initCam = init_camera();
            is_initTimer = init_Timer(0.01);

            // 초기화 완료면 타이머 실행
            if (is_initTimer && is_initCam) timer.Start();
        }

        private bool init_Timer(double interval_ms)
        {
            try
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(interval_ms);
                timer.Tick += new EventHandler(timer_tick);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool init_camera()
        {
            try
            {
                // 0번 카메라로 VideoCapture 생성 (카메라가 없으면 안됨)
                cam = new VideoCapture(0);
                cam.FrameHeight = (int)Cam_1.Height;
                cam.FrameWidth = (int)Cam_1.Width;

                // 카메라 영상을 담을 Mat 변수 생성
                frame = new Mat();

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void upload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Mat binary = new Mat();
                string save_name = DateTime.Now.ToString("yyyy-MM-dd-hh시mm분ss초");
                string file_path = "C:\\Users\\lms\\Desktop\\pokerProject3\\pokerProject\\Images\\" + save_name + ".jpg";
                string[] shape = { "C", "S", "H", "D" };
                string[] number = { "2", "3", "4", "5", "6", "7", "8" };
                Cv2.Resize(frame, frame, new OpenCvSharp.Size(960, 540));
                Cv2.ImWrite(file_path, frame);
                Cv2.ImShow("frame", frame);
                Mat src = Cv2.ImRead(file_path);
                Mat dst = src.Clone();
                Mat result = new Mat();
                //if문 추가해서 매칭여부 확인 후 break
                // for문 두번 돌리기 어떻게? 반환값을 찾아서 조건걸기
                int scount = 0;
                for (scount = 0; scount < 4; scount++)
                {
                    Mat templ = Cv2.ImRead("C:\\Users\\lms\\Desktop\\pokerProject3\\pokerProject\\Images\\" + shape[scount] + ".jpg");
                    Cv2.MatchTemplate(src, templ, result, TemplateMatchModes.CCoeffNormed);

                    Cv2.MinMaxLoc(result, out double minVal, out double maxVal, out OpenCvSharp.Point minLoc, out OpenCvSharp.Point maxLoc);
                    //MessageBox.Show(minVal.ToString());
                    //MessageBox.Show(maxVal.ToString());
                    //double swidth = minLoc.Y - maxLoc.Y;
                    //double sheight = maxLoc.X - minLoc.X;
                    //OpenCvSharp.Point M2 = new OpenCvSharp.Point(maxLoc.X, minLoc.Y);
                    //OpenCvSharp.Point M3 = new OpenCvSharp.Point(minLoc.X, maxLoc.Y);
                    if (scount == 0)
                    {
                        if (maxVal >= 0.78)
                        {
                            //Cv2.Rectangle(dst,OpenCvSharp.Rect(minLoc.X,minLoc.Y,swidth,sheight,new Scalar(0, 0, 255), 4));
                            Cv2.Rectangle(dst, new OpenCvSharp.Rect(maxLoc, templ.Size()), new Scalar(0, 0, 255), 4);
                            MessageBox.Show(templ.Size().ToString());
                            //Cv2.ImShow("dst", dst);
                            Cv2.WaitKey(0);
                            Cv2.DestroyAllWindows();

                            break;
                        }
                    }
                    else if (scount != 0)
                    {

                        if (maxVal >= 0.68)
                        {
                            //Cv2.Rectangle(dst,OpenCvSharp.Rect(minLoc.X,minLoc.Y,swidth,sheight,new Scalar(0, 0, 255), 4));
                            Cv2.Rectangle(dst, new OpenCvSharp.Rect(maxLoc, templ.Size()), new Scalar(0, 0, 255), 4);
                            MessageBox.Show(templ.Size().ToString());
                            //Cv2.ImShow("dst", dst);
                            Cv2.WaitKey(0);
                            Cv2.DestroyAllWindows();
                            break;
                        }

                        else
                        {
                            continue;
                        }
                    }
                }
                int ncount = 0;
                for (ncount = 0; ncount < 7; ncount++)
                {

                    Mat templ = Cv2.ImRead("C:\\Users\\lms\\Desktop\\pokerProject3\\pokerProject\\Images\\" + number[ncount] + ".jpg");

                    //Mat src1 = new Mat();
                    //매칭이 안되면 바로 오류뜸
                    //maxVal가 0.6 이상이어야 정확도가 높음
                    //얘의 반환값을 알아야 함

                    Cv2.MatchTemplate(src, templ, result, TemplateMatchModes.CCoeffNormed);

                    Cv2.MinMaxLoc(result, out double minVal, out double maxVal, out OpenCvSharp.Point minLoc, out OpenCvSharp.Point maxLoc);
                    //MessageBox.Show(minVal.ToString());
                    //MessageBox.Show(maxVal.ToString());
                    //double swidth = minLoc.Y - maxLoc.Y;
                    //double sheight = maxLoc.X - minLoc.X;
                    //OpenCvSharp.Point M2 = new OpenCvSharp.Point(maxLoc.X, minLoc.Y);
                    //OpenCvSharp.Point M3 = new OpenCvSharp.Point(minLoc.X, maxLoc.Y);
                    if (maxVal >= 0.6)
                    {
                        //Cv2.Rectangle(dst,OpenCvSharp.Rect(minLoc.X,minLoc.Y,swidth,sheight,new Scalar(0, 0, 255), 4));
                        Cv2.Rectangle(dst, new OpenCvSharp.Rect(maxLoc, templ.Size()), new Scalar(0, 0, 255), 4);
                        MessageBox.Show(templ.Size().ToString());
                        Cv2.ImShow("dst", dst);
                        Cv2.WaitKey(0);
                        Cv2.DestroyAllWindows();
                        MessageBox.Show(number[ncount]);
                        MessageBox.Show(shape[scount]);
                       
                        string text= number[ncount];
                        Go.shoot(src, number[ncount], shape[scount]);//C++전송
                        
                        Gotext.gogo(shape[scount], Convert.ToInt32(text));//Python전송

                        break;
                    }
                    else
                    {
                        continue;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 " + ex.Message, "오류", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void timer_tick(object sender, EventArgs e)
        {
            // 0번 장비로 생성된 VideoCapture 객체에서 frame을 읽어옴
            cam.Read(frame);
            // Cv2.CvtColor(frame, frame, ColorConversionCodes.BGR2GRAY);
            // 읽어온 Mat 데이터를 Bitmap 데이터로 변경 후 컨트롤에 그려줌
            Cam_1.Source = OpenCvSharp.WpfExtensions.WriteableBitmapConverter.ToWriteableBitmap(frame);
        }
    }
}