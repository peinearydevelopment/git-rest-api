namespace PinaryDevelopment.Git.Server.DataAccess
{
    public class LocalFileSystemDirectoryHelper : IDirectoryHelper
    {
        public DirectoryInfo CreateDirectory(string path)
        {
            return Directory.CreateDirectory(path);
        }
    }
}
