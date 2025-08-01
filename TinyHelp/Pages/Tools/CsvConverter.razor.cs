using System.Globalization;
using CsvHelper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TinyHelp.Domain.Entities.Orders;
using TinyHelp.Domain.Interfaces.Services;

namespace TinyHelp.Pages.Tools;

public partial class CsvConverter : ComponentBase
{
    private IList<Order> _orders;
    private bool _isProcessed;

    private void SetOrders(IList<Order> orders)
    {
        _orders = orders;
        _isProcessed = true;
    }

    
        
        
    
}