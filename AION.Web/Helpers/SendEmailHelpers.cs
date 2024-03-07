using AION.Base;
using AION.Web.Models;
using System;
using System.Web.Mvc;

namespace AION.Web.Helpers
{
    [Authorize]
    public class SendEmailHelpers : BaseController
    {

        /// <summary>
        /// Composes the Notes section sent with Pending Email
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public string ComposeCustomerPendingNoteMessage(EstimationSaveViewModel vm)
        {
            string ret = "";
            if (vm.BEMPApplicationNotes != null)
            {
                if (!string.IsNullOrEmpty(vm.BEMPApplicationNotes.PendingNotesComments))
                {
                    ret = ret + "[BEMP Pending Notes] : " + vm.BEMPApplicationNotes.PendingNotesComments + Environment.NewLine;
                }
            }

            if (vm.FireApplicationNotes != null)
            {
                if (!string.IsNullOrEmpty(vm.FireApplicationNotes.PendingNotesComments))
                {
                    ret = ret + "[Fire Pending Notes] : " + vm.FireApplicationNotes.PendingNotesComments + Environment.NewLine;
                }
            }

            if (vm.ZoningApplicationNotes != null)
            {
                if (!string.IsNullOrEmpty(vm.ZoningApplicationNotes.PendingNotesComments))
                {
                    ret = ret + "[Zoning Pending Notes] : " + vm.ZoningApplicationNotes.PendingNotesComments + Environment.NewLine;
                }
            }

            if (vm.BackFlowApplicationNotes != null)
            {
                if (!string.IsNullOrEmpty(vm.BackFlowApplicationNotes.PendingNotesComments))
                {
                    ret = ret + "[BackFlow Pending Notes] : " + vm.BackFlowApplicationNotes.PendingNotesComments + Environment.NewLine;
                }
            }

            if (vm.EHSApplicationNotes != null)
            {
                if (!string.IsNullOrEmpty(vm.EHSApplicationNotes.PendingNotesComments))
                {
                    ret = ret + "[EHS Pending Notes] : " + vm.EHSApplicationNotes.PendingNotesComments + Environment.NewLine;
                }
            }
            return ret;
        }

    }
}