#pragma warning disable CS8618
using SQLite;

namespace SchnaeppchenJaeger.Model
{
    public class Models
    {
        [PrimaryKey, AutoIncrement]
        public ulong ID { get; set; }
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