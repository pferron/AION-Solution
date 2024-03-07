using AION.Base;
using AION.BL;
using AION.BL.Models;
using AION.Web.Helpers;
using AION.Web.Models.Shared;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace AION.Web.Controllers
{
    [Authorize(Roles = "User")]

    public class NoteController : BaseControllerWeb
    {
        /// <summary>
        /// Main Page
        /// </summary>
        /// <param name="parms">ProjectParms</param>
        /// <returns></returns>
        public ActionResult NotesMain(ProjectParms parms)
        {
            NotesViewModel notesViewModel = GetNotesViewModel(parms);
            //if logged in user is null then return home controller, don't allow customers
            if (notesViewModel.LoggedInUser.ID <= 0 || notesViewModel.PermissionMapping.IsCustomer)
                return RedirectToAction("Index", "Home", new { StatusMessage = "Please log in" });

            //notesViewModel.Project.ID = int.Parse(parms.ProjectId);
            notesViewModel.Project.AccelaProjectRefId = parms.ProjectNumber;

            return View(notesViewModel);

        }

        /// <summary>
        /// Get all the notes for a project
        /// </summary>
        /// <param name="parms">ProjectParms</param>
        /// <returns></returns>
        private NotesViewModel GetNotesViewModel(ProjectParms parms)
        {
            NotesViewModel notesViewModel = new NotesViewModel(); 

            SetUpViewModelBase<NotesViewModel>(notesViewModel);

            if (notesViewModel.LoggedInUser.ID > 0)
            {
                if (!string.IsNullOrWhiteSpace(parms.ProjectId) || !string.IsNullOrWhiteSpace(parms.ProjectNumber))
                {
                    int projectid = 0;
                    if (!string.IsNullOrWhiteSpace(parms.ProjectId))
                    {
                        projectid = int.Parse(parms.ProjectId);
                    }
                    notesViewModel.NotesComments = NoteAPIHelper.GetProjectNotes(projectid, parms.NoteTypeEnum, parms.ProjectNumber);
                }
            }
            return notesViewModel;
        }
    }
}