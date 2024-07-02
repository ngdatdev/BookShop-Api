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
            { OrderStatus.InTransit, "017f8cb6-46c3-448b-9132-81ca2edc9d4e" },
            { OrderStatus.Confirmed, "206f3705-dd63-4ca2-9fbd-cadb786e5e57" },
            { OrderStatus.Cancelled, "3de6bc7b-cc8b-47f6-81eb-3d651c701e61" },
            { OrderStatus.Returned, "710f0649-4fb2-4b7d-b4de-3647b69a5604" },
            { OrderStatus.Delivered, "8585180d-99ae-4839-8780-555e83a2c097" },
            { OrderStatus.PendingConfirmation, "f6c70fa3-042a-4dad-88cd-037c2c248a1b" }
        };

        public static string Get(OrderStatus status)
        {
            return statusGuidMap[status];
        }
    }
}
