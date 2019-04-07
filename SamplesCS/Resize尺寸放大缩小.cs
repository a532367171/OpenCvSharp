using OpenCvSharp;
using SampleBase;

namespace SamplesCS
{
    internal class Resize尺寸放大缩小 : ISample
    {
        public void Run()
        {
            using (var img = new Mat(FilePath.Image.Resize尺寸放大缩小))
            using (var dst = new Mat())
            using (var dst2 = new Mat())
            using (var windowSrc = new Window("原来"))
            using (var windowdst = new Window("尺寸放大之后"))
            using (var windowdst2 = new Window("尺寸缩小之后"))
            {
                //Size dsize = new Size(640, 480);


                Cv2.Resize(img, dst, new Size(img.Cols * 2, img.Rows * 2), 0, 0, InterpolationFlags.Linear);//放大一倍
                //Cv2.Resize(img, dst2, new Size(img.Cols * 0.5, img.Rows * 0.5), 0, 0, InterpolationFlags.Linear);//缩小一倍
                Cv2.Resize(img, dst2, new Size(100,100), 0, 0, InterpolationFlags.Linear);//缩小28*28

                //Cv2.PyrUp(img, dst, new Size(img.Cols * 2, img.Rows * 2)); //放大一倍
                //Cv2.PyrDown(img, dst2, new Size(img.Cols * 0.5, img.Rows * 0.5)); //缩小一倍

                windowSrc.Image = img;
                windowdst.Image = dst;
                windowdst2.Image = dst2;
                Cv2.ImWrite(FilePath.Image.Resize尺寸放大缩小1, dst2);
                Cv2.WaitKey(500000);
            }
        }
    }
}