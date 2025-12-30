namespace Femira.Shared.Dtos
{
    public record PlacedOrderDto(int User_Address_Id,string Address,string AddressName, OrderItemSaveDto[] Items);
}
