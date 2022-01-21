using System;
using System.IO;

namespace SharedNote.Application.Helpers.File
{
    public class FileModel
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public long Length { get; set; }
        public string Extension { get; set; }
        public string DirectoryName { get; set; }
        public DirectoryInfo Directory { get; set; }
        public DateTime CreationTime { get; set; }
    }
}