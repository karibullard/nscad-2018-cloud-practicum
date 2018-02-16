namespace API.Models
{
    using System.IO;

    /// <summary>
    /// Model for Downloading Workflows from Storage Account
    /// </summary>
    public class BlobDownload
    {
        public MemoryStream BlobStream { get; set; }

        public string BlobFileName { get; set; }

        public string BlobContentType { get; set; }

        public long BlobLength { get; set; }
    }
}