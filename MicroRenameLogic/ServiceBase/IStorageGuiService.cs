using System.Collections.Generic;

namespace MicroRenameLogic.ServiceBase;

public interface IStorageGuiService
{
    IEnumerable<string> GetFiles(string dialogMessage, string startingPath = "");

    IEnumerable<string> GetFilesFromFolder(string dialogMessage, string startingPath = "");
}