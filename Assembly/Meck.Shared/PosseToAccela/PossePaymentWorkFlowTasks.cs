using System.Collections.Generic;

namespace Meck.Shared.PosseToAccela
{
    public class PossePaymentWorkFlowTasks
    {

        public List<string> workFlowPaymentTasks { get; set; }

        public PossePaymentWorkFlowTasks(string workFlowTaskName)
        {
            workFlowPaymentTasks.Add(workFlowTaskName);
        }

    }
}
