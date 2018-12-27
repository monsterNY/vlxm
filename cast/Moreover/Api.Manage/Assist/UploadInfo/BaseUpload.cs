using Microsoft.AspNetCore.Http;
using XD.Store.Api.ImgServer.UploadInfo;

namespace Api.Manage.Assist.UploadInfo
{
    /// <summary>
    /// 基础
    /// </summary>
    public class BaseUpload:SuperUpload
    {
        protected override string Run(IFormFile imgFile, string saveUrl)
        {
            imgFile.SaveAs(saveUrl);
            return SUCCESS;
        }

    }
}