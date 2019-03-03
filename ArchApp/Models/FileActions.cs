using ArchApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArchApp.Models
{
    public class FileActions
    {
        public string FileCheckResult { get; set; }
        public bool FileCheckRes { get; set; }
      
        public FileActions FileCheck(HttpPostedFileBase file)
        {
            var allowedExtensions = new[] { ".pdf", ".doc", ".docx" };
            int allowedFileSize = 210000000;
            var checkextension = Path.GetExtension(file.FileName).ToLower();
            FileActions fileAct = new FileActions();
            if (!allowedExtensions.Contains(checkextension))
            {
               
               
                fileAct.FileCheckResult = "Dosya Uzantısı Hatalı";
                fileAct.FileCheckRes = false;
                return fileAct;
            }

            if (file.ContentLength > allowedFileSize)
            {
                fileAct.FileCheckResult = "Dosya Uzantısı Hatalı";
                fileAct.FileCheckRes = false;
                return fileAct;
            }
            fileAct.FileCheckRes = true;
            return fileAct;
        }

    }
}