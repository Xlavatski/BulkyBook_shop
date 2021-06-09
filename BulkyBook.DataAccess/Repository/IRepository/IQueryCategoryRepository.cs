using BulkyBook.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IQueryCategoryRepository
    {
        List<GroupByCategoryQuery> GetAllCategoryEarnings();
        List<GroupByCategoryQuery> SearchGetAllCategoryEarnings(string nameCategory); 
    }
}
