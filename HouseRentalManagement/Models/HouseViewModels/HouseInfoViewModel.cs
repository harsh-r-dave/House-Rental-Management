using System.Collections.Generic;

namespace HouseRentalManagement.Models.HouseViewModels
{
    public class HouseInfoViewModel
    {
        public string FullAddress { get; set; }
        public string Rent { get; set; }
        public string Description { get; set; }
        public string DateAvailable { get; set; }
        public string Occupancy { get; set; }
        public string ParkingSpace { get; set; }
        public string Washrooms { get; set; }

        public string MainImageSrc { get; set; }
        public string MapImageSrc { get; set; }

        public ICollection<AmenityViewModel> AllAmenities { get; set; } = new HashSet<AmenityViewModel>();
        public ICollection<AmenityViewModel> IncludedAmenities { get; set; } = new HashSet<AmenityViewModel>();
        public ICollection<ImagesViewModel> Images { get; set; } = new HashSet<ImagesViewModel>();
        public ICollection<string> Restrictions { get; set; } = new HashSet<string>();
        public ICollection<string> Facilities { get; set; } = new HashSet<string>();
        public ICollection<GettingAroundViewModel> GettingArounds { get; set; } = new HashSet<GettingAroundViewModel>();
    }
}
