namespace TinyHelp.Domain.Entities.Orders;

public record OrderLineItem
{
    public string ProductName { get; init; }
    public string? ProductOptionName { get; init; }
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal Total { get; init; }

    public OrderLineItem(
        string productName, 
        string? productOptionName,
        int quantity,
        decimal unitPrice,
        decimal total)
    {
        ProductName = productName;
        ProductOptionName = productOptionName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Total = total;
    }
}