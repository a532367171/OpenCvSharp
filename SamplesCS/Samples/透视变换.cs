using System;
using OpenCvSharp;
using SampleBase;


namespace SamplesCS
{
    class 透视变换 : ISample
    {

        //cv::Mat get_perspective_mat()
        //{
        //    cv::Point2f src_points[] = {
        //cv::Point2f(165, 270),
        //cv::Point2f(835, 270),
        //cv::Point2f(360, 125),
        //cv::Point2f(615, 125) };

        //    cv::Point2f dst_points[] = {
        //cv::Point2f(165, 270),
        //cv::Point2f(835, 270),
        //cv::Point2f(165, 30),
        //cv::Point2f(835, 30) };

        //    cv::Mat M = cv::getPerspectiveTransform(src_points, dst_points);

        //    return M;

        //}



        public void Run()
        {

            using (var img1 = new Mat(FilePath.Image.透视变换))
            using (var descriptors1 = new Mat())
            using (var windowSrc = new Window("src"))
            using (var windowDst = new Window("dst"))
            {
                Point2f[] src_points = { new Point2f(165, 270),
                                      new Point2f(835, 270),
                                      new Point2f(360, 125),
                                      new Point2f(615, 125) };

                Point2f[] dst_points = { new Point2f(165, 270),
                                      new Point2f(835, 270),
                                      new Point2f(165, 30),
                                      new Point2f(835, 30) };

                Mat M = Cv2.GetPerspectiveTransform(src_points, dst_points);
                Cv2.WarpPerspective(img1, descriptors1, M, new Size(960, 270), InterpolationFlags.Linear);
                windowSrc.Image = img1;
                windowDst.Image = descriptors1;
                Cv2.WaitKey(50);
            }
        }
    }
}
