using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TinyHelp.Domain.Entities.Orders;
using TinyHelp.Domain.Interfaces.Services;

namespace TinyHelp.Components.CsvConverter;

public partial class CsvLoader : ComponentBase
{
    [Inject]
    public ICsvFileService CsvFileService { get; set; } = null!;
    [Parameter]
    public EventCallback<IList<Order>> OnFileProcessed { get; set; }
    
    private IBrowserFile? _file;
    private IList<Order> _orders = new List<Order>();

    private string? _error = string.Empty;
    private bool _orderLoaded = false;
    private bool _isLoading = false;

    private int _progressBar = 0;
    
    private void LoadFiles(InputFileChangeEventArgs e)
    {
        _file = e.File;
        if (_file is null)
        {
            _error = "File is empty.";
            return;
        }

        if (_file.ContentType != "text/csv")
        {
            _error = "File is not a CSV file.";
            return;
        }
    }

    private async Task ProcessFile()
    {
        _isLoading = true;
        if (_file is null)
        {
            _error = "You must load a file.";
            return;
        }
        try
        {
            _orders = await CsvFileService.ReadCsv(_file);
            _orderLoaded = true;
            await OnFileProcessed.InvokeAsync(_orders);
        }
        catch (Exception ex)
        {
            _error = ex.Message;   
        }
        _isLoading = false;
    }
}