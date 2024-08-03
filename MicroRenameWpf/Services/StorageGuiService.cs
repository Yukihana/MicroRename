using MicroRenameWpf.Win32Interop;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using FolderBrowserDialog = System.Windows.Forms.FolderBrowserDialog;
using FormDialogResult = System.Windows.Forms.DialogResult;
using IStorageGuiService = MicroRenameLogic.ServiceBase.IStorageGuiService;

namespace MicroRenameWpf.Services;

public class StorageGuiService : IStorageGuiService
{
    public IEnumerable<string> GetFiles(string dialogMessage, string startingPath = "")
    {
        List<string> filenames = new();
        OpenFileDialog o = new()
        {
            Title = dialogMessage,
            InitialDirectory = startingPath,
            Filter = "AllFiles (*.*)|*.*",
            CheckFileExists = true,
            Multiselect = true
        };

        if (o.ShowDialog() == true)
            filenames.AddRange(o.FileNames);

        return filenames;
    }

    public IEnumerable<string> GetFilesFromFolder(string dialogMessage, string startingPath = "")
    {
        List<string> filenames = new();

        using FolderBrowserDialog o = new()
        {
            Description = dialogMessage,
            ShowNewFolderButton = true,
            SelectedPath = startingPath,
        };

        if (o.ShowDialog() == FormDialogResult.OK)
        {
            filenames.AddRange(Directory.GetFiles(o.SelectedPath));
        }

        filenames.Sort(new NaturalStringComparer());
        return filenames;
    }
}