namespace HouseRentingSystem.Models.House
{
    public class HouseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
        public bool CurentUserIsOwner { get; internal set; }
    }
}