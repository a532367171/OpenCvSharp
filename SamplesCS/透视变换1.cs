using System;
using OpenCvSharp;
using SampleBase;


namespace SamplesCS
{
    class 透视变换1 : ISample
    {
        public void Run()
        {
            using (var img1 = new Mat(FilePath.Image.Match4))
            using (var descriptors1 = new Mat())
            using (var windowSrc = new Window("src"))
            using (var windowDst = new Window("dst"))
            {
                Point2f[] src_points = { new Point2f(105, 194),
                                      new Point2f(268,288),
                                      new Point2f(277, 29),
                                      new Point2f(328, 81) };

                Point2f[] dst_points = { new Point2f(70, 287),
                                      new Point2f(268,288),
                                     new Point2f(70, 100),
                                      new Point2f(268,100) };

                Mat M = Cv2.GetPerspectiveTransform(src_points, dst_points);
                Cv2.WarpPerspective(img1, descriptors1, M, new Size(960, 960), InterpolationFlags.Linear);
                windowSrc.Image = img1;
                windowDst.Image = descriptors1;
                Cv2.WaitKey(500000);
            }
        }
    }
}
