namespace SiteJu.Models
{
    public class PrestationOptionViewModel : PrestationViewModel
    {
        public bool IsSelected { get; set; }
        public int Quantity { get; set; }
        public int MaxQuantity { get; set; }
    }
}
