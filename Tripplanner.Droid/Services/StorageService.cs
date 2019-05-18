using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Android.Graphics;
using Tripplanner.Business;
using Tripplanner.Business.Services;
using ImageFormat = Tripplanner.Business.Configs.ImageFormat;
using Path = System.IO.Path;

namespace Tripplanner.Droid.Services
{
    class StorageService : IStorageService
    {
        private string documentsRootPath;

        public StorageService()
        {
            documentsRootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Tripplanner");
            if (!Directory.Exists(documentsRootPath))
            {
                Directory.CreateDirectory(documentsRootPath);
            }
        }

        public IEnumerable<string> GetFilesInFolder(string folder)
        {
            var fullPath = Path.Combine(documentsRootPath, folder);
            if (!Directory.Exists(fullPath))
                return null;

            var dirInfo = new DirectoryInfo(fullPath);
            return dirInfo.GetFiles().Select(x => x.Name);
        }

        public bool SaveTextFile(string relativeFilePath, string content)
        {
            var filePath = Path.Combine(documentsRootPath, relativeFilePath);
            var dir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            File.WriteAllText(filePath, content);
            return true;
        }

        public string LoadTextFile(string relativeFilePath)
        {
            var filePath = Path.Combine(documentsRootPath, relativeFilePath);
            return File.ReadAllText(filePath);
        }

        public bool DeleteTextFile(string relativeFilePath)
        {
            var filePath = Path.Combine(documentsRootPath, relativeFilePath);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            return true;
        }

        public bool SaveBitmapFile(string relativeFilePath, object content, ImageFormat format)
        {
            var photoDir = Path.Combine(documentsRootPath, Constants.FolderPhotos);
            if (!Directory.Exists(photoDir))
            {
                Directory.CreateDirectory(photoDir);
            }
            var filePath = Path.Combine(photoDir, relativeFilePath);
            if (File.Exists(filePath))
            {
                return true;
            }

            var photo = content as Bitmap;
            if (photo != null)
            {

                using (var fs = new FileStream(filePath, FileMode.CreateNew))
                {
                    photo.Compress(
                        format == ImageFormat.JPG ? Bitmap.CompressFormat.Jpeg : Bitmap.CompressFormat.Png, 70, fs);
                }
            }
            return true;
        }

        public string GetBitmapFullFilePath(string relativeFilePath)
        {
            return Path.Combine(documentsRootPath, Constants.FolderPhotos, relativeFilePath);
        }

        public object LoadBitmapFile(string relativeFilePath)
        {
            var filePath = Path.Combine(documentsRootPath, Constants.FolderPhotos, relativeFilePath);
            return BitmapFactory.DecodeFile(filePath);
        }

        public bool DeleteBitmapFile(string relativeFilePath)
        {
            var filePath = Path.Combine(documentsRootPath, Constants.FolderPhotos, relativeFilePath);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return true;
        }
    }
}