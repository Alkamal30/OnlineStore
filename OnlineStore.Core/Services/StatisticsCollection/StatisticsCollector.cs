using ClosedXML.Excel;
using MailKit.Net.Smtp;
using MimeKit;
using OnlineStore.Core.Abstractions.Services.Crud;
using OnlineStore.Core.Abstractions.Services.Email;
using OnlineStore.Core.Abstractions.Services.StatisticsCollection;
using OnlineStore.Core.Abstractions.Models;

namespace OnlineStore.Core.Services;

public class StatisticsCollector : IStatisticsCollector
{

    public StatisticsCollector(IEmailService emailService, IOrderCrudService orderCrudService)
    {
        _emailService = emailService;
        _orderCrudService = orderCrudService;
    }


    private IEmailService _emailService;
    private IOrderCrudService _orderCrudService;



    public async Task CollectAndSendAsync()
    {
        try
        {
            await CreateExcelTable();
            await _emailService.SendFileAsync(
                "yurevich.vlad3005@gmail.com",
                "Daily Statistics",
                "",
                "Statistics.xlsx"
            );
        }
        catch (Exception exception)
        {
            throw new NullReferenceException();
        }
    }


    private async Task CreateExcelTable()
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Orders");
        var currentRow = 1;
        var orders = await _orderCrudService.GetAllAsync();

        worksheet.Cell(currentRow, 1).Value = "Order ID";
        worksheet.Cell(currentRow, 2).Value = "Customer";
        worksheet.Cell(currentRow, 3).Value = "Formation Date";
        worksheet.Cell(currentRow, 4).Value = "Total Price";

        foreach (var order in orders)
        {
            currentRow++;
            worksheet.Cell(currentRow, 1).Value = order.Id;
            worksheet.Cell(currentRow, 2).Value = order.Customer.Login;
            worksheet.Cell(currentRow, 3).Value = order.FormationDate;
            worksheet.Cell(currentRow, 4).Value = CalculateOrderTotalPrice(order);
        }

        using (var memoryStream = new MemoryStream())
        {
            workbook.SaveAs(memoryStream);
            File.WriteAllBytes("Statistics.xlsx", memoryStream.ToArray());
        }
    }

    private double CalculateOrderTotalPrice(Order order) {
        double totalPrice = 0.0;

        foreach(var orderItem in order.OrderItems) {
            totalPrice += orderItem.Count * orderItem.Product.Price;
        }

        return totalPrice;
    }
}
