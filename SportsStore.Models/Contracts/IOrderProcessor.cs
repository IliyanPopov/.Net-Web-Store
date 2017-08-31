namespace SportsStore.Models.Contracts
{
    using Entities;

    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}