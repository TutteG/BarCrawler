﻿using System.Data.Entity;
using System.Linq;
using BarCrawler.DataAccessLogic.Repositories.Interface;
using BarCrawler.Models;
using BarCrawler.ViewModels;


namespace BarCrawler.DataAccessLogic.Repositories
{
    /// <summary>
    /// The BarRepository is making sure to store, edit and get the correct bars from the tables. 
    /// </summary>
    public class BarRepository : GenericRepository<BarModel>, IBarRepository
    {

        /// <summary>
        /// The table for all the bars.
        /// </summary>
        private DbSet<BarModel> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="BarRepository"/> class.
        /// </summary>
        public BarRepository(BarCrawlerContext receivedContext) : base(receivedContext)
        {
            //_context = receivedContext;
            _dbSet = receivedContext.BarModels;
        }


        /// <summary>
        /// Gets the specific bar profile through a BarID.
        /// </summary>
        public BarModel GetProfile(int? id)
        {
            return (_dbSet.Include(d => d.Drinks)
                .Include(e => e.Events)
                .Include(f => f.Feeds)
                .Include(p => p.Pictures)
                .SingleOrDefault(x => x.BarID == id));
        }

        /// <summary>
        /// Gets the specific bar profile through a BarID when the bar needs to be edit.
        /// </summary>
        public BarModel GetEditInfo(int? id)
        {
            return _context.Set<BarModel>().Include(p => p.BarProfilPictureModel).SingleOrDefault(x => x.BarID == id);
        }

        /// <summary>
        /// Change the bar profiles properties through the <see cref="EditViewModel"/>
        /// </summary>
        public void EditInfo(EditViewModel editviewmodel, BarModel bar)
        {
            bar.Address1 = editviewmodel.Address1;
            bar.Address2 = editviewmodel.Address2;
            bar.PhoneNumber = editviewmodel.PhoneNumber;
            bar.Zipcode = editviewmodel.Zipcode;
            bar.Description = editviewmodel.Description;
            bar.StreetNumber = editviewmodel.StreetNumber;
            bar.City = editviewmodel.City;
            bar.Faculty = editviewmodel.Faculty;
            MarkAsDirty(bar);
        }

        /// <summary>
        /// Change the bar profiles properties through the <see cref="EditViewModel"/>
        /// </summary>
        public BarModel GetByUserID(string userId)
        {
            return _context.Set<BarModel>().SingleOrDefault(x => x.userID == userId);
        }

        /// <summary>
        /// Create the bar thorugh <see cref="BarModel"/> and properties through the <see cref="BigRegisterViewModel"/> by a user <see cref="ApplicationUser"/>
        /// </summary>
        public void CreateAndAddBar(ref BarModel bar, ref BigRegisterViewModel model, ref ApplicationUser user)
        {
            bar.Address1 = model.BarModel.Address1;
            bar.Address2 = model.BarModel.Address2;
            bar.PhoneNumber = model.BarModel.PhoneNumber;
            bar.Zipcode = model.BarModel.Zipcode;
            bar.BarName = model.BarModel.BarName;
            bar.Description = model.BarModel.Description;
            bar.StreetNumber = model.BarModel.StreetNumber;
            bar.City = model.BarModel.City;
            bar.userID = user.Id;
            bar.Email = model.BarModel.Email;
            bar.Faculty = model.BarModel.Faculty;
            bar.OpenTime = model.BarModel.OpenTime;
            bar.CloseTime = model.BarModel.CloseTime;
            Add(bar);
        }
    }
}
