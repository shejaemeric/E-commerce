using E_Commerce_Api.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections.Generic;
using System.Globalization;


namespace E_Commerce_Api.reportDocuments {

public class OrderReportDocument : IDocument
{
    private readonly ICollection<GroupedOrderDetails> _orders;
    private readonly string _title;

    public OrderReportDocument(ICollection<GroupedOrderDetails> orders,string title)
    {
        _orders = orders;
        _title = title;

    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    public DocumentSettings GetSettings() => DocumentSettings.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(1, Unit.Centimetre);
            page.DefaultTextStyle(x => x.FontSize(12));

            page.Header().Text(_title).SemiBold().FontSize(20).FontColor(Colors.Red.Darken4).AlignCenter();
            page.Content().Padding(20).Column(column =>
            {
                column.Spacing(50);

                foreach (var order in _orders)
                {
                    column.Item().Element(container => ComposeOrder(container, order));
                }
            });

            page.Footer().AlignCenter().Text(x =>
            {
                x.Span("Page ");
                x.CurrentPageNumber();
                x.Span(" of ");
                x.TotalPages();
            });
        });
    }

    void ComposeOrder(IContainer container, GroupedOrderDetails order)
    {
        container.Border(1).BorderColor(Colors.Grey.Lighten2).Padding(10).ShowEntire().Column(column =>
        {
            column.Spacing(10);
            column.Item().Text(x=> {
                x.Span("Order ID: ").Bold();
                x.Span($"{order.Id}");
            });

                        column.Item().Text(x=> {
                x.Span("Customer: ").Bold();
                x.Span($"{order.Customer}");
            });

            column.Item().Text(x=> {
                x.Span("Created At: ").Bold();
                x.Span($"{order.Created_At}");
            });

                        column.Item().Text(x=> {
                x.Span("Status: ").Bold();
                x.Span($"{order.Status}");
            });


            column.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().Element(HeaderCellStyle).Text("Product Name").FontColor(Colors.White);
                    header.Cell().Element(HeaderCellStyle).Text("Quantity").FontColor(Colors.White);
                    header.Cell().Element(HeaderCellStyle).Text("Price (Rwf)").FontColor(Colors.White);
                    header.Cell().Element(HeaderCellStyle).Text("Discount (%)").FontColor(Colors.White);
                    header.Cell().Element(HeaderCellStyle).Text("Total (Rwf)").FontColor(Colors.White);
                });
                var totalSpend = 0;
                foreach (var product in order.Products)
                {
                    table.Cell().Element(CellStyle).Text(product.ProductName);
                    table.Cell().Element(CellStyle).Text(product.Quantity.ToString());
                    table.Cell().Element(CellStyle).Text(FormatCurrency(product.Price));
                    table.Cell().Element(CellStyle).Text(product.Discount_percent.ToString());
                    table.Cell().Element(CellStyle).Text(FormatCurrency(product.Total));
                    totalSpend += product.Total;
                }
                    table.Cell().Element(CellStyle).Text("Total");
                    table.Cell().Element(CellStyle).Text("");
                    table.Cell().Element(CellStyle).Text("");
                    table.Cell().Element(CellStyle).Text("");
                    table.Cell().Element(CellStyle).Text($"{FormatCurrency(totalSpend)}");
                totalSpend = 0;
            });
        });
    }

    private string FormatCurrency(decimal amount)
    {
        return string.Format(new CultureInfo("en-RW"), "{0:C0}", amount);
    }

    private IContainer HeaderCellStyle(IContainer container)
    {
        return container
            .Background(Colors.Red.Darken4)
            .BorderBottom(1)
            .BorderColor(Colors.Grey.Lighten2)
            .PaddingVertical(8)
            .AlignCenter();
    }

    private IContainer CellStyle(IContainer container)
    {
        return container
            .BorderBottom(1)
            .BorderColor(Colors.Grey.Lighten2)
            .PaddingVertical(6)
            .AlignCenter();
    }
}

}
