using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AION.BL
{
    public abstract class ModelBase
    {

        public ModelBase()
        {
            //CreatedUser = new UserIdentity() { ID = 1 };
            //UpdatedUser = new UserIdentity() { ID = 1 };
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
            IsModelUpdated = false;
        }

        public int ID { get; set; }

        public UserIdentity CreatedUser { get; set; }

        public UserIdentity UpdatedUser { get; set; }

        public DateTime CreatedDate { get; set; }
     
        public DateTime UpdatedDate { get; set; }

        public bool IsModelUpdated { get; set; }
    }
}
