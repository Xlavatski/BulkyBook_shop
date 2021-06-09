using AutoMapper;
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.Queries;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class ProductEarningsController : Controller
    {
        //private readonly ApplicationDbContext _db;

        //public ProductEarningsController(ApplicationDbContext db)
        //{
        //    _db = db;
        //}

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductEarningsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        //public IActionResult Index()
        //{

        //    var result = _db.OrderDetails.Include(m => m.Product).ToList().GroupBy(i => i.Product).Select(i => new GroupByModelQuery
        //    {
        //        //Earnings = i.Sum(item => item.Count) * i.Sum(item => item.Price), 
        //        Earnings = i.Sum(item => item.Total),
        //        Product = i.Key
        //    });

        //    return View(result);
        //}


        //Method for mapping

        private List<QueryProductVM> MapList(List<GroupByModelQuery> products) 
        {
            var viewModels = new List<QueryProductVM>();

            foreach (var product in products) 
            {
                var temp = new QueryProductVM();

                temp.Title = product.Product.Title;

                temp.Earnings = product.Earnings;

                viewModels.Add(temp);
            }
            return viewModels;
        }

        public IActionResult Index()
        {
            //upit vraca kolika je zarada po kojem proizvodu

            var result = _unitOfWork.OrderDetails.GetAll(includePropertie: "Product").GroupBy(i => i.Product).Select(i => new GroupByModelQuery
            {
                Earnings = i.Sum(item => item.Total),
                Product = i.Key
            }).OrderByDescending(i => i.Earnings).ToList();

            //var vm = MapList(result);


            var vm = _mapper.Map<List<QueryProductVM>>(result);

            //upit vraca koja je prosjecna kolicina narucenog proizvoda

            //var result = _unitOfWork.OrderDetails.GetAll(includePropertie:"Product").GroupBy(i => i.Product).Select(i => new GroupByModelQuery
            //{ 
            //    Earnings = i.Average(item => item.Count),
            //    Product = i.Key
            //});


            //upit vraca kolika je zarada za proizvode koje imaju cijenu vecu od 500 i da im zarada prelazi 3500

            //var result = _unitOfWork.OrderDetails.GetAll(includePropertie:"Product").ToList().Where(c => c.Price > 500).GroupBy(i => i.Product).Select(i => new GroupByModelQuery
            //{
            //    //Earnings = i.Sum(item => item.Count) * i.Sum(item => item.Price), 
            //    Earnings = i.Sum(item => item.Total),
            //    Product = i.Key
            //}).Where(c => c.Earnings > 3500);


            //upit vraca kolicinu prodanig knjiga po narudzbi

            //var result = _unitOfWork.OrderDetails.GetAll(includePropertie: "Product").GroupBy(i => i.Count switch
            // {
            //     <= 10 => "0..10",
            //     <= 20 => "11..20",
            //     <= 50 => "21..50",
            //     _ => "51..200"
            // }).Select(i => new GroupByModelQuery
            // {
            //     Earnings = i.Count(),
            //     Quantity = i.Key
            // });


            return View(vm);
        }

        //Search

        public IActionResult Search(string nameProd) 
        {
            ViewData["GetProductDetails"] = nameProd;

            var repoSearch = _unitOfWork.OrderDetails.GetAll(includePropertie: "Product").GroupBy(i => i.Product).Select(i => new GroupByModelQuery
            {
                Earnings = i.Sum(item => item.Total),
                Product = i.Key
            }).OrderByDescending(i => i.Earnings).ToList();

            if (!string.IsNullOrEmpty(nameProd)) 
            {
                repoSearch = _unitOfWork.OrderDetails.GetAll(includePropertie: "Product").Where(x => x.Product.Title.ToLower().Contains(nameProd.ToLower()))
                    .GroupBy(i => i.Product).Select(i => new GroupByModelQuery
                        {
                            Earnings = i.Sum(item => item.Total),
                            Product = i.Key
                        }).OrderByDescending(i => i.Earnings).ToList();

                var vm2 = _mapper.Map<List<QueryProductVM>>(repoSearch);

                return View("Index", vm2);
            }

            var vm = _mapper.Map<List<QueryProductVM>>(repoSearch);

            return View("Index", vm);
        }
    }
}
