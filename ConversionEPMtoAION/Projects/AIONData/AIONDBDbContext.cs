using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace AIONData
{
    public partial class AIONDBDbContext: DbContext
    {
        public AIONDBDbContext() 
            : base("name=AIONConn")
        {

        }
        public virtual DbSet<PROJECT> PROJECTs { get; set; }
        public virtual DbSet<USER> USERs { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    }
}
