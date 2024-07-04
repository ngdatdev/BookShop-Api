using System.Collections.Generic;

namespace BookShop.Data.Shared.Enum
{
    public class OrderStatusEnum
    {
        public enum OrderStatus
        {
            PendingConfirmation = 0,
            Cancelled = 1,
            Confirmed = 2,
            InTransit = 3,
            Returned = 4,
            Delivered = 5,
        }

        private static readonly Dictionary<OrderStatus, string> statusGuidMap = new Dictionary<
            OrderStatus,
            string
        >
        {
            { OrderStatus.InTransit, "2c677976-ea9d-4195-9f95-2455c3e59bbc" },
            { OrderStatus.Confirmed, "8859e5fe-3f41-4b37-b72e-ad742ec77ab4" },
            { OrderStatus.Cancelled, "c5455c5c-e8aa-466d-96a2-dcb6cbe5db4a" },
            { OrderStatus.Returned, "27d64fac-fbfe-4d90-9b97-d294a0fcb4ba" },
            { OrderStatus.Delivered, "5d37a2cb-f0a4-4d2e-8b59-f3952a8992f1" },
            { OrderStatus.PendingConfirmation, "ee8c3514-43ba-4f82-b212-214f328938c6" }
        };

        public static string Get(OrderStatus status)
        {
            return statusGuidMap[status];
        }
    }
}
