namespace BookShop.Application.Features.Addresses.UpdateAddressById;

/// <summary>
///     UpdateAddressById Response Status Code
/// </summary>
public enum UpdateAddressByIdResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    ADDRESS_IS_NOT_FOUND,
    ADDRESS_IS_TEMPORARILY_REMOVED
}
