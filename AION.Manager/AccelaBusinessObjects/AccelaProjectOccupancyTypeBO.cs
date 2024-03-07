using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;

namespace AION.Manager.AccelaBusinessObjects
{
    public class AccelaProjectOccupancyTypeBO
    {
        public string GetProjectOccupancyTypeNameFromAccelaOccupancyType(string accelaOccupancyType)
        {
            string[] accelaOccupancyCode = accelaOccupancyType.Split('*');
            string shortCode = accelaOccupancyCode[0].Trim();

            OccupancyTypeRefBE be = new OccupancyTypeRefBO().GetByProjectOccupancyTyp(shortCode);

            return be.OccupancyTypName;
        }
    }
}