using AION.Base;
using AION.BL;
using AION.BL.Models;
using AION.Manager.Adapters;
using AION.Manager.Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace AION.Manager.Controllers
{
    [Authorize]
    public class NoteController : BaseApiController
    {
        /// <summary>
        /// Get notes by type for a project id (aion db id) or project number (MMF-000333)
        /// </summary>
        /// <param name="obj">InternalNoteManagerModel</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<Note>))]
        [Route("api/Note/GetProjectNotes")]
        public IHttpActionResult GetProjectNotes(InternalNoteManagerModel obj)
        {
            NoteAdapter thisAdapter = new NoteAdapter();
            var mNotes = thisAdapter.GetAllInternalNotes(obj);

            return Ok(mNotes);
        }

        [HttpPost]
        [ResponseType(typeof(int))]
        [Route("api/Note/InsertCustomerResponse")]
        public IHttpActionResult InsertCustomerResponse(Note note)
        {
            NoteAdapter thisAdapter = new NoteAdapter();

            return Ok(thisAdapter.InsertCustomerResponse(note));
        }

        [HttpGet]
        [ResponseType(typeof(List<StandardNote>))]
        [Route("api/Note/GetStandardNotes")]
        public IHttpActionResult GetStandardNotes(NoteTypeEnum noteTypeEnum, PropertyTypeEnums propertyTypeEnum)
        {
            NoteAdapter thisAdapter = new NoteAdapter();
            var result = thisAdapter.GetStandardNotes(noteTypeEnum, propertyTypeEnum);

            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(List<StandardNoteGroupEnums>))]
        [Route("api/Note/GetStandardNoteGroupEnums")]
        public IHttpActionResult GetStandardNoteGroupEnums()
        {
            NoteAdapter thisAdapter = new NoteAdapter();
            var result = thisAdapter.GetStandardNoteGroupEnums();

            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(List<NoteType>))]
        [Route("api/Note/GetNoteTypeBaseList")]
        public IHttpActionResult GetNoteTypeBaseList()
        {
            NoteAdapter thisAdapter = new NoteAdapter();
            var result = thisAdapter.GetNoteTypeBaseList();

            return Ok(result);
        }

    }
}