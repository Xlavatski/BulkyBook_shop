using BulkyBook.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models.ViewModels
{
    public class QueryCategoryVM
    {
        public string Name { get; set; }
        public double Count { get; set; }

        //public GroupByCategoryQuery groupByCategoryQuery { get; set; }
    }
}
