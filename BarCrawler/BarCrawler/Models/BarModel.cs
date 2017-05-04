﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BarCrawler.Models
{
    public class BarModel
    {
        [Key]
        public int BarID { get; set; }

        [Timestamp]
        public Byte[] TimeStamp { get; set; }

        //Bar info
        [Required]
        [MaxLength(50)]
        public string BarName { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Faculty { get; set; }
        [Display(Name = "Telefon Nummer")]
        [MaxLength(8)]
        public string PhoneNumber { get; set; }


        //Bar Address
        [Required]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Required]
        public string StreetNumber { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [MaxLength(5)]
        public string Zipcode { get; set; }


        public double Longitude { get; set; }
        public double Latitude { get; set; }



        //Foreign keys
        public List<DrinkModel> Drinks { get; set; }        //Overvej at bruge et Dictionary til at gemme i. Burg evt. navn på drink som key
        public List<EventModel> Events { get; set; }        //Overvej at bruge et Dictionary til at gemme i. Brug evt. dato + tid for hvornår event finder sted, for som key til at gemme i dictionary 
        public List<FeedModel> Feeds { get; set; }          //Overvej at bruge et Dictionary til at gemme i. Burg evt. tidspunkt for oprettelse som key
        public List<PictureModel> Pictures { get; set; }    
        
    }

}