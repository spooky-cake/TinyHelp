using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TinyHelp.Domain.Entities.Orders;

namespace TinyHelp.Domain.Interfaces.Services;

public interface ICsvFileService
{
    EventCallback<int> ProgressChanged { get; set; }
    Task<IList<Order>> ReadCsv(IBrowserFile file);
}