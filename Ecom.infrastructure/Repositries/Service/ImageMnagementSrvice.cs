using Ecom.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositries.Service
{
    public class ImageMnagementSrvice : IImageMnagementSrvice
    {
        private readonly IFileProvider fileProvider;
        public ImageMnagementSrvice(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }
        public async Task<List<string>> AddImageAsync(IFormFileCollection files, string src)
        {
            var SaveImageSrc = new List<string>();
            var ImageDir = Path.Combine("wwwroot", "Images", src);
            if (!Directory.Exists(ImageDir))
                Directory.CreateDirectory(ImageDir);
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var ImageName = file.FileName;
                    var ImageSrc = $"/Images/{src}/{ImageName}";
                    var root = Path.Combine(ImageDir, ImageName);
                    using (var stream = new FileStream(root, FileMode.Create))
                        await file.CopyToAsync(stream);
                    SaveImageSrc.Add(ImageSrc);
                }

            }
            return SaveImageSrc;
        }

        public void DeleteImageAsync(string src)
        {
            var info = fileProvider.GetFileInfo(src);
            var root = info.PhysicalPath;
            File.Delete(root);
        }
    }
}
