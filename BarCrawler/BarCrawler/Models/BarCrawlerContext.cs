using BarCrawler.Migrations;

namespace BarCrawler.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class BarCrawlerContext : DbContext, ICustomBarContext
    {
        // Your context has been configured to use a 'BarModels' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'BarCrawler.Models.BarModels' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'BarModels' 
        // connection string in the application configuration file.
        public BarCrawlerContext()
            : base("name=BarCrawlerContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            //Database.
            //Database.SetInitializer(new BarCrawlerContextInitializer(this));
            //Database.SetInitializer(new BarCrawlerContextInitializer());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<BarModel> BarModels { get; set; }
        public virtual DbSet<DrinkModel> DrinkModels { get; set; }
        public virtual DbSet<EventModel> EventModels { get; set; }
        public virtual DbSet<FeedModel> FeedModels { get; set; }
        public virtual DbSet<PictureModel> PictureModels { get; set; }


        public DbContext Create()
        {
            throw new NotImplementedException();
        }


    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}