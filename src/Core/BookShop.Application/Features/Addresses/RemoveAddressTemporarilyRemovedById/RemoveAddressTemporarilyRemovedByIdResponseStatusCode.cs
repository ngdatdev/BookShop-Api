namespace BookShop.Application.Features.Addresses.RemoveAddressTemporarilyRemovedById;

/// <summary>
///     RemoveAddressTemporarilyRemovedById Response Status Code
/// </summary>
public enum RemoveAddressTemporarilyRemovedByIdResponseStatusCode
{
    OPERATION_SUCCESS,
    ADDRESS_IS_ALREADY_TEMPORARILY_REMOVED,
    ADDRESS_ID_IS_NOT_FOUND,
    DATABASE_OPERATION_FAIL,
}
