#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace SchnaeppchenJaeger.Model
{
    /// <summary>
    /// Represents an item in a shopping list.
    /// </summary>
    public class ShoppingList
    {
        /// <summary>
        /// The name of the advertiser or seller.
        /// </summary>
        public string AdvertiserName { get; set; }

        /// <summary>
        /// The description of the item.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The price of the item.
        /// </summary>
        public float Price { get; set; }

        /// <summary>
        /// The reference price of the item (e.g., original price before discount).
        /// </summary>
        public float ReferencePrice { get; set; }

        /// <summary>
        /// The unit of measurement for the item (e.g., kg, piece, etc.).
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// The starting date for the item's validity or offer period.
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// The ending date for the item's validity or offer period.
        /// </summary>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Indicates whether the item requires loyalty membership for purchase.
        /// </summary>
        public bool RequiresLoyaltyMembership { get; set; }
    }
}