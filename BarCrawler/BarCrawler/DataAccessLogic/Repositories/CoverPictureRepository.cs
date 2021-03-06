﻿using BarCrawler.DataAccessLogic.Repositories.Interface;
using BarCrawler.Models;
using BarCrawler.ViewModels;

namespace BarCrawler.DataAccessLogic.Repositories
{
    /// <summary>
    /// The CoverPictureRepository contains functions to store, edit and get the correct cover pictures from the database.
    /// </summary>
    /// <seealso cref="CoverPictureModel" />
    /// <seealso cref="ICoverPictureRepository" />
    public class CoverPictureRepository : GenericRepository<CoverPictureModel>, ICoverPictureRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoverPictureRepository" /> class.
        /// </summary>
        /// <param name="ReceivedContext">The received context.</param>
        /// <seealso cref="BarCrawlerContext"/>
        public CoverPictureRepository(BarCrawlerContext ReceivedContext) : base(ReceivedContext)
        {
        }

        /// <summary>
        /// Adds the coverpictureModel for update. <br />
        /// And after marks the coverpictureModel as dirty to indicate a change in the coverpictureModel.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="pictureModel">The <see cref="CoverPictureModel"/>.</param>
        /// <param name="imageDir">The image directory</param>
        /// <seealso cref="CoverPictureModel" />
        public void AddModelForUpdate(ref PictureViewModel viewModel, ref CoverPictureModel pictureModel, string imageDir)
        {
            pictureModel.Directory = imageDir;
            pictureModel.Description = viewModel.Description;
            MarkAsDirty(pictureModel);
        }
    }
}