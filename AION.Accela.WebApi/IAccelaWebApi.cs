using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using AION.Accela.WebApi;
  

namespace AION.Accela.WebApi
{
   public interface IAccelaWebApi
    {
        IHttpActionResult PingTest();
        IHttpActionResult InsertAIONRecord(string inBoundMessage);

        IHttpActionResult InsertAIONPlanReviewHistoryRecord(string inBoundMessage);

        IHttpActionResult AuditPlanReviewHistory();

        IHttpActionResult AuditReceivedRecordsQueUe();
    }
}
