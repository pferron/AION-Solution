using AION.BL.Models;
using AION.Estimator.Engine.BusinessEntities;
using System.Collections.Generic;
using System.Linq;

namespace AION.BL.BusinessObjects
{
    public class StandardNoteBO
    {
        private Estimator.Engine.BusinessObjects.StandardNoteBO _standardNoteBO;
        private NoteTypeEnum _notetype;
        private int _notetypeid;
        List<StandardNoteBE> _standardnotesbe;
        List<StandardNote> _standardnotes;
        List<StandardNoteGroupEnums> _standardnotegroupenums;
        public StandardNoteBO()
        {
            _standardNoteBO = new Estimator.Engine.BusinessObjects.StandardNoteBO();
        }
        public NoteTypeEnum NoteType
        {
            get
            {
                SetStandardNoteType();
                return _notetype;
            }
            set
            {
                _notetype = value;
            }
        }
        public List<StandardNote> StandardNotes
        {
            get
            {
                _notetypeid = (int)_notetype;
                _standardnotesbe = _standardNoteBO.GetListByType(_notetypeid);
                //convert BE to model
                ConvertStandardNotesToModel();
                return _standardnotes;
            }
        }
        public List<StandardNote> GetStandardNotes(NoteTypeEnum noteTypeEnum)
        {
            NoteType = noteTypeEnum;
            return StandardNotes;
        }
        public List<StandardNoteGroupEnums> StandardNoteGroupEnums
        {
            get
            {
                if (_standardnotegroupenums == null)
                {
                    _standardnotegroupenums = new List<StandardNoteGroupEnums>();
                    _standardnotegroupenums.Add(BL.StandardNoteGroupEnums.First_Cycle_Packaging_Instructions);
                    _standardnotegroupenums.Add(BL.StandardNoteGroupEnums.ReReview_Packaging_Instructions);
                    _standardnotegroupenums.Add(BL.StandardNoteGroupEnums.RTAP_Packaging_Instructions);
                    _standardnotegroupenums.Add(BL.StandardNoteGroupEnums.Express_Review_Packaging_Instructions);
                    _standardnotegroupenums.Add(BL.StandardNoteGroupEnums.Charlotte_City_Zoning);
                    _standardnotegroupenums.Add(BL.StandardNoteGroupEnums.Towns);
                    _standardnotegroupenums.Add(BL.StandardNoteGroupEnums.County_Engineering);
                    _standardnotegroupenums.Add(BL.StandardNoteGroupEnums.Charlotte_City_Engineering);
                    _standardnotegroupenums.Add(BL.StandardNoteGroupEnums.Commercial_Plan_Review_Link);
                    _standardnotegroupenums.Add(BL.StandardNoteGroupEnums.Health_Department);
                    _standardnotegroupenums.Add(BL.StandardNoteGroupEnums.CMUD_Backflow);

                }

                return _standardnotegroupenums;
            }
        }
        #region "private methods"
        private void SetStandardNoteType()
        {
            if (_notetype != NoteTypeEnum.EstimationStandardNotes && _notetype != NoteTypeEnum.SchedulingStandardNotes)
                _notetype = NoteTypeEnum.EstimationStandardNotes;
        }
        private void ConvertStandardNotesToModel()
        {
            _standardnotes = _standardnotesbe.Select(x => new StandardNote
            {
                StandardNoteGroupName = x.StandardNoteGroupName,
                StandardNoteId = (int)x.StandardNoteId,
                StandardNoteText = x.StandardNoteText,
                StandardNoteType = (x.NoteTypeRefId != null) ? (NoteTypeEnum)x.NoteTypeRefId : NoteTypeEnum.NA,
                StandardNoteGroupEnum = (StandardNoteGroupEnums)System.Enum.Parse(typeof(StandardNoteGroupEnums), x.StandardNoteGroupName),
                StandardNoteTitle = x.StandardNoteTitleText,
                StandardNoteGroupDesc = (x.StandardNoteGroupName != null) ? x.StandardNoteGroupName.Replace('_', ' ') : "",
                PropertyTypeEnum = x.ProjectTypRefId.HasValue ? (PropertyTypeEnums)x.ProjectTypRefId.Value : PropertyTypeEnums.NA
            }).ToList();
        }
        #endregion "private methods"
    }
}
