using AION.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AION.Web.Models
{
    public class MultiSelectViewModel
    {
        public List<SelectListItem> FromList { get; set; }
        public List<SelectListItem> ToList { get; set; }
        public string PageName { get; set; }
        public string TitleFromList { get; set; }
        public string TitleToList { get; set; }
        public string NameFromList { get; set; }
        public string NameToList { get; set;}
        public bool DisplayMoveUpDown { get; set; }
    }
}