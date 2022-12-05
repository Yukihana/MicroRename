using System.Collections.Generic;

namespace MicroRenameLogic.FrontendInterfaces
{
    public interface IFileSystemHandler
    {
        // File check methods
        bool FileExists(string filename);


        // File operations
        bool MoveFile(string oldPath, string newPath, bool overwrite);


        // Get operations
        string GetFile(string dialogMessage, string startingPath = null);
        string GetFolder(string dialogMessage, string startingPath = null);
        IEnumerable<string> GetFiles(string dialogMessage, string startingPath = null);
        IEnumerable<string> GetFilesFromFolder(string dialogMessage, string startingPath = null);


        // Path parsing operations
        string GetFilename(string path);
        string GetDirectory(string path);
        string GetExtension(string path);


        // Path generating operations
        string CombinePath(string filename, string directory);
    }
}