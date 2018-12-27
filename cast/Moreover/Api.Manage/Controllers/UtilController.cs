using System;
using System.IO;
using System.Linq;
using Api.Manage.Assist.Entity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Manage.Controllers
{
  [EnableCors("AllowCors")]
  [Route("api/[controller]")]
  [ApiController]
  public class UtilController : ControllerBase
  {
    private IHostingEnvironment hostingEnvironment;

    public UtilController(IHostingEnvironment hostingEnvironment)
    {
      this.hostingEnvironment = hostingEnvironment;
    }

    [Route(nameof(Upload))]
    [HttpPost]
    public object Upload()
    {
      var files = Request.Form.Files;

      long size = files.Sum(f => f.Length);
      string webRootPath = hostingEnvironment.WebRootPath;
      string contentRootPath = hostingEnvironment.ContentRootPath;
      var uploadUrl = string.Empty;
      foreach (var formFile in files)
      {
        if (formFile.Length > 0)
        {
          string fileExt = GetFileExt(formFile.FileName); //文件扩展名，不含“.”
          long fileSize = formFile.Length; //获得文件大小，以字节为单位
          string newFileName = System.Guid.NewGuid().ToString() + "." + fileExt; //随机生成新的文件名
          var floderPath = webRootPath + "/upload/";
          uploadUrl = "upload/" + newFileName;
          if (!Directory.Exists(floderPath))
          {
            Directory.CreateDirectory(floderPath);
          }

          var filePath = webRootPath + "/upload/" + newFileName;
          using (var stream = new FileStream(filePath, FileMode.Create))
          {
            formFile.CopyTo(stream);
          }
        }
      }

      return ResultModel.GetSuccessModel("文件上传", uploadUrl);
      
    }

    public string GetFileExt(string fileName)
    {
      if (string.IsNullOrWhiteSpace(fileName))
      {
        return string.Empty;
      }

      return fileName.Substring(fileName?.LastIndexOf(".") + 1 ?? 0);
    }
  }

  public class UploadResult
  {
    /// <summary>
    /// 状态码，0 代表成功
    /// </summary>
    public int Code { get; set; } = -1;

    /// <summary>
    /// 图片预览地址
    /// </summary>
    public string ImgUrl { get; set; }

    /// <summary>
    /// 文件下载地址 (可选)
    /// </summary>
    public string DownloadUrl { get; set; }

    /// <summary>
    /// 文件大小 (可选)
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// 图片高度，非图片类型不需要 (可选)
    /// </summary>
    public int FileHeight { get; set; }

    /// <summary>
    /// 图片宽度，非图片类型不需要 (可选)
    /// </summary>
    public int FileWidth { get; set; }

    /// <summary>
    /// 文件 hash 值 (可选)
    /// </summary>
    public string FileMd5 { get; set; }

    public string Path { get; set; }
  }
}