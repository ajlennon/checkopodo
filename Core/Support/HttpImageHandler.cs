using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Web;

namespace DynamicDevices.Utilities
{
    public class HttpImageHandler : IHttpHandler
    {
        private string GetExtension(string url)
        {
            string extension = "";
            if (null != url)
            {
                int indexSeperator = url.LastIndexOf(".");
                if (indexSeperator > -1)
                    extension = url.Substring(indexSeperator + 1, url.Length - indexSeperator - 1).ToUpper();
            }
            return extension;
        }

        private string GetContentType(string url)
        {
            switch (GetExtension(url))
            {
                case "JPG":
                case "JPEG":
                    return "image/jpg";
                case "PNG":
                    return "image/png";
                case "GIF":
                    return "image/gif";
                default:
                    return "";
            }
        }

        private ImageFormat GetImageFormat(string url)
        {
            switch (GetExtension(url))
            {
                case "JPG":
                case "JPEG":
                    return ImageFormat.Jpeg;
                case "PNG":
                    return ImageFormat.Png;
                case "GIF":
                    return ImageFormat.Gif;
                default:
                    return ImageFormat.Bmp;
            }
        }

        public Image GetImage(string url)
        {
            HttpWebRequest wReq = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse wRes = (HttpWebResponse)(wReq).GetResponse();
            Stream wStr = wRes.GetResponseStream();
            return Image.FromStream(wStr);
        }

        #region IHttpHandler Members
        public void ProcessRequest(HttpContext context)
        {
            string url = context.Request.QueryString["imageUrl"];
            if (null != url)
            {
                context.Response.Clear();
                context.Response.ContentType = GetContentType(url);
                Image img = GetImage(url);
                img.Save(context.Response.OutputStream, GetImageFormat(url));
            }
            context.Response.End();
        }

        public bool IsReusable
        {
            get { return false; }
        }
        #endregion
    }
}