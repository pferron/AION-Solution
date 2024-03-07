using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Models;
using Meck.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AION.BL.BusinessObjects
{
    public class NoteModelBO : ModelBaseModelBO, INoteBO
    {
        public Note GetInstance(int ID)
        {
            Note ret = new Note();
            NotesBO bo = new NotesBO();
            NotesBE be = bo.GetById(ID);
            if (be == null || be.NotesId == 0)
                return new Note();
            return ConvertBEToModel(be);
        }

        public Note CreateInstance(NoteTypeEnum noteType, string noteComment, UserIdentity createdUser, UserIdentity updatedUser, DepartmentNameEnums deptNameEnum, int projectID = -1, int parentID = 0)
        {

            Note ret = new Note();
            ret.ProjectID = projectID;
            ret.Attachments = new List<NotesAttachments>();
            ret.NotesType = new NoteTypeModelBO().GetInstance(noteType);
            ret.NotesComments = noteComment;
            ret.CreatedDate = DateTime.Now;
            ret.CreatedUser = createdUser;
            ret.UpdatedDate = DateTime.Now;
            ret.UpdatedUser = updatedUser;
            ret.ParentNoteID = parentID;
            ret.DeptNameEnum = deptNameEnum;
            return ret;
        }

        public Note GetInstance()
        {
            return new Note();
        }

        public List<Note> GetInstanceByProject(InternalNoteManagerModel obj)
        {
            List<Note> ret = new List<Note>();
            NotesBO bo = new NotesBO();
            //jcl send 0 if getting all the notes
            int? noteTypeEnumInt = obj.NoteTypeEnum == null ? 0 : (int?)obj.NoteTypeEnum;
            List<NotesBE> be = bo.GetAllNotesByProject(obj.ProjectId, noteTypeEnumInt, obj.ProjectNumber);
            if (be == null || be.Count == 0)
                return new List<Note>();
            foreach (var item in be)
            {
                ret.Add(ConvertBEToModel(item));
            }
            return ret;
        }

        public Note ConvertBEToModel(NotesBE be)
        {
            var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            Note ret = new Note();
            InjectBaseObjects(ret, be.NotesId.Value, be.CreatedDate.Value, be.UpdatedDate.Value, be.CreatedByWkrId, be.UpdatedByWkrId);
            ret.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc((DateTime)ret.CreatedDate, easternZone);
            ret.UpdatedDate = TimeZoneInfo.ConvertTimeFromUtc((DateTime)ret.UpdatedDate, easternZone);
            ret.CreatedUser = new UserIdentityModelBO().GetInstance(int.Parse(be.CreatedByWkrId));
            ret.ID = be.NotesId.Value;
            ret.NotesComments = be.NotesComment;
            ret.NotesType = new NoteTypeModelBO().GetInstance(be.NotesTypeRefId.Value);
            ret.ProjectID = be.ProjectId.Value;
            ret.ParentNoteID = be.ParentNoteID;
            ret.DeptNameEnum = (DepartmentNameEnums)be.BusinessRefID;
            return ret;
        }

        public NotesBE ConvertModelToBe(Note model)
        {
            NotesBE ret = new NotesBE();
            ret.CreatedDate = model.CreatedDate;
            ret.CreatedByWkrId = model.CreatedUser.ID.ToString();
            ret.UpdatedDate = model.UpdatedDate;
            ret.UpdatedByWkrId = model.UpdatedUser.ID.ToString();
            ret.NotesId = model.ID;
            ret.NotesComment = model.NotesComments;
            ret.NotesTypeRefId = model.NotesType.ID;
            ret.ProjectId = model.ProjectID;
            ret.ParentNoteID = model.ParentNoteID;
            ret.BusinessRefID = (int)model.DeptNameEnum;
            return ret;
        }
        /// <summary>
        /// Inserts Note for Project
        /// ProjectID
        /// </summary>
        /// <param name="note"></param>
        public int InsertProjectNote(Note note)
        {
            try
            {
                int NoteID = 0;

                NotesBO bo = new NotesBO();

                NotesBE be = ConvertModelToBe(note);

                NoteID = bo.Create(be);

                return NoteID;
            }
            catch (Exception ex)
            {
                var errdata = JsonConvert.SerializeObject(note);
                string errorMessage = "Error  in InsertProjectNote : Data:" + errdata + " - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }

        }
    }

    public interface INoteBO
    {
        Note GetInstance();

        Note GetInstance(int ID);
        List<Note> GetInstanceByProject(InternalNoteManagerModel obj);

        Note ConvertBEToModel(NotesBE be);

        NotesBE ConvertModelToBe(Note model);

        Note CreateInstance(NoteTypeEnum noteType, string noteComment, UserIdentity createdUser, UserIdentity updatedUser, DepartmentNameEnums deptNameEnum, int projectID = -1, int parentID = 0);
        int InsertProjectNote(Note note);
    }
}
