using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS
{
    class Class1 : ISample
    {
        public void Run()
        {
            var cap = new VideoCapture("C:\\Users\\Administrator\\Web\\RecordFiles\\2018-07-26\\20180726115428592.mp4");
            if (!cap.IsOpened()) //检查打开是否成功
                return;
            var frame = new Mat();
            var result = new Mat();
            var background = new Mat();
            int count = 0;
            while (true)
            {
                cap.Read(frame);
                if (frame.Empty())
                    break;
                else
                {
                    count++;
                    if (count == 1)
                        background = frame.Clone();//提取第一帧为背景帧  
                    //var windowSrc = new Window("video");
                    //windowSrc.Image = frame;
                    Cv2.ImShow("video", frame);

                    result = MoveDetect(background, frame);

                    //var windowDst = new Window("result");
                    // windowDst.Image = result;
                    Cv2.ImShow("result", result);


                    if (Cv2.WaitKey(50) == 27)
                        break;
                }
            }
            cap.Release();
        }

        private Mat MoveDetect(Mat frame1, Mat frame2)
        {
            Mat result = frame2.Clone();


            var gray1 = new Mat();
            var gray2 = new Mat();
            Cv2.CvtColor(frame1, gray1, ColorConversionCodes.BGR2GRAY);
            Cv2.CvtColor(frame2, gray2, ColorConversionCodes.BGR2GRAY);
            var diff = new Mat();
            Cv2.Absdiff(gray1, gray2, diff);
            Cv2.ImShow("absdiss", diff);
            Cv2.Threshold(diff, diff, 45, 255, ThresholdTypes.Binary);
            Cv2.ImShow("threshold", diff);

            Mat element = Cv2.GetStructuringElement(MorphShapes.Rect, new Size(3, 3));
            Mat element2 = Cv2.GetStructuringElement(MorphShapes.Rect, new Size(25, 25));
            Cv2.Erode(diff, diff, element);
            Cv2.ImShow("erode", diff);

            Cv2.Dilate(diff, diff, element2);
            Cv2.ImShow("dilate", diff);

            Point[][] contours = null;

            //vector<Vec4i> hierarcy;

            HierarchyIndex[] hierarcy = null;
            //画椭圆及中心
            Cv2.FindContours(diff, out contours, out hierarcy, RetrievalModes.External, ContourApproximationModes.ApproxNone);
            //cout << "num=" << contours.size() << endl;
          //contours.Count;
            int X = contours.GetLength(0);
            List<RotatedRect> box = new List<RotatedRect>();

            //for (int i = 0; i < X; i++)
            //{
            //    box[i] = Cv2.FitEllipse(new Mat(contours[i]));
            //    Cv2.Ellipse(result, box[i], new Scalar(0, 255, 0), 2, 8);
            //    Cv2.Circle(result, box[i].center, 3, new Scalar(0, 0, 255), -1, 8);
            //}



            foreach (Point[] pts in contours)
            {
                var    box1 = Cv2.FitEllipse(pts);
                Cv2.Ellipse(result, box1, new Scalar(0, 255, 0), 2);


                Point point;
                point.X = (int)box1.Center.X;
                point.Y= (int)box1.Center.X;
                //Cv2.Circle(result, (int)box1.Center.X, (int)box1.Center.X, 1, -1, 8);
                 result.Circle(point, 1, -1);



                //Scalar color = Scalar.RandomColor();
                //foreach (Point p in pts)
                //{
                //    result.Circle(p,1, color);
                //}
            }
            return result;
        }


    }
}
