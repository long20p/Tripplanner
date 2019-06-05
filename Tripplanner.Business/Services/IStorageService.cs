﻿using System;
using System.Collections.Generic;
using System.Text;
using Tripplanner.Business.Configs;

namespace Tripplanner.Business.Services
{
    public interface IStorageService
    {
        IEnumerable<string> GetFilesInFolder(string folder);
        bool SaveTextFile(string relativeFilePath, string content);
        string LoadTextFile(string relativeFilePath);
        bool DeleteTextFile(string relativeFilePath);
        string GetBitmapFullFilePath(string relativeFilePath);
        bool SaveBitmapFile(string relativeFilePath, object content, ImageFormat format);
        object LoadBitmapFile(string relativeFilePath);
        bool DeleteBitmapFile(string relativeFilePath);
    }
}
