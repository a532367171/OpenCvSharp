using OpenCvSharp;
using SampleBase;

namespace SamplesCS
{
    /// <summary>
    /// 
    /// </summary>
    class MorphologySample : ISample
    {
        public void Run()
        {
           Mat gray = new Mat(FilePath.Image.Goryokaku, ImreadModes.GrayScale);
            //Mat gray = new Mat(FilePath.Image.Lenna, ImreadModes.GrayScale);
            Mat binary = new Mat();
            Mat dilate1 = new Mat();
            Mat dilate2 = new Mat();
            Mat dilate3 = new Mat();
            Mat dilate4 = new Mat();

            Mat dilate12 = new Mat();
            Mat dilate13 = new Mat();


            //byte[] kernelValues = {0, 1, 0, 1, 1, 1, 0, 1, 0}; // cross (+)
            byte[] kernelValues = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0,0, 0, 0, 0, 0}; // cross (+)

            Mat kernel = new Mat(3, 10, MatType.CV_8UC1, kernelValues);

            // Binarize
            Cv2.Threshold(gray, binary, 180, 255, ThresholdTypes.Binary);
            
            // empty kernel / /膨胀图像通过使用一个特定的结构元素。
            Cv2.Dilate(~binary, dilate1, null);
            // + kernel
            Cv2.Dilate(~binary, dilate2, kernel);
            Cv2.Dilate(dilate2, dilate3, kernel);
            Cv2.Dilate(dilate3, dilate4, kernel);

            Mat horizontalStructure = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(binary.Cols / 10, 2));
    
            //Cv2.Erode(~binary, dilate12, horizontalStructure, new OpenCvSharp.Point(-1, -1));

                //横膨胀
          Cv2.Dilate(~binary, dilate13, horizontalStructure, new OpenCvSharp.Point(-1, -1));
          



                Cv2.ImShow("binary", binary);
            Cv2.ImShow("dilate (kernel = null)", ~dilate1);
            Cv2.ImShow("dilate (kernel = +)", ~dilate13);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
    }
}