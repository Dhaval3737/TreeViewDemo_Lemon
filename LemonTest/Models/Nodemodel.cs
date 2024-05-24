using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LemonTest.Models
{
    public class Nodemodel
    {
        public int NodeId { get; set; }
        public string NodeName { get; set; }
        public int? ParentNodeId { get; set; }
        public string ParentNodeName { get; set; }
        [NotMapped]
        public List<SelectListItem> PrentNodeList { get; set; } = new List<SelectListItem>();
        public bool IsActive { get; set; } = true;

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }
    }

    public class ParentNodeModel
    {
        public int? ParentNodeId { get; set;}
        public string ParentNodeName { get; set;}
    }

    public class NodeListModel
    {
        public List<Nodemodel> NodeList { get; set; }
    }
}