using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp.Extensions;
using Tesseract;
//얘는 예비용->사각형 못찾으면 추가하여 로직 다시 짜기
namespace pokerProject
{
    public class Text_Get
    {
        public static string OCR(Mat src)
        {
            Bitmap bitmap = src.ToBitmap();

            TesseractEngine ocr = new TesseractEngine(@"C:\Users\lms\source\repos\pokerProjecttessdata\", "eng", EngineMode.LstmOnly);
            ocr.SetVariable("tessedit_char_whitelist", ",SUM$0123456789");
            string texts = bitmap.ToString();

            return texts;
        }
    }
}
