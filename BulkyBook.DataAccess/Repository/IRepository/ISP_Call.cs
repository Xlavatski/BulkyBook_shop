using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface ISP_Call : IDisposable
    {
        T Single<T>(string procedureName, DynamicParameters parm = null);

        void Execute(string procedureName, DynamicParameters parm = null);

        T OneRecord<T>(string procedureName, DynamicParameters parm = null);

        IEnumerable<T> List<T>(string procedureName, DynamicParameters parm = null);

        Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string procedureName, DynamicParameters parm = null);
        Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> List<T1, T2, T3>(string procedureName, DynamicParameters parm = null);
    }
}
