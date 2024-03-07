using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.BL
{
    public class MeetingType : ModelBase
    {
        #region Properties

        public int? MeetingTypeRefId { get; set; }

        public string MeetingTypeDesc { get; set; }

        public int? EnumMappingValNbr { get; set; }

        public bool? ActiveInd { get; set; }

        public MeetingTypeEnum MeetingTypeEnum { get; set; }
        #endregion

    }
}