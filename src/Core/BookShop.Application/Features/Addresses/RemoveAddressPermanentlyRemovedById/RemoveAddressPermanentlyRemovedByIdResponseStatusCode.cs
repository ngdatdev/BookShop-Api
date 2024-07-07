namespace BookShop.Application.Features.Addresses.RemoveAddressPermanentlyRemovedById;

/// <summary>
///     RemoveAddressPermanentlyRemovedById Response Status Code
/// </summary>
public enum RemoveAddressPermanentlyRemovedByIdResponseStatusCode
{
    OPERATION_SUCCESS,
    ADDRESS_IS_NOT_TEMPORARILY_REMOVED,
    ADDRESS_ID_IS_NOT_FOUND,
    DATABASE_OPERATION_FAIL,
}
