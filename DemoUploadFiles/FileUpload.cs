namespace DemoUploadFiles
{
    public class FileUpload
    {
            public int Id { get; set; }
            public string FileName { get; set; }
            public byte[] Data { get; set; }
             public string ContentType { get; set; } // Add this line


    }
}
