using TinyHelp.Domain.Entities.Orders;

namespace TinyHelp.Components.CsvConverter.DataTransferObjects;

public record OrderDetailDto(
    string OrderNumber,
    Buyer Buyer,
    DateTime OrderDate,
    string TransactionId,
    Shippment Shippment,
    OrderLineItem OrderLineItem);