namespace SportsStore.Models.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class ShippingDetails
    {
        [Required(ErrorMessage = "Please enter a Name")]
        [MaxLength(35)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the first address line")]
        [MaxLength(80)]
        public string Line1 { get; set; }

        [MaxLength(80)]
        public string Line2 { get; set; }

        [MaxLength(80)]
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Please enter a city name")]
        [MaxLength(35)]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter a state name")]
        [MaxLength(35)]
        public string State { get; set; }

        [MaxLength(35)]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Please enter a country name")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}