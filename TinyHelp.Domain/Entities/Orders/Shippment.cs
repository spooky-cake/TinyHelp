namespace TinyHelp.Domain.Entities.Orders;

public record Shippment
{
    public string ShipmentStatus { get; init; }
    public ShippingAddress ShippingAddress { get; init; }
    public ShippingCity ShippingCity { get; init; }
    public ShippingState ShippingState { get; init; }
    public ShippingZipCode ShippingZipCode { get; init; }
    public ShippingCountry ShippingCountry { get; init; }

    public Shippment(string shipmentStatus, ShippingAddress shippingAddress, ShippingCity shippingCity,
        ShippingState shippingState, ShippingZipCode shippingZipCode, ShippingCountry shippingCountry)
    {
        ShipmentStatus = shipmentStatus;
        ShippingAddress = shippingAddress;
        ShippingCity = shippingCity;
        ShippingState = shippingState;
        ShippingZipCode = shippingZipCode;
        ShippingCountry = shippingCountry;
        
    }
}

public record ShippingAddress(string Value);
public record ShippingCity(string Value);
public record ShippingState(string Value);
public record ShippingZipCode(string Value);
public record ShippingCountry(string Value);