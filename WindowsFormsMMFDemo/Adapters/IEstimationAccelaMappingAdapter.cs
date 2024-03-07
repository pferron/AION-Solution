
using AION.BL;
using AION.BL.Models;
using System.Collections.Generic;
using Meck.Shared.Accela;
using Meck.Shared.Accela.ParserModels;
using Meck.Shared.MeckDataMapping;


namespace DemoInterface.Adapters
{
    public interface IEstimationAccelaMappingAdapter
    {
        /// <summary>
        ///  GetProjectDetails
        /// </summary>
        /// <param name="projInfo"></param>
        /// <returns></returns>
       AccelaProjectModel GetProjectDetailsLoad(ProjectParms projInfo);
        
        /// <summary>
        /// GetProjectDetailsForAIONDisplayInfo
        /// </summary>
        /// <param name="parms"></param>
        /// <param name="mCurrentProjectModel"></param>
        /// <returns></returns>
        AccelaProjectDisplayInfo GetProjectDetailsForAIONDisplayInfo(ProjectParms parms, AccelaProjectModel mCurrentProjectModel);

        /// <summary>
        /// ConvertAccelaToAionMappingAccelaProjectModel
        /// </summary>
        /// <param name="recorddata"></param>
        /// <param name="mAccelaAIONMap"></param>
        /// <returns></returns>
       
        AccelaProjectModel ConvertAccelaToAionMappingAccelaProjectModel(AccelaRecordModel recorddata, List<MeckAccelaDataMap> mAccelaAIONMap);

        /// <summary>
        /// ConvertAccelaToAIONProjectDisplayInfo
        /// </summary>
        /// <param name="RecordId"></param>
        /// <param name="mAccelaRecord"></param>
        /// <param name="mAccelaAIONMap"></param>
        /// <returns></returns>
        AccelaProjectDisplayInfo ConvertAccelaToAIONProjectDisplayInfo(string RecordId, Result mAccelaRecord, List<MeckAccelaDataMap> mAccelaAIONMap);

    }
}
