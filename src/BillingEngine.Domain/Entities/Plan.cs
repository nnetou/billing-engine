namespace BillingEngine.Domain.Entities
{
    public class Plan
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public int IncludedRequests { get; private set; }
        public decimal PricePerUser { get; private set; }

        protected Plan() { }

        public Plan(Guid id, String name, int includedRequests, decimal pricePerUser)
        {
            Id = id;
            Name = name.Trim();
            IncludedRequests = includedRequests;
            PricePerUser = pricePerUser;
        }

        public void UpdateValues(int newRequests, decimal newPrice)
        {
            IncludedRequests = newRequests;
            PricePerUser = newPrice;
        }

    }
}