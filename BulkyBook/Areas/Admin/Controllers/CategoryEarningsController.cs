using AutoMapper;
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.Queries;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class CategoryEarningsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IQueryCategoryRepository _queryCatRepo;
        public CategoryEarningsController(IMapper mapper ,IQueryCategoryRepository queryCatRepo)
        {
            _mapper = mapper;
            _queryCatRepo = queryCatRepo;
        }

        //Method for mapping

        //private List<QueryCategoryVM> MapList(List<GroupByCategoryQuery> categorys)
        //{
        //    var viewModles = new List<QueryCategoryVM>();

        //    foreach (var category in categorys)
        //    {

        //        var temp = new QueryCategoryVM();

        //        temp.Name = category.Name;

        //        temp.Count = category.Count;

        //        viewModles.Add(temp);
        //    }
        //    return viewModles;
        //}

        public IActionResult Index()
        {

            var repo = _queryCatRepo.GetAllCategoryEarnings();

            //var vm = MapList(repo);

            var vm = _mapper.Map<List<QueryCategoryVM>>(repo);

            return View(vm);
        }

        //Search

        public IActionResult Search(string nameCat) 
        {
            ViewData["GetCategorycDetails"] = nameCat;

            var repoSearch = _queryCatRepo.GetAllCategoryEarnings();

            if (!string.IsNullOrEmpty(nameCat))
            {
                repoSearch = _queryCatRepo.SearchGetAllCategoryEarnings(nameCat);

                var vm2 = _mapper.Map<List<QueryCategoryVM>>(repoSearch);

                return View("Index", vm2);
            }

            var vm = _mapper.Map<List<QueryCategoryVM>>(repoSearch);

            return View("Index", vm);

        }

        //*****************************************************************


        //private IDbConnection dbConnect;
        //public CategoryEarningsController(IUnitOfWork unitOfWork, IConfiguration configuration)
        //{
        //    this.dbConnect = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        //}

        ////Method for mapping

        //private List<QueryCategoryVM> MapList(List<GroupByCategoryQuery> categorys)
        //{
        //    var viewModles = new List<QueryCategoryVM>();

        //    foreach (var category in categorys)
        //    {

        //        var temp = new QueryCategoryVM();

        //        temp.Name = category.Name;

        //        temp.Count = category.Count;

        //        viewModles.Add(temp);
        //    }
        //    return viewModles;
        //}

        //public IActionResult Index()
        //{

        //    var sql = "SELECT Categories.Name AS Name, SUM(OrderDetails.Price * OrderDetails.Count) AS Count " + 
        //                 " FROM Products " +
        //                 " INNER JOIN OrderDetails ON Products.Id = OrderDetails.ProductId " +
        //                 " INNER JOIN Categories ON Categories.Id = Products.CategoryId " +
        //                 " GROUP BY Categories.Name " +
        //                 " ORDER BY Count DESC ";

        //    var resultQuery = dbConnect.Query<GroupByCategoryQuery>(sql).ToList();

        //    var vm = MapList(resultQuery);

        //    return View(vm);
        //}

        ////Search

        //public IActionResult Search(string nameCat) 
        //{
        //    ViewData["GetCategorycDetails"] = nameCat;

        //    var nameCatQuery = "SELECT Categories.Name AS Name, SUM(OrderDetails.Price * OrderDetails.Count) AS Count " +
        //                 " FROM Products " +
        //                 " INNER JOIN OrderDetails ON Products.Id = OrderDetails.ProductId " +
        //                 " INNER JOIN Categories ON Categories.Id = Products.CategoryId " +
        //                 " GROUP BY Categories.Name " +
        //                 " ORDER BY Count DESC ";

        //    var resultQuery = dbConnect.Query<GroupByCategoryQuery>(nameCatQuery).ToList();

        //    if (!string.IsNullOrEmpty(nameCat))
        //    {
        //        nameCatQuery = "SELECT Categories.Name AS Name, SUM(OrderDetails.Price * OrderDetails.Count) AS Count " +
        //                 " FROM Products " +
        //                 " INNER JOIN OrderDetails ON Products.Id = OrderDetails.ProductId " +
        //                 " INNER JOIN Categories ON Categories.Id = Products.CategoryId " +
        //                 " WHERE Name LIKE CONCAT('%', @Name, '%')" +
        //                 " GROUP BY Categories.Name " +
        //                 " ORDER BY Count DESC";

        //        var resultQuery2 = dbConnect.Query<GroupByCategoryQuery>(nameCatQuery, new { Name =  nameCat }).ToList();

        //        var vm2 = MapList(resultQuery2);

        //        return View("Index", vm2);
        //    }

        //    var vm = MapList(resultQuery);

        //    return View("Index", vm);

        //}
    }
}
