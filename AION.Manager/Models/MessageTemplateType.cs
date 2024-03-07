namespace AION.Manager.Models
{
    public class MessageTemplateType
    {
        public int? TemplateTypeId { get; set; }

        public string TemplateTypeName { get; set; }

        public string TemplateTypeDesc { get; set; }

        public int? EnumMappingValNbr { get; set; }

        public int? TemplateModuleId { get; set; }

        public bool? IsEditable { get; set; }


    }
}