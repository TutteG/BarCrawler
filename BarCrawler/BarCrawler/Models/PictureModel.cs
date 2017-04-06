﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BarCrawler.Models
{
    public class PictureModel
    {
        [Key]
        public int PictureID { get; set; }
        [ForeignKey("BarModel")]
        public int BarID { get; set; }


        [Timestamp]
        public Byte[] TimeStamp { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
    }
}