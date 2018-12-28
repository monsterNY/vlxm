using System;
using System.Drawing;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using Microsoft.AspNetCore.Http;

namespace Api.Manage.Assist.UploadInfo
{
    public class WatermarkUpload : BaseUpload
    {

        /// <summary>
        /// 水印信息
        /// </summary>
        public string WatermarkStr { get; set; }

        protected override string Run(IFormFile imgFile, string saveUrl)
        {
            //通过文件的输入流获取图片
            Image image = Image.FromStream(imgFile.OpenReadStream());

            //获取画布
            Graphics graphics = Graphics.FromImage(image);

            //设置水印
            string makeStr = string.IsNullOrWhiteSpace(WatermarkStr) ? ("xdStore" + DateTime.Now) : WatermarkStr;

            graphics.DrawString(makeStr, new Font("宋体", 16), new SolidBrush(System.DrawingCore.Color.Aquamarine), new System.DrawingCore.PointF(
                (image.Width - (makeStr.Length * 16)), image.Height - 24
                ));

            image.Save(saveUrl, ImageFormat.Jpeg);
            return SUCCESS;
        }
    }
}