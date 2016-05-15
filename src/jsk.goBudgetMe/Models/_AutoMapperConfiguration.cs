using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using jsk.goBudgetMe.Models;
using jsk.goBudgetMe.Services;

using AutoMapper;

namespace jsk.goBudgetMe
{
    public class AutoMapperConfiguration : Profile
    {
        protected override void Configure()
        {
            CreateMap<Transaction, TransactionDto>();
            CreateMap<TransactionDto, Transaction>();

            //CreateMap<Task<IEnumerable<Transaction>>, Task<IEnumerable<TransactionDto>>>();
            //CreateMap<Task<IEnumerable<TransactionDto>>, Task<IEnumerable<Transaction>>>();

            // ADD ADDITIONAL MAPPINGS HERE
        }
    }
}
