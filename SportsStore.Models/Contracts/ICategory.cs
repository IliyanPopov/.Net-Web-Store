namespace SportsStore.Models.Contracts
{
    public interface ICategory
    {
        int CategoryId { get; set; }

        string Name { get; set; }
    }
}