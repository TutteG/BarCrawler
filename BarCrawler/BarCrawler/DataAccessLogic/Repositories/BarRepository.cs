﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BarCrawler.DataAccessLogic.Repositories;
using BarCrawler.Models;
using BarCrawler.ViewModels;

namespace DataAccessLogic.Repositories
{
    public class BarRepository : GenericRepository<BarModel>, IBarRepository
    {
        private DbSet<BarModel> _dbSet;

        public BarRepository(BarCrawlerContext receivedContext) : base(receivedContext)
        {
            //_context = receivedContext;
            _dbSet = receivedContext.BarModels;
        }

        public BarModel GetProfile(int? id)
        {
            return (_dbSet.Include(d => d.Drinks)
                .Include(e => e.Events)
                .Include(f => f.Feeds)
                .Include(p => p.Pictures)
                .SingleOrDefault(x => x.BarID == id));
        }

        public BarModel GetEditInfo(int? id)
        {
            return _context.Set<BarModel>().Include(p => p.ProfilPictureModel).SingleOrDefault(x => x.BarID == id);
        }

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

        public BarModel GetByUserID(string userId)
        {
            return _context.Set<BarModel>().SingleOrDefault(x => x.userID == userId);
        }
    }
}
