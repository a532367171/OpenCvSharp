using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS
{
    class Class2 : ISample
    {
        

       public void Run()
        {
            var img = Cv2.ImRead("temp.jpg");    
            //#载入图像
      //var h;
        var h = img.Row;
            var w = img.Cols;
            //获取图像的高和宽 
            Cv2.ImShow("Origin", img);
        }

    }
}
