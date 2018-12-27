using System;
using System.Collections.Generic;
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

    /// <summary>
    /// 上传图片 单图片
    /// </summary>
    /// <returns></returns>
    [Route(nameof(UploadSingleImage))]
    [HttpPost]
    public object UploadSingleImage()
    {

      var title = "图片上传";

      //获取文件
      var file = Request.Form.Files.FirstOrDefault();

      //获取项目地址
      string webRootPath = hostingEnvironment.WebRootPath;

      if (file == null)
      {
        return ResultModel.GetNullErrorModel(title);
      }

      //允许的图片类型
      var allowFileType = new Dictionary<string,string>
      {
        {"image/jpeg","jpg" },
        {"image/jpg","jpg" },
        {"image/gif","jpg" },
        {"image/png","jpg" },
        {"image/bmp","jpg" },
      };

      if (!allowFileType.ContainsKey(file.ContentType))
      {
        return ResultModel.GetParamErrorModel(title, $"不支持的文件格式：{file.ContentType}");
      }

      var maxSize = 5;//文件上传的最大大小 (mb)
      if (file.Length > maxSize * 1024 * 1024)
      {
        return ResultModel.GetParamErrorModel(title, $"文件最大支持：{maxSize}MB,当前文件大小：{file.Length}字节");
      }

      var fileName = $"{Guid.NewGuid().ToString().Replace("-", "")}.{allowFileType[file.ContentType]}";

      var floderPath = $"/upload/image/{DateTime.Now:yyyy-MM-dd}/";

      //检测是否存在此文件夹
      if (!Directory.Exists(webRootPath + floderPath))
      {
        Directory.CreateDirectory(webRootPath+floderPath);
      }

      using (var stream = new FileStream(webRootPath + floderPath + fileName, FileMode.Create))
      {
        file.CopyTo(stream);
      }

      return ResultModel.GetSuccessModel(title, floderPath + fileName);

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
  
}