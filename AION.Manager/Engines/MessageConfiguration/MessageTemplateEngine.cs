using AION.BL;
using AION.BL.BusinessObjects;
using AION.BL.Common;
using AION.Engine.BusinessObjects;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Models;
using AION.Scheduler.Engine.BusinessEntities;
using AION.Scheduler.Engine.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AION.Manager.Engines
{
    public class MessageTemplateEngine
    {
        public MessageTemplateEngine()
        {

        }

        public Project Project { get; set; }
        public ProjectBE ProjectBE { get; set; }
        public int ProjectId { get; set; }
        public string ProjectScheduleTypDesc { get; set; }
        public int? MeetingTypRefId { get; set; }
        public MeetingTypeEnum MeetingTypeEnum { get; set; }

        //[[PENDING ESTIMATION NOTES]]
        public string PendingEstimationNotes { get; set; }

        //[[PENDING ESTIMATION REASON]]
        public string PendingEstimationReason { get; set; }

        //[[MEETING TIME]]
        public string MeetingTime { get; set; }

        //[[MEETING ROOM]]
        public string MeetingRoom { get; set; }

        //[[MEETING DATE]]
        public string MeetingDate { get; set; }

        //[[MEETING NAME]]
        public string MeetingName { get; set; }

        //[[EXPRESS REVIEW DATE]]
        public string ExpressReviewDate { get; set; }

        //[[GATE DATE]]
        public string GateDate { get; set; }

        //[[PRELIMINARY MEETING ACCEPTANCE DEADLINE]]
        public string PreliminaryMeetingAcceptanceDeadline { get; set; }

        //[[PRELIMINARY MEETING DATE]]
        public string PreliminaryMeetingDate { get; set; }

        //[[PROJECT ADDRESS]]
        public string ProjectAddress { get; set; }

        //[[PROJECT NUMBER]]
        public string ProjectNumber { get; set; }

        //[[PROJECT NAME]]
        public string ProjectName { get; set; }

        //[[PLAN REVIEW ACCEPTANCE DEADLINE]]
        public string PlanReviewAcceptanceDeadline { get; set; }

        //[[PLAN REVIEW START DT]]
        public string PlanReviewStartDt { get; set; }

        //[[PROD]]
        public string PROD { get; set; }

        //[[PLAN REVIEWER NAME W/PHONE LIST]]
        public string PlanReviewerPhoneList { get; set; }

        //[[PLAN REVIEWER EMAIL LIST]]
        public string PlanReviewerEmailList { get; set; }

        //[[PLAN REVIEWER NAME LIST]]
        public string PlanReviewerNameList { get; set; }

        //[[ESTIMATOR PHONE]]
        public string EstimatorPhone { get; set; }

        //[[ESTIMATOR EMAIL]]
        public string EstimatorEmail { get; set; }

        //[[ESTIMATOR NAME]]
        public string EstimatorName { get; set; }

        //[[FACILITATOR PHONE]]
        public string FacilitatorPhone { get; set; }

        //[[FACILITATOR EMAIL]]
        public string FacilitatorEmail { get; set; }

        //[[FACILITATOR NAME]]
        public string FacilitatorName { get; set; }

        //[[CYCLE NUMBER]]
        public string CycleNumber { get; set; }
        //****************************************
        //not in wireframe list
        //if these need to have values, you must send them in when calling the class

        //[[PROJECT MANAGER PHONE]]
        public string ProjectManagerPhone { get; set; }

        //[[PROJECT MANAGER EMAIL]]
        public string ProjectManagerEmail { get; set; }

        //[[PROJECT MANAGER NAME]]
        public string ProjectManagerName { get; set; }

        //[[TIMESTAMP]]
        public string Timestamp { get; set; }

        //[[PLAN REVIEW FEE]]
        public string PlanReviewFee { get; set; }

        //[[NOTES]]
        public string Notes { get; set; }

        //[[PROJECT TYPE]]
        public string ProjectType { get; set; }

        //[[CANCELLATION FEE]]
        public string CancellationFee { get; set; }

        //[[CANCELLATION CALCULATION]]
        public string CancellationCalculation { get; set; }

        //[[CANCELLATION DUE DATE]]
        public string CancellationDueDate { get; set; }

        //[[PLAN REVIEW START DAY]]
        public string PlanReviewStartDay { get; set; }

        //[[BASEURL]]
        public string BaseURL { get; set; }

        public List<MessageTemplateDataElement> DataElements { get; set; }

        public string MessageTemplate
        {
            get
            {
                return GetMessageTemplate();
            }
        }

        public MessageTemplateTypeEnum MessageTemplateTypeEnum { get; set; }

        public List<MessageTemplateAppointmentBE> MessageTemplateAppointments { get; set; }

        public string MessageTemplateDefault { get; set; }

        public void GetProject()
        {
            if (string.IsNullOrWhiteSpace(ProjectNumber))
            {
                //check the project id
                if (ProjectId > 0)
                {
                    ProjectBE = new ProjectBO().GetById(ProjectId);
                    ProjectNumber = ProjectBE.SrcSystemValTxt;
                }
            }
            else
            {
                ProjectBE = new ProjectBO().GetByExternalRefInfo(ProjectNumber);
                if (ProjectId == 0)
                {
                    ProjectId = ProjectBE.ProjectId.Value;

                }

            }

        }
        public void GetMessageTemplateDefaultElements()
        {
            if (string.IsNullOrWhiteSpace(ProjectScheduleTypDesc))
            {
                ProjectScheduleTypDesc = ProjectBE.PreliminaryInd.Value == true ? "PMA" : ProjectBE.ProjectTypRefId.Value == (int)PropertyTypeEnums.Express ? "EMA" : "PR";
            }
            UserIdentity assignedestimator = new UserIdentity();
            UserIdentity assignedfacilitator = new UserIdentity();
            if (ProjectBE.AssignedEstimatorId.HasValue && ProjectBE.AssignedEstimatorId.Value > 0)
            {
                assignedestimator = new UserIdentityModelBO().GetInstance(ProjectBE.AssignedEstimatorId.Value);
            }
            if (ProjectBE.AssignedFacilitatorId.HasValue && ProjectBE.AssignedFacilitatorId.Value > 0)
            {
                assignedfacilitator = new UserIdentityModelBO().GetInstance(ProjectBE.AssignedFacilitatorId.Value);
            }
            //EstimatorName 
            if (string.IsNullOrWhiteSpace(EstimatorName))
            {
                if (assignedestimator != null)
                {
                    EstimatorName = assignedestimator.FirstName + ' ' + assignedestimator.LastName;
                }
            }
            //EstimatorPhone 
            if (string.IsNullOrWhiteSpace(EstimatorPhone))
            {
                if (assignedestimator != null)
                {
                    EstimatorPhone = assignedestimator.Phone;
                }
            }
            //EstimatorEmail 
            if (string.IsNullOrWhiteSpace(EstimatorEmail))
            {
                if (assignedestimator != null)
                {
                    EstimatorPhone = assignedestimator.Email;
                }
            }
            //FacilitatorPhone 
            if (string.IsNullOrWhiteSpace(FacilitatorPhone))
            {
                if (assignedfacilitator != null)
                {
                    FacilitatorPhone = assignedfacilitator.Phone;
                }
            }

            //FacilitatorEmail 
            if (string.IsNullOrWhiteSpace(FacilitatorEmail))
            {
                if (assignedfacilitator != null)
                {
                    FacilitatorEmail = assignedfacilitator.Email;
                }
            }

            //FacilitatorName 
            if (string.IsNullOrWhiteSpace(FacilitatorName))
            {
                if (assignedfacilitator != null)
                {
                    FacilitatorName = assignedfacilitator.FirstName + ' ' + assignedfacilitator.LastName;
                }
            }

            //GateDate 
            if (string.IsNullOrWhiteSpace(GateDate))
            {
                if (ProjectBE.GateDt.HasValue && ProjectBE.GateDt.Value > DateTime.MinValue)
                {
                    GateDate = ProjectBE.GateDt.Value.ToShortDateString();
                }
            }

            //ProjectAddress 
            if (string.IsNullOrWhiteSpace(ProjectAddress))
            {
                if (!string.IsNullOrWhiteSpace(ProjectBE.ProjectAddrTxt))
                {
                    ProjectAddress = ProjectBE.ProjectAddrTxt;
                }
            }
            //ProjectName 
            if (string.IsNullOrWhiteSpace(ProjectName))
            {
                if (!string.IsNullOrWhiteSpace(ProjectBE.ProjectNm))
                {
                    ProjectName = ProjectBE.ProjectNm;
                }
            }

            //PROD 
            if (string.IsNullOrWhiteSpace(PROD))
            {
                if (ProjectBE.PlansReadyOnDt.HasValue && ProjectBE.PlansReadyOnDt.Value > DateTime.MinValue)
                {
                    PROD = ProjectBE.PlansReadyOnDt.Value.ToShortDateString();
                }
            }


            //PendingEstimationReason 
            if (string.IsNullOrWhiteSpace(PendingEstimationReason) && (MessageTemplateTypeEnum == MessageTemplateTypeEnum.Pending_Estimation || MessageTemplateTypeEnum == MessageTemplateTypeEnum.Pending_Preliminary_Estimation))
            {
                ProjectStatus status = new ProjectStatusModelBO().GetInstance(ProjectBE.ProjectStatusRefId.Value);
                PendingEstimationReason = status.ProjectStatusEnum.ToStringValue();
            }

            //BaseURL
            if (string.IsNullOrWhiteSpace(BaseURL))
            {
                BaseURL = new CatalogRefBO().GetByExternalRef("EMAIL").Where(x => x.Key == "EMAILENGINE.BASEURL").FirstOrDefault().Value;

            }
            GetMessageTemplateAppointmentList();
            //don't do any of these if there are no MessageTemplateAppointments since this means the project hasn't been scheduled yet
            if (MessageTemplateAppointments != null && MessageTemplateAppointments.Count > 0)
            {
                ProcessMessageTemplateAppointmentList();
            }

        }
        public void GetMessageTemplateAppointmentList()
        {
            MessageTemplateAppointments = new List<MessageTemplateAppointmentBE>();
            MessageTemplateAppointmentBO messageTemplateAppointmentBO = new MessageTemplateAppointmentBO();
            MessageTemplateAppointments = messageTemplateAppointmentBO.GetList(ProjectId, ProjectScheduleTypDesc, MeetingTypRefId);
        }
        public void ProcessMessageTemplateAppointmentList()
        {
            UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();
            //PlanReviewerPhoneList - name of reviewer and their phone
            //add <br/> between each set
            if (string.IsNullOrWhiteSpace(PlanReviewerPhoneList))
            {
                foreach (MessageTemplateAppointmentBE item in MessageTemplateAppointments.Where(x => x.RowType == "User_Id").ToList())
                {
                    UserIdentity userIdentity = userIdentityModelBO.GetInstance(item.ScheduledUserId.Value);
                    StringBuilder sb = new StringBuilder();
                    sb.Append(userIdentity.FirstName);
                    sb.Append(" ");
                    sb.Append(userIdentity.LastName);
                    sb.Append(" ");
                    sb.Append(userIdentity.Phone);
                    sb.Append("<br/>");
                    PlanReviewerPhoneList += sb.ToString();
                }

            }
            //PlanReviewerEmailList - email list
            if (string.IsNullOrWhiteSpace(PlanReviewerEmailList))
            {
                foreach (MessageTemplateAppointmentBE item in MessageTemplateAppointments.Where(x => x.RowType == "User_Id").ToList())
                {
                    UserIdentity userIdentity = userIdentityModelBO.GetInstance(item.ScheduledUserId.Value);
                    StringBuilder sb = new StringBuilder();
                    sb.Append(userIdentity.Email);
                    sb.Append("<br/>");
                    PlanReviewerEmailList += sb.ToString();
                }

            }

            //PlanReviewerNameList - name list
            if (string.IsNullOrWhiteSpace(PlanReviewerNameList))
            {
                foreach (MessageTemplateAppointmentBE item in MessageTemplateAppointments.Where(x => x.RowType == "User_Id").ToList())
                {
                    UserIdentity userIdentity = userIdentityModelBO.GetInstance(item.ScheduledUserId.Value);
                    StringBuilder sb = new StringBuilder();
                    sb.Append(userIdentity.FirstName);
                    sb.Append(" ");
                    sb.Append(userIdentity.LastName);
                    sb.Append("<br/>");
                    PlanReviewerNameList += sb.ToString();
                }

            }

            //CycleNumber
            if (string.IsNullOrWhiteSpace(CycleNumber) && (ProjectScheduleTypDesc.Equals("PR") || ProjectScheduleTypDesc.Equals("EMA") || ProjectScheduleTypDesc.Equals("FIFO")))
            {
                MessageTemplateAppointmentBE messageTemplateAppointmentBE = MessageTemplateAppointments.Where(x => x.RowType == "Cycle_Nbr").FirstOrDefault();
                if (messageTemplateAppointmentBE.CycleNbr.HasValue)
                {
                    CycleNumber = messageTemplateAppointmentBE.CycleNbr.Value.ToString();

                }
                else
                {
                    CycleNumber = "0";
                }


            }
            bool hasStartDt = MessageTemplateAppointments.Any(x => x.RowType == "Start_Dt");
            MessageTemplateAppointmentBE messageTemplateAppointment = null;
            if (hasStartDt)
            {
                messageTemplateAppointment = MessageTemplateAppointments.FirstOrDefault(x => x.RowType == "Start_Dt" && x.StartDt != null);
            }

            DateTime? startDate = null;
            if (messageTemplateAppointment != null)
            {
                startDate = messageTemplateAppointment.StartDt;
            }

            if (startDate.HasValue)
            {

                //PlanReviewAcceptanceDeadline - 2 business days from start
                if (string.IsNullOrWhiteSpace(PlanReviewAcceptanceDeadline))
                {
                    PlanReviewAcceptanceDeadline = DateTimeHelper.DetermineWorkDateBeforeDateSpecified(startDate.Value, 2).ToShortDateString();

                }
                //PreliminaryMeetingAcceptanceDeadline  - 2 business days from start
                if (string.IsNullOrWhiteSpace(PreliminaryMeetingAcceptanceDeadline))
                {
                    PreliminaryMeetingAcceptanceDeadline = DateTimeHelper.DetermineWorkDateBeforeDateSpecified(startDate.Value, 2).ToShortDateString();

                }

                //PlanReviewStartDt 
                if (string.IsNullOrWhiteSpace(PlanReviewStartDt))
                {
                    PlanReviewStartDt = startDate.Value.ToShortDateString();
                }
                //PlanReviewStartDay
                if (string.IsNullOrWhiteSpace(PlanReviewStartDay))
                {
                    PlanReviewStartDay = startDate.Value.DayOfWeek.ToString();
                }

                //CancellationFee
                if (string.IsNullOrWhiteSpace(CancellationFee))
                {
                    //if nothing passed in, get the current cycle values saved with the project
                    CancellationFee = (ProjectBE.CancellationFee.HasValue && ProjectBE.CancellationFee != null)
                                            ? "$" + ProjectBE.CancellationFee.ToString() : "$0";
                }

                //CancellationCalculation
                if (string.IsNullOrWhiteSpace(CancellationCalculation))
                {
                    //if nothing passed in, get the values for the current cycle
                    if (Project == null)
                    {
                        Project = new ProjectEstimationModelBO().GetInstanceFromAION(ProjectId);
                    }
                    decimal? cancellationFeePerHour = CatalogItemModelBO.GetCancellationFeePerHour();
                    decimal totalHrsEstimated = ProjectModelBaseBO.GetBEMPTotalHoursEstimated(Project);
                    if (cancellationFeePerHour.HasValue)
                    {
                        CancellationCalculation = "$" + cancellationFeePerHour.Value.ToString("0.00") + " * " + totalHrsEstimated.ToString("0.00") + "hrs";
                    }
                }

                //CancellationDueDate
                if (string.IsNullOrWhiteSpace(CancellationDueDate))
                {
                    //<INSERT DAY and CALCULATE 5 BUSINESS DAYS PRIOR TO PLAN REVIEW START DATE>
                    DateTime duedate = DateTimeHelper.DetermineWorkDateBeforeDateSpecified(startDate.Value, 5);
                    CancellationDueDate = duedate.DayOfWeek.ToString() + " " + duedate.ToShortDateString();
                }

                //ExpressReviewDate 
                if (string.IsNullOrWhiteSpace(ExpressReviewDate))
                {

                    ExpressReviewDate = startDate.Value.ToShortDateString();

                }

                //PreliminaryMeetingDate 
                if (string.IsNullOrWhiteSpace(PreliminaryMeetingDate))
                {

                    PreliminaryMeetingDate = startDate.Value.ToShortDateString();

                }

                //MeetingDate 
                if (string.IsNullOrWhiteSpace(MeetingDate))
                {

                    MeetingDate = startDate.Value.ToShortDateString();

                }

                //MeetingTime -- get the time from the meeting date
                if (string.IsNullOrWhiteSpace(MeetingTime))
                {

                    MeetingTime = startDate.Value.ToShortTimeString();

                }

                //MeetingRoom --get the object for the ref id "Start_Dt" rowtype
                if (string.IsNullOrWhiteSpace(MeetingRoom))
                {
                    int? meetingRoomRefId = messageTemplateAppointment.MeetingRoomRefId;
                    if (meetingRoomRefId.HasValue && string.IsNullOrWhiteSpace(MeetingRoom))
                    {
                        BL.Models.MeetingRoom meetingroom = new MeetingRoomBO().GetById(meetingRoomRefId.Value);
                        MeetingRoom = meetingroom.MeetingRoomName;
                    }

                }
                //MeetingName -- meeting type "exit, prepermitting, etc"
                if (string.IsNullOrWhiteSpace(MeetingName))
                {
                    //MeetingTypRefId
                    int? meetingTypRefId = messageTemplateAppointment.MeetingTypRefId;
                    if (meetingTypRefId.HasValue && string.IsNullOrWhiteSpace(MeetingName))
                    {
                        MeetingType meetingType = new MeetingTypeModelBO().GetInstance(meetingTypRefId.Value);
                        MeetingName = meetingType.MeetingTypeEnum.ToStringValue();
                    }

                }

            }
            //PendingEstimationNotes 
            if (string.IsNullOrWhiteSpace(PendingEstimationNotes) && (MessageTemplateTypeEnum == MessageTemplateTypeEnum.Pending_Estimation || MessageTemplateTypeEnum == MessageTemplateTypeEnum.Pending_Preliminary_Estimation))
            {
                foreach (MessageTemplateAppointmentBE item in MessageTemplateAppointments.Where(x => x.RowType == "Pending_Note").ToList())
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(item.PendingNote);
                    sb.Append("<br/>");
                    PendingEstimationNotes += sb.ToString();
                }


            }

        }
        public string GetMessageTemplate()
        {
            if (string.IsNullOrWhiteSpace(MessageTemplateDefault))
            {
                //get the active template by template type id
                MessageTemplateBE messageTemplateBE = new MessageTemplateBO().GetActiveByTypeId((int)MessageTemplateTypeEnum, DateTime.Now);
                return messageTemplateBE.TemplateText;
            }
            else
            {
                return MessageTemplateDefault;
            }
        }
        public void GetDataElements()
        {
            List<MessageTemplateDataElementBE> dataelements = new MessageTemplateDataElementBO().GetList();
            List<MessageTemplateDataElement> returnDataElements = new List<MessageTemplateDataElement>();

            //for each dataelement name that matches the properties, get that property value
            foreach (MessageTemplateDataElementBE dataElement in dataelements)
            {
                MessageTemplateDataElement retDataElement = new MessageTemplateDataElement();
                switch (dataElement.DataElementValTxt)
                {
                    case "[[CYCLE NUMBER]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = CycleNumber;
                        returnDataElements.Add(retDataElement);
                        break;
                    case "[[PENDING ESTIMATION NOTES]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = PendingEstimationNotes;
                        returnDataElements.Add(retDataElement);
                        break;
                    case "[[PENDING ESTIMATION REASON]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = PendingEstimationReason;
                        returnDataElements.Add(retDataElement);
                        break;
                    case "[[MEETING TIME]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = MeetingTime;
                        returnDataElements.Add(retDataElement);
                        break;
                    case "[[MEETING ROOM]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = MeetingRoom;
                        returnDataElements.Add(retDataElement);
                        break;
                    case "[[MEETING DATE]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = MeetingDate;
                        returnDataElements.Add(retDataElement);
                        break;
                    case "[[MEETING NAME]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = MeetingName;
                        returnDataElements.Add(retDataElement);
                        break;
                    case "[[EXPRESS REVIEW DATE]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = ExpressReviewDate;
                        returnDataElements.Add(retDataElement);
                        break;
                    case "[[GATE DATE]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = GateDate;
                        returnDataElements.Add(retDataElement);
                        break;
                    case "[[PRELIMINARY MEETING ACCEPTANCE DEADLINE]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = PreliminaryMeetingAcceptanceDeadline;
                        returnDataElements.Add(retDataElement);
                        break;
                    case "[[PRELIMINARY MEETING DATE]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = PreliminaryMeetingDate;
                        returnDataElements.Add(retDataElement);
                        break;
                    case "[[PROJECT ADDRESS]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = ProjectAddress;
                        returnDataElements.Add(retDataElement);
                        break;
                    case "[[PROJECT NUMBER]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = ProjectNumber;
                        returnDataElements.Add(retDataElement);
                        break;
                    case "[[PROJECT NAME]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = ProjectName;
                        returnDataElements.Add(retDataElement);
                        break;
                    case "[[PLAN REVIEW ACCEPTANCE DEADLINE]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = PlanReviewAcceptanceDeadline;
                        returnDataElements.Add(retDataElement);
                        break;
                    case "[[PLAN REVIEW START DT]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = PlanReviewStartDt;
                        returnDataElements.Add(retDataElement);
                        break;
                    case "[[PROD]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = PROD;
                        returnDataElements.Add(retDataElement);
                        break;
                    case "[[PLAN REVIEWER NAME W/PHONE LIST]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = PlanReviewerPhoneList;
                        returnDataElements.Add(retDataElement);
                        break;
                    case "[[PLAN REVIEWER EMAIL LIST]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = PlanReviewerEmailList;
                        returnDataElements.Add(retDataElement);
                        break;
                    case "[[PLAN REVIEWER NAME LIST]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = PlanReviewerNameList;
                        returnDataElements.Add(retDataElement);
                        break;
                    case "[[ESTIMATOR PHONE]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = EstimatorPhone;
                        returnDataElements.Add(retDataElement);
                        break;
                    case "[[ESTIMATOR EMAIL]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = EstimatorEmail;
                        returnDataElements.Add(retDataElement);
                        break;
                    case "[[ESTIMATOR NAME]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = EstimatorName;
                        returnDataElements.Add(retDataElement);
                        break;
                    case "[[FACILITATOR PHONE]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = FacilitatorPhone;
                        returnDataElements.Add(retDataElement);
                        break;
                    case "[[FACILITATOR EMAIL]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = FacilitatorEmail;
                        returnDataElements.Add(retDataElement);
                        break;
                    case "[[FACILITATOR NAME]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = FacilitatorName;
                        returnDataElements.Add(retDataElement);
                        break;

                    case "[[PROJECT MANAGER NAME]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = ProjectManagerName;
                        returnDataElements.Add(retDataElement);
                        break;

                    case "[[PROJECT MANAGER EMAIL]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = ProjectManagerEmail;
                        returnDataElements.Add(retDataElement);
                        break;

                    case "[[PROJECT MANAGER PHONE]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = ProjectManagerPhone;
                        returnDataElements.Add(retDataElement);
                        break;

                    case "[[TIMESTAMP]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = Timestamp;
                        returnDataElements.Add(retDataElement);
                        break;

                    //Estmated_Fee string in db
                    case "[[PLAN REVIEW FEE]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = PlanReviewFee;
                        returnDataElements.Add(retDataElement);
                        break;

                    case "[[NOTES]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = Notes;
                        returnDataElements.Add(retDataElement);
                        break;

                    case "[[PROJECT TYPE]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = ProjectType;
                        returnDataElements.Add(retDataElement);
                        break;

                    case "[[CANCELLATION FEE]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = CancellationFee;
                        returnDataElements.Add(retDataElement);
                        break;

                    case "[[CANCELLATION CALCULATION]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = CancellationCalculation;
                        returnDataElements.Add(retDataElement);
                        break;

                    case "[[CANCELLATION DUE DATE]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = CancellationDueDate;
                        returnDataElements.Add(retDataElement);
                        break;

                    case "[[PLAN REVIEW START DAY]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = PlanReviewStartDay;
                        returnDataElements.Add(retDataElement);
                        break;
                    case "[[BASEURL]]":
                        retDataElement = ConvertBE(dataElement);
                        retDataElement.ReplacementValTxt = BaseURL;
                        returnDataElements.Add(retDataElement);
                        break;
                    default:
                        break;
                }
            }

            DataElements = returnDataElements;
        }
        public string BuildMessage()
        {
            //get the project
            GetProject();
            //get special data elements values
            GetMessageTemplateDefaultElements();
            //get the values for those data elements
            GetDataElements();
            //replace the data element with the value

            //Use StringBuilder here because it uses less memory
            StringBuilder html = new StringBuilder();

            html.Append(MessageTemplate);

            foreach (MessageTemplateDataElement dataelement in DataElements)
            {
                //replace with the value in the correct property
                html.Replace(dataelement.DataElementValTxt, dataelement.ReplacementValTxt);

            }

            return html.ToString();
        }

        private MessageTemplateDataElement ConvertBE(MessageTemplateDataElementBE dataElementBE)
        {
            return new MessageTemplateDataElement
            {
                DataElementDesc = dataElementBE.DataElementDesc,
                DataElementId = dataElementBE.DataElementId,
                DataElementName = dataElementBE.DataElementName,
                DataElementValTxt = dataElementBE.DataElementValTxt,
                EnumMappingValNbr = dataElementBE.EnumMappingValNbr

            };
        }
    }
}