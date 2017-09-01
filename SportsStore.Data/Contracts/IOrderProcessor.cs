namespace SportsStore.Data.Contracts
{
    using Models.Entities;

    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}