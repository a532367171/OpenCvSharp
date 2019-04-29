using OpenCvSharp;
using SampleBase;

namespace SamplesCS
{
    internal class 直方图均衡化 : ISample
    {
        public void Run()
        {
            using (var img = new Mat(FilePath.Image.直方图均衡化))
            using (var img2 = new Mat(FilePath.Image.直方图均衡化2))
            using (var dst2 = new Mat())
            using (var dst = new Mat())
            using (var windowSrc = new Window("原来"))
            using (var windowdst = new Window("直方图均衡化"))
            using (var windowdst2 = new Window("直方图均衡化2"))
            {


                Mat[] imageRGB = new Mat[3];

                imageRGB = Cv2.Split(img);

                for (int i = 0; i < 3; i++)
                {
                    Cv2.EqualizeHist(imageRGB[i], imageRGB[i]);
                }

                Cv2.Merge(imageRGB, dst);


                Mat[] imageRGB1 = new Mat[3];

                imageRGB1 = Cv2.Split(img2);

                for (int i = 0; i < 3; i++)
                {
                    Cv2.EqualizeHist(imageRGB1[i], imageRGB1[i]);
                }

                Cv2.Merge(imageRGB1, dst2);




                windowSrc.Image = img;
                windowdst.Image = dst;
                windowdst2.Image = dst2;
                //Cv2.ImWrite(FilePath.Image.图像金字塔1, dst2);
                Cv2.WaitKey(500000);
            }

        }
    }
}