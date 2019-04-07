using System;
using System.Runtime.InteropServices;
using OpenCvSharp;
using SampleBase;

namespace SamplesCS
{
    class VideoCaptureSampleLianJiangGuanPianChuYao : ISample
    {
        public void Run()
        {
            // Opens MP4 file (ffmpeg is probably needed)
            //var capture = new VideoCapture(FilePath.Movie.Bach);
            var capture = new VideoCapture("rtmp://rtmp.open.ys7.com/openlive/61e96da9f12a4d058f4737d02c42998d");
            #region MyRegion
            //int sleepTime = (int)Math.Round(1000 / capture.Fps);

            //using (var window = new Window("capture"))
            //{
            //    // Frame image buffer
            //    Mat image = new Mat();

            //    // When the movie playback reaches end, Mat.data becomes NULL.
            //    while (true)
            //    {
            //        capture.Read(image); // same as cvQueryFrame
            //        if (image.Empty())
            //            break;

            //        window.ShowImage(image);
            //        Cv2.WaitKey(sleepTime);
            //    }
            //} 
            #endregion
            Rect roi=new Rect();
            Mat kernel1 = Cv2.GetStructuringElement(0,new Size(3, 3), new Point(-1, -1));
            Mat kernel2 = Cv2.GetStructuringElement(0, new Size(5, 5), new Point(-1, -1));


            using (var mog = BackgroundSubtractorMOG.Create())
            using (var windowSrc = new Window("src"))
            //using (var windowinrange = new Window("inrange"))
            using (var windowDst = new Window("dst"))
            {
                var frame = new Mat();
                //var inrange = new Mat();
                var fg = new Mat();
                while (true)
                {
                    capture.Read(frame);
                    if (frame.Empty())
                        break;

                    //Cv2.InRange(frame, new Scalar(100, 100, 0), new Scalar(255, 255, 100), inrange);

                    mog.Apply(frame, fg, 0.01);
                    // 开操作
                    //Cv2.MorphologyEx(fg, fg, OpenCvSharp.MorphTypes.Open, kernel1, new Point(-1, -1), 1);
                    //// 膨胀
                    //Cv2.Dilate(fg, fg, kernel2,new Point(-1, -1), 4);
                    // 轮廓发现与位置标定
                    processFrame(ref fg, ref roi); 
                    //画矩形                         
                    Cv2.Rectangle(frame, roi,new Scalar(0, 255, 0));
                    Cv2.Rectangle(fg, roi, new Scalar(0, 255, 0));


                    windowSrc.Image = frame;
                    //windowinrange.Image = inrange;

                    windowDst.Image = fg;
                    Cv2.WaitKey(50);
                }
            }
        }

        public void processFrame(ref Mat binary, ref Rect rect)
        {
            Point[][] contours = null;

            //vector<Vec4i> hierarcy;

            HierarchyIndex[] hierarcy = null;
            //画椭圆及中心
            Cv2.FindContours(binary, out contours, out hierarcy, RetrievalModes.External, ContourApproximationModes.ApproxNone);

            if (contours.GetLength(0) > 0)
            {
                double maxArea = 0.0;
                for (int t = 0; t < contours.GetLength(0); t++)
                {
                    double area = Cv2.ContourArea(contours[t]);
                    if (area > maxArea)
                    {
                        maxArea = area;
                        rect = Cv2.BoundingRect(contours[t]);
                    }
                }
            }
            else
            {
                rect.X = rect.Y = rect.Width = rect.Height = 0;
            }

        }

    }
}