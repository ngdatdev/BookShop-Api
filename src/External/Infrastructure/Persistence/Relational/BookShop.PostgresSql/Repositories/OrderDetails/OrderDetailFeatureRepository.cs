using BookShop.Data.Features.Repositories.OrderDetails;
using BookShop.Data.Features.Repositories.OrderDetails.GetAllOrderDetailsByUserId;
using BookShop.Data.Features.Repositories.OrderDetails.GetAllTemporarilyRemovedOrderDetails;
using BookShop.Data.Features.Repositories.OrderDetails.GetOrderDetailById;
using BookShop.Data.Features.Repositories.OrderDetails.GetOrderDetailsByOrderStatusId;
using BookShop.Data.Features.Repositories.OrderDetails.RemoveOrderDetailPermanentlyById;
using BookShop.Data.Features.Repositories.OrderDetails.RemoveOrderDetailTemporarilyById;
using BookShop.Data.Features.Repositories.OrderDetails.RestoreOrderDetailById;
using BookShop.Data.Features.Repositories.OrderDetails.SwitchOrderStatusToNext;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.OrderDetails.GetAllOrderDetailsByUserId;
using BookShop.PostgresSql.Repositories.OrderDetails.GetAllTemporarilyRemovedOrderDetails;
using BookShop.PostgresSql.Repositories.OrderDetails.GetOrderDetailById;
using BookShop.PostgresSql.Repositories.OrderDetails.GetOrderDetailsByOrderStatusId;
using BookShop.PostgresSql.Repositories.OrderDetails.RemoveOrderDetailPermanentlyById;
using BookShop.PostgresSql.Repositories.OrderDetails.RemoveOrderDetailTemporarilyById;
using BookShop.PostgresSql.Repositories.OrderDetails.RestoreOrderDetailById;
using BookShop.PostgresSql.Repositories.OrderDetails.SwitchOrderStatusToNext;

namespace BookShop.PostgresSql.Repositories.OrderDetails;

/// <summary>
///    Implement of CartFeatureRepository interface.
/// </summary>
internal class OrderDetailFeatureRepository : IOrderDetailFeatureRepository
{
    private readonly BookShopContext _context;
    private IGetOrderDetailByIdRepository _getOrderDetailByIdRepository;
    private IGetOrderDetailsByOrderStatusIdRepository _getOrderDetailsByOrderStatusIdRepository;
    private IGetAllOrderDetailsByUserIdRepository _getAllOrderDetailsByUserIdRepository;
    private IGetAllTemporarilyRemovedOrderDetailsRepository _getAllTemporarilyRemovedOrderDetailsRepository;
    private IRemoveOrderDetailTemporarilyByIdRepository _removeOrderDetailTemporarilyByIdRepository;
    private IRemoveOrderDetailPermanentlyByIdRepository _removeOrderDetailPermanentlyByIdRepository;
    private IRestoreOrderDetailByIdRepository _restoreOrderDetailByIdRepository;
    private ISwitchOrderStatusToNextRepository _switchOrderStatusToNextRepository;

    internal OrderDetailFeatureRepository(BookShopContext context)
    {
        _context = context;
    }

    public IGetOrderDetailByIdRepository GetOrderDetailByIdRepository
    {
        get
        {
            return _getOrderDetailByIdRepository ??= new GetOrderDetailByIdRepository(
                context: _context
            );
        }
    }

    public IGetOrderDetailsByOrderStatusIdRepository GetOrderDetailsByOrderStatusIdRepository
    {
        get
        {
            return _getOrderDetailsByOrderStatusIdRepository ??=
                new GetOrderDetailsByOrderStatusIdRepository(context: _context);
        }
    }

    public IGetAllOrderDetailsByUserIdRepository GetAllOrderDetailsByUserIdRepository
    {
        get
        {
            return _getAllOrderDetailsByUserIdRepository ??=
                new GetAllOrderDetailsByUserIdRepository(context: _context);
        }
    }

    public IGetAllTemporarilyRemovedOrderDetailsRepository GetAllTemporarilyRemovedOrderDetailsRepository
    {
        get
        {
            return _getAllTemporarilyRemovedOrderDetailsRepository ??=
                new GetAllTemporarilyRemovedOrderDetailsRepository(context: _context);
        }
    }

    public IRemoveOrderDetailTemporarilyByIdRepository RemoveOrderDetailTemporarilyByIdRepository
    {
        get
        {
            return _removeOrderDetailTemporarilyByIdRepository ??=
                new RemoveOrderDetailTemporarilyByIdRepository(context: _context);
        }
    }

    public IRemoveOrderDetailPermanentlyByIdRepository RemoveOrderDetailPermanentlyByIdRepository
    {
        get
        {
            return _removeOrderDetailPermanentlyByIdRepository ??=
                new RemoveOrderDetailPermanentlyByIdRepository(context: _context);
        }
    }

    public IRestoreOrderDetailByIdRepository RestoreOrderDetailByIdRepository
    {
        get
        {
            return _restoreOrderDetailByIdRepository ??= new RestoreOrderDetailByIdRepository(
                context: _context
            );
        }
    }

    public ISwitchOrderStatusToNextRepository SwitchOrderStatusToNextByIdRepository
    {
        get
        {
            return _switchOrderStatusToNextRepository ??= new SwitchOrderStatusToNextRepository(
                context: _context
            );
        }
    }
}
