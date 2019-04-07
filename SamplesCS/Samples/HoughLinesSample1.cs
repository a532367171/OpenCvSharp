﻿using System;
using OpenCvSharp;
using SampleBase;

namespace SamplesCS
{
    /// <summary>
    /// Hough Transform Sample / ハフ変換による直線検出 ///霍夫变换的直线检测
    /// </summary>
    /// <remarks>http://opencv.jp/sample/special_transforms.html#hough_line</remarks>
    class HoughLinesSample1 : ISample
    {
        public void Run()
        {
            SampleCpp();
        }

        /// <summary>
        /// sample of new C++ style wrapper
        /// </summary>
        private void SampleCpp()
        {
            // (1) Load the image
            using (Mat imgGray = new Mat(FilePath.Image.Match4, ImreadModes.GrayScale))
            using (Mat imgStd = new Mat(FilePath.Image.Match4, ImreadModes.Color))
            using (Mat imgProb = imgStd.Clone())
            {
                // Preprocess边缘检测
                Cv2.Canny(imgGray, imgGray, 50, 200, 3, false);

                // (3) Run Standard Hough Transform 
                LineSegmentPolar[] segStd = Cv2.HoughLines(imgGray,1, Math.PI / 180, 100, 0, 0);
                int limit = Math.Min(segStd.Length, 10000);
                for (int i = 0; i < limit; i++)
                {
                    // Draws result lines
                    float rho = segStd[i].Rho;
                    float theta = segStd[i].Theta;
                    if (theta > 1.5F && theta < 1.6F)
                    {
                        double a = Math.Cos(theta);
                        double b = Math.Sin(theta);
                        double x0 = a * rho;
                        double y0 = b * rho;
                        Point pt1 = new Point { X = (int)Math.Round(x0 + 1000 * (-b)), Y = (int)Math.Round(y0 + 1000 * (a)) };
                        Point pt2 = new Point { X = (int)Math.Round(x0 - 1000 * (-b)), Y = (int)Math.Round(y0 - 1000 * (a)) };
                        imgStd.Line(pt1, pt2, Scalar.Red, 3, LineTypes.AntiAlias, 0);
                    }
                  
                }

                // (4) Run Probabilistic Hough Transform
                //LineSegmentPoint[] segProb = Cv2.HoughLinesP(imgGray, 1, Math.PI / 180, 50, 50, 10);
                LineSegmentPoint[] segProb = Cv2.HoughLinesP(imgGray, 1, Math.PI / 180, 50, 80, 1);

                foreach (LineSegmentPoint s in segProb)
                {
                    imgProb.Line(s.P1, s.P2, Scalar.Red, 3, LineTypes.AntiAlias, 0);
                }

                // (5) Show results
                using (new Window("Hough_line_standard", WindowMode.AutoSize, imgStd))
                using (new Window("Hough_line_probabilistic", WindowMode.AutoSize, imgProb))
                using (new Window("imgGray", WindowMode.AutoSize, imgGray))
                {

                    Window.WaitKey(0);
                }
            }
        }

    }
}