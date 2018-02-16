namespace API.Models
{
    using System;

    /// <summary>
    /// Model for Uploading Workflows to Storage Account
    /// </summary>
    public class BlobUpload
    {
        public string FileName { get; set; }

        public string FileUrl { get; set; }

        public long FileSizeInBytes { get; set; }

        public long FileSizeInKb { get { return (long)Math.Ceiling((double)FileSizeInBytes / 1024); } }
    }
}