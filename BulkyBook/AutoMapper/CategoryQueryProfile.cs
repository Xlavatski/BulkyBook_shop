using AutoMapper;
using BulkyBook.Models.Queries;
using BulkyBook.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.AutoMapper
{
    public class CategoryQueryProfile : Profile
    {
        public CategoryQueryProfile()
        {
            CreateMap<GroupByCategoryQuery, QueryCategoryVM>();
            //kreiranje novog mapiranja ??

            CreateMap<GroupByModelQuery, QueryProductVM>()
                .ForMember(dest =>
                dest.Title,
                opt => opt.MapFrom(src => src.Product.Title))
                .ForMember(dest =>
                dest.Earnings,
                opt => opt.MapFrom(src => src.Earnings));

        }
    }
}
