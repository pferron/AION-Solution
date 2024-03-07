using AION.BL;
using AION.BL.BusinessObjects;
using AION.BL.Common;
using AION.BL.Models;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Models;
using Meck.Logging;
using Meck.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AION.Manager.Adapters
{
    public class NoteAdapter : BaseManagerAdapter
    {
        public List<Note> GetAllInternalNotes(InternalNoteManagerModel obj)
        {
            List<Note> ret;
            NoteModelBO noteBO = new NoteModelBO();
            ret = noteBO.GetInstanceByProject(obj);
            return ret;
        }
        public int InsertProjectNote(Note note)
        {
            try
            {
                NoteModelBO bo = new NoteModelBO();

                int noteId = bo.InsertProjectNote(note);

                return noteId;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in InsertProjectNote - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }
        /// <summary>
        /// This will only be used by Customer Response to Pending and when this note is entered, the status of the trades/agencies will
        ///     be set to "CR" to indicate the customer responded.
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public int InsertCustomerResponse(Note note)
        {
            try
            {
                NoteModelBO bo = new NoteModelBO();
                Helper helper = new Helper();
                //insert the note
                int noteId = bo.InsertProjectNote(note);

                ProjectBusinessRelationshipBO projectBusinessRelationshipBO = new ProjectBusinessRelationshipBO();
                List<ProjectBusinessRelationshipBE> projectBusinessRelationshipBEs = new List<ProjectBusinessRelationshipBE>();

                bool bemp = helper.BEMPDepartmentNames.Any(x => x == note.DeptNameEnum);
                if (bemp)
                {
                    //get all the trades
                    projectBusinessRelationshipBEs = projectBusinessRelationshipBO.GetListByProjectId(note.ProjectID)
                        .Where(x => helper.BEMPDepartmentNames.Contains((DepartmentNameEnums)x.BusinessRefId)).ToList();

                }
                else
                {
                    //get just the one to the list
                    projectBusinessRelationshipBEs = projectBusinessRelationshipBO.GetListByProjectId(note.ProjectID)
                        .Where(x => x.BusinessRefId == (int)note.DeptNameEnum).ToList();

                }
                //update each in the list
                foreach (ProjectBusinessRelationshipBE projectBusinessRelationshipBE in projectBusinessRelationshipBEs)
                {
                    projectBusinessRelationshipBE.ProjectBusinessRelationshipStatusDesc = ProjectDisplayStatus.CustomerResponded;
                    projectBusinessRelationshipBO.Update(projectBusinessRelationshipBE);
                }

                return noteId;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in InsertCustomerResponse - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }
        public List<StandardNote> GetStandardNotes(NoteTypeEnum noteTypeEnum, PropertyTypeEnums propertyTypeEnum)
        {
            try
            {
                BL.BusinessObjects.StandardNoteBO bo = new BL.BusinessObjects.StandardNoteBO();
                bo.NoteType = noteTypeEnum;
                List<StandardNote> notes = bo.GetStandardNotes(noteTypeEnum);
                if (propertyTypeEnum != PropertyTypeEnums.NA)
                    return notes.Where(x => x.PropertyTypeEnum == propertyTypeEnum).ToList();
                return notes;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in GetStandardNotes - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public List<StandardNoteGroupEnums> GetStandardNoteGroupEnums()
        {
            try
            {
                AION.BL.BusinessObjects.StandardNoteBO bo = new AION.BL.BusinessObjects.StandardNoteBO();
                return bo.StandardNoteGroupEnums;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in GetStandardNoteGroupEnums - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }
        public List<NoteType> GetNoteTypeBaseList()
        {
            try
            {
                NoteTypeModelBO bo = new NoteTypeModelBO();
                return bo.BaseList;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in GetNoteTypeBaseList - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

    }
}