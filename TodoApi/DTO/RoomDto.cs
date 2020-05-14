using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.DTO
{
    public class RoomDto : Room
    {
        

        public string FloorName { get; set; }

        public string ResourceTypeName { get; set; } = "room";

        public string ImageUri
        {
            get
            {
                if (base.Images?.Count != 0)
                {
                    return base.Images?.FirstOrDefault(image => image.IsThumbnail == false)?.PathWithSite;
                }

                return "";
            }

        }
    }
}
