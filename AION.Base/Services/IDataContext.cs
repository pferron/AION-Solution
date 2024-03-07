using System.Collections.Generic;
using System.Data;

namespace AION.Base.Services
{
    public interface IDataContext<T> where T : class
    {
        int Create(T entity);
        int Delete(int id);
        T GetById(int id);
        DataSet GetDataSet(int id);
        List<T> GetList(int id);
        int Update(T entity);
    }
}
