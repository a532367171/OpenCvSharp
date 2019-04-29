using OpenCvSharp;
using SampleBase;
using System;

namespace SamplesCS
{
    internal class 视频转换1 : ISample
    {
        public void Run()
        {
            // Opens MP4 file (ffmpeg is probably needed)
            string Bach = @"D:\Program Files (x86)\Ezviz Studio\record\C62618904_1_20190125T130011Z_20190125T132547Z.mp4";
            //var capture = new VideoCapture(FilePath.Movie.Bach);
            string Bach图片存放位置 = @"D:\图片\";
            var capture = new VideoCapture(Bach);
            Rect r1 = new Rect(100, 100, 250, 300);
            int i;
            i = 100;
            int sleepTime = (int)Math.Round(1000 / capture.Fps);
            using (Mat image = new Mat())
            using (Mat image灰度图 = new Mat())
            using (Mat mask = Mat.Zeros(280, 280, MatType.CV_8UC3))
            using (Mat image缩小 = new Mat())
            using (Mat image遮盖 = new Mat())

            using (var window = new Window("capture"))
            {
                //mask.RowRange(450, 460).SetTo(new Scalar(255))
                // When the movie playback reaches end, Mat.data becomes NULL.
                while (i > 0)
                {
                    capture.Read(image); // same as cvQueryFrame
                    if (image.Empty())
                        break;

                    Cv2.CvtColor(image, image灰度图, ColorConversionCodes.BGR2GRAY);


                    Cv2.Resize(image灰度图, image缩小, new Size(280, 280), 0, 0, InterpolationFlags.Linear);//缩小28*28

                    //Mat left = new Mat(mask, new Rect(0, 0, 130, 80));

                    image缩小.Rectangle(new Point(0, 0), new Point(130, 80), new Scalar(0), -1);
                    //image遮盖.CopyTo(left);
                    //image缩小.Line(260,0, 140, 280, new Scalar(0),4);

                    image缩小.Line(0, 0, 280, 280, new Scalar(0), 20);

                    Cv2.ImWrite(Bach图片存放位置 + "dog." + i.ToString() + ".jpg", image缩小);


                    window.ShowImage(image缩小);
                    Cv2.WaitKey(1);
                    i--;
                }
            }
        }
    }
}