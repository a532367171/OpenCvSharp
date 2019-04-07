using OpenCvSharp;
using SampleBase;

namespace SamplesCS
{
    internal class 图像金字塔 : ISample
    {
        public void Run()
        {
            using (var img = new Mat(FilePath.Image.图像金字塔))
            using (var dst = new Mat())
            using (var dst2 = new Mat())
            using (var windowSrc = new Window("原来"))
            using (var windowdst = new Window("尺寸放大之后"))
            using (var windowdst2 = new Window("尺寸缩小之后"))
            {
              
                Cv2.PyrUp(img, dst,new Size(img.Cols * 2, img.Rows * 2)); //放大一倍
                Cv2.PyrDown(img, dst2, new Size(img.Cols * 0.5, img.Rows * 0.5)); //缩小一倍

                windowSrc.Image = img;
                windowdst.Image = dst;
                windowdst2.Image = dst2;
                Cv2.ImWrite(FilePath.Image.图像金字塔1, dst2);
                Cv2.WaitKey(500000);
            }


        }
    }
}