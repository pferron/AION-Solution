using Meck.Shared.Accela;
using System.Collections.Generic;

namespace Meck.Shared.PosseToAccela
{
    public class PosseCustomTasksAdd
    {
        public RequestCustomTablesTasksBE CreateRTAPAdd(string RecordId, string TaskID, List<Dictionary<string,string>> DataForAdd)
        {
            string idvalue = @"CE_RES-RTAP.cREQUESTS";

            // ResponseTaskItemModelBE UpDateRecordWorkFlowItem(AccelaWorkFlowTaskUpdate mWorkFlowTaskUpdate);
            RequestCustomTablesTasksBE mRequestCustomTablesTasksBe = new RequestCustomTablesTasksBE();

            List<TableFieldBE> mFieldBE = new List<TableFieldBE>();

            mFieldBE.Add(new TableFieldBE(null, "id", TaskID));

            foreach (var addDetail in DataForAdd)
            {
                foreach (var addFieldDetail in addDetail)
                {
                    mFieldBE.Add(new TableFieldBE(null, addFieldDetail.Key.ToString(), addFieldDetail.Value));
                }

                TableRowsBE mTableRowsBE = new TableRowsBE(idvalue, TableRowBE.ActionEnum.Add, mFieldBE);

                mRequestCustomTablesTasksBe.array.Add(mTableRowsBE);
            }

            mRequestCustomTablesTasksBe.recordId = RecordId;
            
            return mRequestCustomTablesTasksBe;
        }
    }
}
