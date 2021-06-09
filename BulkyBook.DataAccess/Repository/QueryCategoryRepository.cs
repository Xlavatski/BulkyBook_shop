using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.Queries;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class QueryCategoryRepository : IQueryCategoryRepository
    {
        private IDbConnection dbConn;

        public QueryCategoryRepository(IConfiguration configuration)
        {
            this.dbConn = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public List<GroupByCategoryQuery> GetAllCategoryEarnings()
        {
            var sql = "SELECT Categories.Name AS Name, SUM(OrderDetails.Price * OrderDetails.Count) AS Count " +
                         " FROM Products " +
                         " INNER JOIN OrderDetails ON Products.Id = OrderDetails.ProductId " +
                         " INNER JOIN Categories ON Categories.Id = Products.CategoryId " +
                         " GROUP BY Categories.Name " +
                         " ORDER BY Count DESC ";

            var resultQuery = dbConn.Query<GroupByCategoryQuery>(sql).ToList();

            return (resultQuery);
        }

        public List<GroupByCategoryQuery> SearchGetAllCategoryEarnings(string nameCategory) 
        {
            var SQLnameCatQuery = "SELECT Categories.Name AS Name, SUM(OrderDetails.Price * OrderDetails.Count) AS Count " +
                         " FROM Products " +
                         " INNER JOIN OrderDetails ON Products.Id = OrderDetails.ProductId " +
                         " INNER JOIN Categories ON Categories.Id = Products.CategoryId " +
                         " WHERE Name LIKE CONCAT('%', @Name, '%')" +
                         " GROUP BY Categories.Name " +
                         " ORDER BY Count DESC";

            var resultQuery2 = dbConn.Query<GroupByCategoryQuery>(SQLnameCatQuery, new { Name = nameCategory }).ToList();

            return (resultQuery2);
        }
    }
}
