

function setPlanReviewAutoScheduledAuditFromResponse(ret) {
    $("#auditAutoScheduleButton").val(true);
    $("#auditBuildingUserID").val(ret.BuildingUserID);
    $("#auditElectricUserID").val(ret.ElectricUserID);
    $("#auditMechUserID").val(ret.MechUserID);
    $("#auditPlumbUserID").val(ret.PlumbUserID);
    $("#auditZoneUserID").val(ret.ZoneUserID);
    $("#auditFireUserID").val(ret.FireUserID);
    $("#auditFoodServiceUserID").val(ret.FoodServiceUserID);
    $("#auditPoolUserID").val(ret.PoolUserID);
    $("#auditFacilityUserID").val(ret.FacilityUserID);
    $("#auditDayCareUserID").val(ret.DayCareUserID);
    $("#auditBackFlowUserID").val(ret.BackFlowUserID);

    $("#auditBuildingScheduleStart").val(ret.BuildingScheduleStartTxt);
    $("#auditBuildingScheduleEnd").val(ret.BuildingScheduleEndTxt);
    $("#auditElectricScheduleStart").val(ret.ElectricScheduleStartTxt);
    $("#auditElectricScheduleEnd").val(ret.ElectricScheduleEndTxt);
    $("#auditMechScheduleStart").val(ret.MechScheduleStartTxt);
    $("#auditMechScheduleEnd").val(ret.MechScheduleEndTxt);
    $("#auditPlumbScheduleStart").val(ret.PlumbScheduleStartTxt);
    $("#auditPlumbScheduleEnd").val(ret.PlumbScheduleEndTxt);
    $("#auditZoneScheduleStart").val(ret.ZoneScheduleStartTxt);
    $("#auditZoneScheduleEnd").val(ret.ZoneScheduleEndTxt);
    $("#auditFireScheduleStart").val(ret.FireScheduleStartTxt);
    $("#auditFireScheduleEnd").val(ret.FireScheduleEndTxt);
    $("#auditFoodScheduleStart").val(ret.FoodScheduleStartTxt);
    $("#auditFoodScheduleEnd").val(ret.FoodScheduleEndTxt);
    $("#auditPoolScheduleStart").val(ret.PoolScheduleStartTxt);
    $("#auditPoolScheduleEnd").val(ret.PoolScheduleEndTxt);
    $("#auditFacilityScheduleStart").val(ret.FacilityScheduleStartTxt);
    $("#auditFacilityScheduleEnd").val(ret.FacilityScheduleEndTxt);
    $("#auditDayCareScheduleStart").val(ret.DayCareScheduleStartTxt);
    $("#auditDayCareScheduleEnd").val(ret.DayCareScheduleEndTxt);
    $("#auditBackFlowScheduleStart").val(ret.BackFlowScheduleStartTxt);
    $("#auditBackFlowScheduleEnd").val(ret.BackFlowScheduleEndTxt);

    $("#auditZoneIsPool").val(ret.ZoneIsPool);

}

function setMeetingAutoScheduledAuditFromResponse(ret) {
    $("#auditAutoScheduleButton").val(true);
    $("#auditBuildingUserID").val(ret.BuildingUserID);
    $("#auditElectricUserID").val(ret.ElectricUserID);
    $("#auditMechUserID").val(ret.MechUserID);
    $("#auditPlumbUserID").val(ret.PlumbUserID);
    $("#auditZoneUserID").val(ret.ZoneUserID);
    $("#auditFireUserID").val(ret.FireUserID);
    $("#auditFoodServiceUserID").val(ret.FoodServiceUserID);
    $("#auditPoolUserID").val(ret.PoolUserID);
    $("#auditFacilityUserID").val(ret.FacilityUserID);
    $("#auditDayCareUserID").val(ret.DayCareUserID);
    $("#auditBackFlowUserID").val(ret.BackFlowUserID);

    $("#auditSelectedStartDateTime").val(ret.ScheduleStartTxt);
    $("#auditSelectedEndDateTime").val(ret.ScheduleEndTxt);
    $("#auditScheduleDate").val(ret.ScheduleStartTxt);

}