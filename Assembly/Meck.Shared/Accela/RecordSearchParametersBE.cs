namespace Meck.Shared.Accela
{
    public class RecordSearchParametersBE
    {
        public string type { get; set; }               /// <param name="type">Filter by record type. See [Get All Record Types](./api-settings.html#operation/v4.get.settings.records.types). (optional)</param>
        public string openedDateFrom { get; set; }     /// <param name="openedDateFrom">Filter by the record&#39;s open date range, beginning with this date. (optional)</param>

        public string openedDateTo { get; set; }         /// <param name="openedDateTo">Filter by the record&#39;s open date range, ending with this date. (optional)</param>
        public string customId { get; set; }            /// <param name="customId">Filter by the record custom id. (optional)</param>
        public string module { get; set; }              /// <param name="module">Filter by module. See [Get All Modules](./api-settings.html#operation/v4.get.settings.modules). (optional)</param>
        public string status { get; set; }              /// <param name="status">Filter by record status. (optional)</param>
        public string assignedToDepartment {get; set;}  /// <param name="assignedToDepartment">Filter by the assigned department. (optional)</param>
        public string assignedUser { get; set; }        /// <param name="assignedUser">Filter by the assigned user. (optional)</param>
        public string assignedDateFrom { get; set; }    /// <param name="assignedDateFrom">Filter by the record&#39;s assigned date range starting with this date. (optional)</param>
        public string assignedDateTo { get; set; }      /// <param name="assignedDateTo">Filter by the record&#39;s assigned date range ending with this date. (optional)</param>
        public string completedDateFrom { get; set; }  /// <param name="completedDateFrom">Filter by the record&#39;s completed date range starting with this date. (optional)</param>
        public string completedDateTo { get; set; }    /// <param name="completedDateTo">Filter by the record&#39;s completed date range ending with this date. (optional)</param>
        public string statusDateFrom { get; set; }      /// <param name="statusDateFrom">Filter by the record&#39;s status date range starting with this date. (optional)</param>
        public string statusDateTo { get; set; }          /// <param name="statusDateTo">Filter by the record&#39;s status date range ending with this date. (optional)</param>
        public string completedByDepartment { get; set; } /// <param name="completedByDepartment">Filter by the deparment which completed the application. (optional)</param>
        public string completedByUser { get; set; }       /// <param name="completedByUser">Filter by the user who completed the application. (optional)</param>
        public string closedDateFrom { get; set; }         /// <param name="closedDateFrom">Filter by the record&#39;s closed date range starting with this date. (optional)</param>
        public string closedDateTo { get; set; }        /// <param name="closedDateTo">Filter by the record&#39;s closed date range ending with this date. (optional)</param>
        public string closedByDepartment { get; set; }   /// <param name="closedByDepartment">Filter by the department which closed the application. (optional)</param>
        public string closedByUser { get; set; }        /// <param name="closedByUser">Filter by the user who closed the application. (optional)</param>
        public string recordClass { get; set; }          /// <param name="recordClass">Filter by record class (optional)</param>
        public long? limit { get; set; }                /// <param name="limit">Search result size limit. (optional)</param>
        public long? offset { get; set; }              /// <param name="offset">The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional)</param>
        public string fields { get; set; }             /// <param name="fields">Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional)</param>
        public string lang { get; set; }                ///  for foreign languages, not used  


        public RecordSearchParametersBE()
        {
            type = null;
            openedDateFrom = null;
            openedDateTo = null;
            customId = null;
            module = null;
            status = null;
            assignedToDepartment = null;
            assignedUser = null;
            assignedDateFrom = null;
            assignedDateTo = null;
            completedDateFrom = null;
            completedDateTo = null;
            statusDateFrom = null;
            statusDateTo = null;
            completedByDepartment = null;
            completedByUser = null;
            closedDateFrom = null;
            closedDateTo = null;
            closedByDepartment = null;
            closedByUser = null;
            recordClass = null;
            limit = 25;
            offset = null;
            fields = null;
            lang = null;
        }

    }
}
