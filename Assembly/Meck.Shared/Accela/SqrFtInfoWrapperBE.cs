using System.Collections.Generic;

namespace Meck.Shared.Accela
{
    class SqrFtInfoWrapperBE
    {
        public List<FloorBE> Floors { get; set; }
        public SqrFtInfoWrapperBE()
        {

        }
    }

    public class FloorBE
    {
        public int Floor { get; set; }
        public int New { get; set; }
        public int Renovation { get; set; }
        public int Existing { get; set; }
    }
}
