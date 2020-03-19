using System;
using System.Collections.Generic;

namespace TodoApi.DTO
{
    public class Filter
    {
        public List<FieldCondition> Fields { get; set; }

        public List<long> RegionIds { get; set; }

        public List<long> SiteIds { get; set; }

        public List<long> BuildingIds { get; set; }

        public List<long> FloorIds { get; set; }

    }
}