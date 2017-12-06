using Domain.CoinRate;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(VCoinWeb.Startup))]
namespace VCoinWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var dbContextFactory = new VCoinDbContextFactory();
            dbContextFactory.Create();

            //VCoinDbContext dbContext = new VCoinDbContext("VCoinDbContext");
            //dbContext.Links.Add(new Link()
            //{
            //    Name = "Link 1",
            //    Note = "note 1",
            //    CreatedBy = "",
            //    CreatedDate = DateTime.Now,
            //    UpdatedBy = "",
            //    UpdatedDate = DateTime.Now,
            //    IsActived = true,
            //    IsDeleted = false
            //});

            //var re = dbContext.SaveChanges();

        }
    }
}
