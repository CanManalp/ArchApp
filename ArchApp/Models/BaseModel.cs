using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArchApp.Models
{
    public class BaseModel
    {
        public string Notlar { get; set; }
        public string Notification { get; set; }
        public string Alert { get; set; }
        public string EntityName { get; set; }
        public string PViewName { get; set; }
        public string CurrentSortOrder { get; set; }
        public string CurrentPrefix { get; set; }
        public int? CurrentPageNumber { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 4;
        public bool IsEdit { get; set; }
        public bool IsAttached { get; set; }
        public SelectList Ulke { get; set; }
        
        //[RegularExpression(@"([a-zA-Z0-9()\s_\\.\-:!@#$%^&])+(.png|.jpg|.gif|.bmp|.tiff|.PNG|.JPG|.GIF|.BMP|.TIFF|.pdf|.doc|.docx|.rtf|.txt|.odt)$", ErrorMessage = "Dosya Uzantısı Hatalı")]
        public HttpPostedFileBase File { get; set; }
    }
}