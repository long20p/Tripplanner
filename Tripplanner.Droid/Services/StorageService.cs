using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Android.Graphics;
using Tripplanner.Business;
using Tripplanner.Business.Configs;
using Tripplanner.Business.Services;
using Tripplanner.Business.Utils;
using ImageFormat = Tripplanner.Business.Configs.ImageFormat;
using Path = System.IO.Path;

namespace Tripplanner.Droid.Services
{
    class StorageService : IStorageService
    {
        public StorageService()
        {
            RootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Tripplanner");
            if (!Directory.Exists(RootPath))
            {
                Directory.CreateDirectory(RootPath);
            }
        }

        public string RootPath { get; }

        public IEnumerable<string> GetFilesInFolder(string folder, bool includeExtension = true)
        {
            var fullPath = Path.Combine(RootPath, folder);
            if (!Directory.Exists(fullPath))
                return null;

            var dirInfo = new DirectoryInfo(fullPath);
            return dirInfo.GetFiles().Select(x =>
                includeExtension 
                    ? x.Name 
                    : string.IsNullOrEmpty(x.Extension) 
                        ? x.Name 
                        : x.Name.Substring(0, x.Name.LastIndexOf('.')));
        }

        public bool SaveTextFile(string relativeFilePath, string content)
        {
            var filePath = Path.Combine(RootPath, relativeFilePath);
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
            var filePath = Path.Combine(RootPath, relativeFilePath);
            return File.ReadAllText(filePath);
        }

        public bool DeleteTextFile(string relativeFilePath)
        {
            var filePath = Path.Combine(RootPath, relativeFilePath);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            return true;
        }

        public void SaveZipFile(string relativeFilePath, IEnumerable<ArchiveEntry> entries)
        {
            var filePath = Path.Combine(RootPath, relativeFilePath);
            var dir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using (var zipFile = new FileStream(filePath, FileMode.Create))
            {
                using (var archive = new ZipArchive(zipFile, ZipArchiveMode.Update))
                {
                    foreach (var entry in entries)
                    {
                        var record = archive.CreateEntry(entry.Name);
                        if (entry.Type == ArchiveEntryType.Text)
                        {
                            using (var writer = new StreamWriter(record.Open()))
                            {
                                writer.Write(entry.Content);
                            }
                        }
                        else if (entry.Type == ArchiveEntryType.Binary)
                        {
                            if (entry.Content is byte[] bytes)
                            {
                                using (var writer = new BinaryWriter(record.Open()))
                                {
                                    writer.Write(bytes);
                                }
                            }
                        }
                    }
                }
            }
        }

        public IEnumerable<ArchiveEntry> GetZipContents(string relativeFilePath)
        {
            var filePath = Path.Combine(RootPath, relativeFilePath);
            if (!File.Exists(filePath))
            {
                return null;
            }

            var entries = new List<ArchiveEntry>();
            using (var zipFile = new FileStream(filePath, FileMode.Open))
            {
                using (var archive = new ZipArchive(zipFile, ZipArchiveMode.Read))
                {
                    foreach (var entry in archive.Entries)
                    {
                        if (entry.IsText())
                        {
                            using (var reader = new StreamReader(entry.Open()))
                            {
                                var content = reader.ReadToEnd();
                                entries.Add(new ArchiveEntry(entry.Name, ArchiveEntryType.Text, content));
                            }
                        }
                        else
                        {
                            using (var reader = new MemoryStream())
                            {
                                using (var entryStream = entry.Open())
                                {
                                    entryStream.CopyTo(reader);
                                    var content = reader.ToArray();
                                    entries.Add(new ArchiveEntry(entry.Name, ArchiveEntryType.Binary, content));
                                }
                            }
                        }
                    }
                }
            }

            return entries;
        }

        public bool SaveBitmapFile(string relativeFilePath, object content, ImageFormat format)
        {
            var photoDir = Path.Combine(RootPath, Constants.FolderPhotos);
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
            return Path.Combine(RootPath, Constants.FolderPhotos, relativeFilePath);
        }

        public object LoadBitmapFile(string relativeFilePath)
        {
            var filePath = Path.Combine(RootPath, Constants.FolderPhotos, relativeFilePath);
            return BitmapFactory.DecodeFile(filePath);
        }

        public bool DeleteBitmapFile(string relativeFilePath)
        {
            var filePath = Path.Combine(RootPath, Constants.FolderPhotos, relativeFilePath);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return true;
        }
    }
}