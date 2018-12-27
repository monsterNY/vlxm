using System.Drawing;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using Microsoft.AspNetCore.Http;

namespace Api.Manage.Assist.UploadInfo
{
  /// <summary>
  /// 缩略图
  /// </summary>
  public class SmallUpload : SuperUpload
  {
    protected override string Run(IFormFile imgFile, string saveUrl)
    {

      //通过文件获取Image图像
      Image image = Image.FromStream(imgFile.OpenReadStream());

      //创建缩略图
      Bitmap bitmap = new Bitmap(100, 100);

      //获取缩略图画布
      Graphics graphics = Graphics.FromImage(bitmap);

      //将图片画到缩略图上
      graphics.DrawImage(image, new System.DrawingCore.Rectangle(0, 0, 100, 100), new System.DrawingCore.Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

      //保存缩略图
      bitmap.Save(saveUrl, ImageFormat.Jpeg);
      return SUCCESS;

    }
  }
}