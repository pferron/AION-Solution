using Meck.Shared;

namespace AION.BL
{
    public class DefaultEstimationHour : ModelBase
    {

        decimal _DefaultHours;
        public decimal DefaultHours
        {
            get { return decimal.Parse(_DefaultHours.ToString("0.###############")); ; }
            set
            {
                if (_DefaultHours != value)
                {
                    IsModelUpdated = true;
                    _DefaultHours = value;
                }
            }
        }

        int _DepartmentID;
        public int DepartmentID
        {
            get { return _DepartmentID; }
            set
            {
                if (_DepartmentID != value)
                {
                    IsModelUpdated = true;
                    _DepartmentID = value;
                }
            }
        }

        int _PropertyTypeID;

        public int PropertyTypeID
        {
            get { return _PropertyTypeID; }
            set
            {
                if (_PropertyTypeID != value)
                {
                    IsModelUpdated = true;
                    _PropertyTypeID = value;
                }
            }
        }

        string _EstimationHoursMode;

        public string EstimationHoursMode
        {
            get { return _EstimationHoursMode; }
            set
            {
                if (_EstimationHoursMode != value)
                {
                    IsModelUpdated = true;
                    LastEstimationHoursMode = (TradeSelectOptionConsts)_EstimationHoursMode;
                    _EstimationHoursMode = value;
                }
            }
        }

        public TradeSelectOptionConsts LastEstimationHoursMode { get; set; }

        bool _IsEnabled;

        public bool IsEnabled
        {
            get { return _IsEnabled; }
            set
            {
                if (_IsEnabled != value)
                {
                    IsModelUpdated = true;
                    _IsEnabled = value;
                }
            }
        }

    }
}
