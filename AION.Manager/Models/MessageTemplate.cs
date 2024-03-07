using AION.BL;
using System;

namespace AION.Manager.Models
{
    public class MessageTemplate : ModelBase
    {
        public int? TemplateId { get; set; }

        public string TemplateName { get; set; }

        public int? TemplateTypeId { get; set; }

        public string TemplateText { get; set; }

        public bool? ActiveInd { get; set; }

        public DateTime? ActiveDt { get; set; }

        public string DisplayActiveDate { get; set; }

        public string DisplayActiveTime { get; set; }

        public int? TemplateTypeEnumValNbr { get; set; }

    }
}