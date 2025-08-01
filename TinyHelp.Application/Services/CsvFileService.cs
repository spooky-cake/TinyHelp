using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using CsvHelper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TinyHelp.Domain.Entities.Orders;
using TinyHelp.Domain.Interfaces.Services;

namespace TinyHelp.Application.Services;

public class CsvFileService : ICsvFileService
{
    public EventCallback<int> ProgressChanged { get; set; }

    public async Task<IList<Order>> ReadCsv(IBrowserFile file)
    {
        if (file is null)
        {
            throw new FileNotFoundException();
        }
        
        var orders = new List<Order>();
        
        using var reader = new StreamReader(file.OpenReadStream());
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        await csv.ReadAsync();
        csv.ReadHeader();
        while (await csv.ReadAsync())
        {
            
            
            string orderNumber = csv.GetField<string>("Number") ?? throw new InvalidOperationException();
            
            Buyer buyer = new(
                firstName: csv.GetField<string>("Buyer first name") ?? throw new InvalidOperationException(),
                lastName: csv.GetField<string>("Buyer last name") ?? string.Empty,
                email: csv.GetField<string>("Buyer email") ?? string.Empty,
                phoneNumber: csv.GetField<string>("Buyer phone number") ?? string.Empty
            );
            
            DateTime orderDate = BuildDate(
                csv.GetField<string>("Date") ?? throw new InvalidEnumArgumentException(),
                csv.GetField<string>("Time") ?? throw new InvalidEnumArgumentException());
            
            string? status = csv.GetField<string>("Status");
            string? paymentStatus = csv.GetField<string>("Payment status");
            string transactionId = csv.GetField<string>("Transaction ID") ?? throw new InvalidOperationException();
           
            Shippment shippment = new(
                shipmentStatus: csv.GetField<string>("Shipping status") ?? throw new InvalidOperationException(),
                shippingAddress: new(
                    csv.GetField<string>("Shipping address 1") + "," + csv.GetField<string>("Shipping address 2") ??
                    string.Empty),
                shippingCity: new(csv.GetField<string>("Shipping city") ?? throw new InvalidOperationException()),
                shippingState: new(csv.GetField<string>("Shipping state") ?? throw new InvalidOperationException()),
                shippingZipCode: new(csv.GetField<string>("Shipping zip") ?? throw new InvalidOperationException()),
                shippingCountry: new(csv.GetField<string>("Shipping country") ?? throw new InvalidOperationException())
            );
            
            string currencyCode = csv.GetField<string>("Currency") ?? throw new InvalidOperationException();
            
            string itemString = csv.GetField<string>("Items") ?? throw new InvalidOperationException();
            IEnumerable<OrderLineItem> items = this.SetItemsFromString(itemString);
            
            int itemCount = csv.GetField<int>("Item count");        
            decimal itemTotal = csv.GetField<decimal>("Item total");
            decimal totalPrice = csv.GetField<decimal>("Total price");
            decimal totalShipping = csv.GetField<decimal>("Total shipping");
            decimal totalTax = csv.GetField<decimal>("Total tax");
            decimal taxRemitted = csv.GetField<decimal>("Tax remitted by Big Cartel");
            decimal totalDiscount = csv.GetField<decimal>("Total discount");
            string? discountCode = csv.GetField<string?>("Discount code");
            string source = csv.GetField<string>("Source") ?? string.Empty;
            string? note = csv.GetField<string>("Note");
            string? privateNotes = csv.GetField<string>("Private notes");

            var order = Order.Create(
                orderNumber: orderNumber,
                buyer: buyer,
                orderDate: orderDate,
                status: status,
                transactonId: transactionId,
                shippment: shippment,
                currencyCode: currencyCode,
                orderLineItems: items,
                paymentStatus: paymentStatus,
                itemCount: itemCount,
                itemTotal: itemTotal,
                totalPrice: totalPrice,
                totalShipping: totalShipping,
                totalTax: totalTax,
                taxRemitted: taxRemitted,
                totalDiscount: totalDiscount,
                discountCode: discountCode,
                source: source,
                note: note,
                privateNote: privateNotes
            );
            orders.Add(order);
        }
        return orders;
    }

    private IEnumerable<OrderLineItem> SetItemsFromString(string itemsString)
    {
        List<OrderLineItem> orderLineItems = [];
        
        foreach (var line in itemsString.Split(";"))
        {
            var lineProperties = line.Split("|");
            string productName = lineProperties
                .First(x => x.StartsWith("product_name"))
                .Split(":")[1];
            string? productOptionNamePropertie = lineProperties
                .First(x => x.StartsWith("product_option_name"))?
                .Split(":")
                .ElementAtOrDefault(1);

            int quantity;
            int.TryParse(lineProperties
                .First(x => x.StartsWith("quantity"))
                .Split(":")
                .ElementAtOrDefault(1), out quantity);
            
            decimal unitPrice;
            decimal.TryParse(lineProperties
                .First(x => x.StartsWith("price"))
                .Split(":")
                .ElementAtOrDefault(1), out unitPrice);

            decimal total;
            decimal.TryParse(lineProperties
                .First(x => x.StartsWith("total"))
                .Split(":")
                .ElementAtOrDefault(1), out total);
            
            var orderLineItem = new OrderLineItem(productName, productOptionNamePropertie, quantity, unitPrice, total);
            orderLineItems.Add(orderLineItem);
        }     
        return orderLineItems;
    }

    private DateTime BuildDate(string date, string time)
    {
        var timePattern = "\\d{1,2}:\\d{2} (AM|PM)";
        Match matchTime = Regex.Match(time, timePattern);
        if (!matchTime.Success)
        {
            throw new ArgumentException($"Invalid date: {time}");
        }
        
        var timeString = matchTime.Value;
        bool tryTimeOnly = TimeOnly.TryParse(timeString, out TimeOnly timeOnly);
        bool tryDateOnly = DateOnly.TryParse(date, out DateOnly dateOnly);
        
        if (!tryDateOnly || !tryTimeOnly)
        {
            throw new ArgumentException($"Invalid date: {date}, Invalid time: {time}");
        }

        DateTime fullDate = dateOnly.ToDateTime(timeOnly);
        return fullDate;
    }
}