namespace PrisonerWebAPI.Models
{
    public class Inventories
    {
        public int InventoryId { get; set; }

        public int PrisonerId { get; set; }

        public string ItemName { get; set; }

        public int Quantity { get; set; }
    }
}
