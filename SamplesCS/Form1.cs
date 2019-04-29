using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SamplesCS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            string folderPath = string.Empty;
            DialogResult ret = folderBrowserDialog1.ShowDialog();
            if (ret != DialogResult.Cancel)
            {
                folderPath = folderBrowserDialog1.SelectedPath;
                //// 开始查询此文件目录中的所有txt文件
                ////  第三个参数，如果要查找此目录下的子目录，则用System.IO.SearchOption.AllDirectories
                ////              如果只查找当前目录则用System.IO.SearchOption.TopDirectoryOnly
                string[] txtFiles = System.IO.Directory.GetFiles(folderPath, "*.mp4", System.IO.SearchOption.TopDirectoryOnly);
                foreach (string path in txtFiles)
                {



                    //检查是否存在文件夹
                    string subPath = folderPath + "\\" + Path.GetFileNameWithoutExtension(path);
                    if (false == System.IO.Directory.Exists(subPath))
                    {
                        System.IO.Directory.CreateDirectory(subPath);
                    }

                    extract_frames(path, subPath, Path.GetFileNameWithoutExtension(path), 2500);


                    //// 打开文件
                    //System.IO.StreamReader reader = new System.IO.StreamReader(path);
                    //while (string.IsNullOrEmpty(reader.ReadLine()))
                    //{
                    //    ////进行你自己的操作吧
                    //}
                }
            }


        }

        private void extract_frames(string video_path, string dst_folder, string 文件名, int 视频的频率)
        {

            //var capture = new VideoCapture(FilePath.Movie.Bach);
            //string Bach图片存放位置 = @"D:\图片\";
            var capture = new VideoCapture(video_path);
            //Rect r1 = new Rect(100, 100, 250, 300);
            int i = 0;
            int sleepTime = (int)Math.Round(1000 / capture.Fps);
            using (var image = new Mat())
            //using (var image灰度图 = new Mat())
            using (Mat mask = Mat.Zeros(280, 280, MatType.CV_8UC3))
            using (var image缩小 = new Mat())
            using (var image遮盖 = new Mat())

            //using (var window = new Window("capture"))
            {
                string[] sArray = 文件名.Split(' ');
                int times = 0;
                //mask.RowRange(450, 460).SetTo(new Scalar(255))
                // When the movie playback reaches end, Mat.data becomes NULL.
                while (true)
                {
                    times++;
                    i++;
                    capture.Read(image); // same as cvQueryFrame
                    if (image.Empty())
                        break;


                    //if (times % 视频的频率 == 0)
                    //{
                    //    Cv2.Resize(image, image缩小, new OpenCvSharp.Size(280, 280), 0, 0, InterpolationFlags.Linear);//缩小28*28

                    //    //Mat left = new Mat(mask, new Rect(0, 0, 130, 80));

                    //    //image缩小.Rectangle(new Point(0, 0), new Point(130, 80), new Scalar(0), -1);
                    //    //image遮盖.CopyTo(left);
                    //    //image缩小.Line(260,0, 140, 280, new Scalar(0),4);

                    //    //image缩小.Line(0, 0, 280, 280, new Scalar(0), 20);

                    //    Cv2.ImWrite(dst_folder +"\\"+ "未知." + 文件名 + i.ToString() + ".jpg", image缩小);
                    //}

                    Cv2.Resize(image, image缩小, new OpenCvSharp.Size(280, 280), 0, 0, InterpolationFlags.Linear);//缩小28*28


                    Cv2.ImWrite(dst_folder + "\\" + sArray[0] + "." + 文件名 + i.ToString() + ".jpg", image缩小);



                    //window.ShowImage(image缩小);
                    Cv2.WaitKey(1);
                    //i--;
                }
            }
        }
    }
}
