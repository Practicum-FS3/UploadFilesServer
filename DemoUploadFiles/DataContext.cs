using Microsoft.EntityFrameworkCore;
using System;

namespace DemoUploadFiles
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
        public DbSet<FileUpload> FileUploads { get; set; }


    }
}
