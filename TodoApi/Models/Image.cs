using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace TodoApi.Models
{
    public class Image
    {

        //todo: hacky, refactor
       

        public Image()
        {

        }
        

        public long Id { get; set; }
        public Room Room { get; set; }

        public long RoomId { get; set; }

        public string Path { get; set; }

        [NotMapped]
        public string PathWithSite
        {
            get
            {
                return $"{Environment.GetEnvironmentVariable("ASPNETCORE_PATH")}uploads/{Path}";
            }
        }

        public string Name { get; set; }

        public bool IsThumbnail { get; set; } = false;

        public string PathToFullImage { get; set; }

        [NotMapped]
        public string PathToFullImageWithSite
        {
            get
            {
                if (!IsThumbnail) return "";
                else return $"{Environment.GetEnvironmentVariable("ASPNETCORE_PATH")}uploads/{PathToFullImage}";
            }
        }


    }
}
