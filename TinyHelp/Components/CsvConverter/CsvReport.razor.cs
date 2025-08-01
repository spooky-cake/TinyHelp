using Microsoft.AspNetCore.Components;
using MudBlazor;
using TinyHelp.Components.CsvConverter.DataTransferObjects;
using TinyHelp.Domain.Entities.Orders;

namespace TinyHelp.Components.CsvConverter;

public partial class CsvReport : ComponentBase
{
    [Parameter] public IList<Order> Orders { get; set; } = null!;

    private IEnumerable<OrderDetailDto> _orderDetails = null!;
    private Order? _selectedOrder = null;

    private string _currencyCode = string.Empty;
    private bool _isLoading;
    private string _detailSearchString = string.Empty;

    private bool FilterFunc1(OrderDetailDto line) => FilterFunc(line, _detailSearchString);

    private bool FilterFunc(OrderDetailDto line, string searchString)
    {
        if (string.IsNullOrEmpty(searchString))
        {
            return true;
        }

        if (line.Buyer.FirstName.Contains(searchString)
            || (line.Buyer.LastName is not null && line.Buyer.LastName.Contains(searchString)))
            return true;
        
        if (line.Shippment.ShippingCountry.Value.Contains(searchString))
            return true;
        
        if(line.OrderLineItem.ProductName.Contains(searchString))
            return true;
        
        if(line.OrderLineItem.ProductOptionName is not null 
           && line.OrderLineItem.ProductOptionName.Contains(searchString))
            return true;
        
        return false;
    }

    private void RowClickEventDetail(TableRowClickEventArgs<OrderDetailDto> args)
    {
        var orderNumber = args.Item?.OrderNumber;
        var order = Orders.FirstOrDefault(o => o.OrderNumber == orderNumber);
        if (order is not null)
        {
            _selectedOrder = order;
        }
    }

    protected override async Task OnInitializedAsync()
    {
        _isLoading = true;

        _currencyCode = Orders.First().CurrencyCode;
        _orderDetails = Orders
            .SelectMany(items => items.OrderLineItems,
                ((order, item) => new
                    OrderDetailDto(
                        order.OrderNumber,
                        order.Buyer,
                        order.OrderDate,
                        order.TransactionId,
                        order.Shipment,
                        item
                    )
                ));

        _isLoading = false;
    }
}