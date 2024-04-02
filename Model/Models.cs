#pragma warning disable CS8618
using SQLite;

namespace SchnaeppchenJaeger.Model
{
    // product model?
    public class Models
    {
        public string AdvertiserName { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public float ReferencePrice { get; set; }
        public string Unit { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool RequiresLoyaltyMembership { get; set; }
    }
}