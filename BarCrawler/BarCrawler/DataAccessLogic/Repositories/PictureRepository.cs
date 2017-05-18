﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarCrawler.DataAccessLogic.Repositories;
using BarCrawler.DataAccessLogic.Repositories.Interface;
using BarCrawler.Models;
using BarCrawler.ViewModels;

namespace DataAccessLogic.Repositories
{
    public class PictureRepository : GenericRepository<PictureModel>, IPictureRepository
    {
        public PictureRepository(BarCrawlerContext ReceivedContext) : base(ReceivedContext)
        {
            
        }


        public void AddModelForUpdate(ref PictureViewModel viewModel, ref PictureModel pictureModel)
        {
            pictureModel.Directory = viewModel.Directory;
            pictureModel.Description = viewModel.Description;
            MarkAsDirty(pictureModel);
        }
    }
}
