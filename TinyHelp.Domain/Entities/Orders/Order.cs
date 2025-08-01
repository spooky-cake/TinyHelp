namespace TinyHelp.Domain.Entities.Orders;

public class Order
{
    public string OrderNumber { get; init; }
    public Buyer Buyer { get; init; }
    public DateTime OrderDate { get; init; }
    public string? Status { get; private set; }
    public string TransactionId { get; init; }
    public Shippment Shipment { get; init; }
    public string CurrencyCode { get; private set; }
    
    public IEnumerable<OrderLineItem> OrderLineItems { get; init; }
    
    public string? PaymentStatus { get; private set; }
    public int ItemCount { get; init; }
    public decimal ItemTotal { get; init; }
    public decimal TotalPrice { get; init; }
    public decimal TotalShipping { get; init; }
    public decimal TotalTax { get; init; }
    public decimal TaxRemitted { get; init; }
    public decimal TotalDiscount { get; init; }
    public string? DiscountCode { get; init; }
    public string Source { get; init; }
    public string? Note { get; init; }
    public string? PrivateNote { get; init; }

    private Order(string orderNumber, Buyer buyer, DateTime orderDate,
        string? status, string transactionId, Shippment shippment, string currencyCode,
        IEnumerable<OrderLineItem> orderLineItems, string? paymentStatus, int itemCount,
        decimal itemTotal, decimal totalPrice, decimal totalShipping, decimal totalTax,
        decimal taxRemitted, decimal totalDiscount, string? discountCode,
        string source, string? note, string? privateNote)
    {
        OrderNumber = orderNumber;
        Buyer = buyer;
        OrderDate = orderDate;
        Status = status;
        TransactionId = transactionId;
        Shipment = shippment;
        CurrencyCode = currencyCode;
        OrderLineItems = orderLineItems;
        PaymentStatus = paymentStatus;
        ItemCount = itemCount;
        ItemTotal = itemTotal;
        TotalPrice = totalPrice;
        TotalShipping = totalShipping;
        TotalTax = totalTax;
        TaxRemitted = taxRemitted;
        TotalDiscount = totalDiscount;
        DiscountCode = discountCode;
        Source = source;
        Note = note;
        PrivateNote = privateNote;
    }

    public static Order Create(string orderNumber, Buyer buyer, DateTime orderDate,
        string? status, string transactonId, Shippment shippment, string currencyCode,
        IEnumerable<OrderLineItem> orderLineItems, string? paymentStatus, int itemCount,
        decimal itemTotal, decimal totalPrice, decimal totalShipping, decimal totalTax,
        decimal taxRemitted, decimal totalDiscount, string? discountCode,
        string source, string? note, string? privateNote)
    {
        return new Order(orderNumber, buyer, orderDate, status, transactonId, shippment, currencyCode, orderLineItems,
            paymentStatus, itemCount, itemTotal, totalPrice, totalShipping, totalTax, taxRemitted, totalDiscount,
            discountCode, source, note, privateNote);
    }
    
}