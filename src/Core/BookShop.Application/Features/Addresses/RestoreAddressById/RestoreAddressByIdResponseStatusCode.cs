namespace BookShop.Application.Features.Addresses.RestoreAddressById;

/// <summary>
///     RestoreAddressById Response Status Code
/// </summary>
public enum RestoreAddressByIdResponseStatusCode
{
    OPERATION_SUCCESS,
    ADDRESS_IS_NOT_TEMPORARILY_REMOVED,
    ADDRESS_ID_IS_NOT_FOUND,
    DATABASE_OPERATION_FAIL,
}
