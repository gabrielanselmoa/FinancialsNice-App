using FinancialsNice.Application.Dtos.Transactions;
using FinancialsNice.Application.Helpers;
using FinancialsNice.Application.Interfaces.Services;
using FinancialsNice.Domain.Enums;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace FinancialsNice.Application.Services;

public class PdfGeneratorService : IPdfGenerator
{
    public byte[] GenerateTransactionReport(List<TransactionResponse> transactions)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var totalReceitas = transactions.Where(t => t.TransactionType == TransactionType.RECEIVE).Sum(t => t.Amount);
        var totalDespesas = transactions.Where(t => t.TransactionType == TransactionType.PAY).Sum(t => t.Amount);
        var total = totalReceitas + totalDespesas;

        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                // page.Margin(2.54f, Unit.Centimetre);
                page.Margin(20);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(10).FontFamily(Fonts.Trebuchet));

                // HEADER
                page.Header().Element(header =>
                {
                    header.Background(Colors.Blue.Medium).Padding(10).Column(col =>
                    {
                        col.Item().AlignCenter().Text("Financial Report")
                            .FontSize(16).Bold().FontColor(Colors.White);
                        col.Spacing(2);
                        // col.Item().AlignCenter().Text("Organizando suas finanças com o Financials Nice 😊")
                        //     .FontSize(11).FontColor(Colors.Grey.Lighten3);
                        col.Item().AlignCenter().Text($"Gerado em: {DateTime.Now:dd/MM/yyyy HH:mm}")
                            .FontSize(9).FontColor(Colors.Grey.Lighten3);
                    });
                });

                // CONTENT
                page.Content().PaddingVertical(20).Column(column =>
                {
                    column.Spacing(15);

                    // TÍTULO DAS TRANSAÇÕES
                    column.Item().Text("Transações Realizadas")
                        .FontSize(15).Bold().FontColor(Colors.Black).AlignCenter();

                    // TABELA
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2.5f); // PayerReceiver
                            // columns.RelativeColumn(2f); // Email
                            columns.RelativeColumn(1.2f); // Categoria
                            columns.RelativeColumn(1); // Câmbio
                            columns.RelativeColumn(1.5f); // Valor
                            columns.RelativeColumn(1.5f); // Tipo
                            // columns.RelativeColumn(1.5f); // Status (ligeiramente maior para textos como "COMPLETED")
                            columns.RelativeColumn(2f); // Data Agendada
                        });

                        // Cabeçalho
                        table.Header(header =>
                        {
                            void HeaderCell(string text) => header.Cell().Element(CellStyle).AlignCenter().Text(text);
                            HeaderCell("Entidade");
                            // HeaderCell("Email");
                            HeaderCell("Categoria");
                            HeaderCell("Câmbio");
                            HeaderCell("Valor");
                            HeaderCell("Tipo");
                            // HeaderCell("Status");
                            HeaderCell("Data Agendada");
                        });

                        IContainer CellStyle(IContainer container) => container
                            .Background(Colors.Blue.Medium)
                            .PaddingVertical(5)
                            .PaddingHorizontal(6)
                            .DefaultTextStyle(x => x.FontSize(10).Bold().FontColor(Colors.White));

                        // Dados
                        for (int i = 0; i < transactions.Count; i++)
                        {
                            var t = transactions[i];
                            // bool isEven = i % 2 == 0;
                            bool isPayment = t.TransactionType == TransactionType.PAY;
                            
                            IContainer DataCell(IContainer container) => container
                                // .Background(isPayment ? Colors.Red.Lighten2 : Colors.Green.Lighten2)
                                .Background(Colors.White)
                                .PaddingVertical(4)
                                .PaddingHorizontal(6)
                                .BorderColor(Colors.Blue.Lighten2);
                                // .BorderBottom(1)

                            table.Cell().Element(DataCell).AlignCenter().Text(t.PayerReceiver!.Name).FontColor
                                (isPayment ? Colors.Red.Lighten2 : Colors.Green.Lighten2);
                            // table.Cell().Element(DataCell).AlignCenter().Text(t.Email);
                            table.Cell().Element(DataCell).AlignCenter().Text(EnumHelper.TranslateEnumValue(t.Category.ToString())).FontColor
                                (isPayment ? Colors.Red.Lighten2 : Colors.Green.Lighten2);
                            table.Cell().Element(DataCell).AlignCenter().Text(t.Currency).FontColor
                                (isPayment ? Colors.Red.Lighten2 : Colors.Green.Lighten2);
                            table.Cell().Element(DataCell).AlignCenter().Text(t.Amount.ToString("C2")).FontColor
                                (isPayment ? Colors.Red.Lighten2 : Colors.Green.Lighten2);
                            table.Cell().Element(DataCell).AlignCenter().Text(EnumHelper.TranslateEnumValue(t.TransactionType.ToString())).FontColor
                                (isPayment ? Colors.Red.Lighten2 : Colors.Green.Lighten2);
                            // table.Cell().Element(DataCell).AlignCenter().Text(t.Status.ToString());
                            table.Cell().Element(DataCell).AlignCenter().Text(t.ScheduledAt.ToString("dd/MM/yyyy")).FontColor
                                (isPayment ? Colors.Red.Lighten2 : Colors.Green.Lighten2);
                        }

                        // Linha de total
                        table.Cell().ColumnSpan(6).Element(container =>
                        {
                            container.Background(Colors.Blue.Medium)
                                // .Padding(2)
                                .AlignCenter();
                            // Rodapé com colunas alinhadas com a tabela
                            table.Cell().Element(CellStyle).AlignCenter().Text("Total:").FontColor(Colors.White).Bold(); // PayerReceiver
                            // table.Cell().Element(CellStyle).AlignCenter().Text(""); // Email
                            table.Cell().Element(CellStyle).AlignCenter().Text(""); // Categoria
                            table.Cell().Element(CellStyle).AlignCenter().Text(""); // Câmbio
                            table.Cell().Element(CellStyle).AlignCenter().Text(total.ToString("C2")).FontColor(Colors.White).Bold(); // Valor
                            table.Cell().Element(CellStyle).AlignCenter().Text(""); // Tipo
                            // table.Cell().Element(CellStyle).AlignCenter().Text(""); // Status
                            table.Cell().Element(CellStyle).AlignCenter().Text($"Qtd: {transactions.Count}").FontColor(Colors.White).Bold(); // Data
                        });
                    });
                    
                    // RESUMO FINANCEIRO
                    column.Item().PaddingTop(15).Column(col =>
                    {
                        col.Spacing(6);
                        col.Item().Text("Resumo Financeiro")
                            .Bold().FontSize(15).FontColor(Colors.Black).AlignCenter();

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            // Cabeçalho da tabela
                            table.Header(header =>
                            {
                                header.Cell().Element(HeaderStyle).AlignCenter().Text("Receitas");
                                header.Cell().Element(HeaderStyle).AlignCenter().Text("Despesas");
                                header.Cell().Element(HeaderStyle).AlignCenter().Text("Total");
                            });

                            // Valores da tabela
                            table.Cell().Element(ValueStyleGreen).AlignCenter().Text($"{totalReceitas.ToString("C2")}");
                            table.Cell().Element(ValueStyleRed).AlignCenter().Text($"{totalDespesas.ToString("C2")}");
                            table.Cell().Element(ValueStyleBlue).AlignCenter().Text($"{total.ToString("C2")}");
                        });
                    });
                    // General Styles
                    IContainer HeaderStyle(IContainer container) => container
                        .Background(Colors.Yellow.Darken2)
                        .PaddingVertical(5)
                        .PaddingHorizontal(4)
                        .DefaultTextStyle(x => x.FontSize(10).Bold().FontColor(Colors.Black));

                    IContainer ValueStyleGreen(IContainer container) => container
                        // .Background(Colors.Green.Lighten3)
                        .Background(Colors.Grey.Lighten4)
                        .PaddingVertical(5)
                        .DefaultTextStyle(x => x.FontSize(10).SemiBold().FontColor(Colors.Green.Darken2));

                    IContainer ValueStyleRed(IContainer container) => container
                        // .Background(Colors.Red.Lighten3)
                        .Background(Colors.Grey.Lighten4)
                        .PaddingVertical(5)
                        .DefaultTextStyle(x => x.FontSize(10).SemiBold().FontColor(Colors.Red.Darken2));

                    IContainer ValueStyleBlue(IContainer container) => container
                        .Background(Colors.Grey.Lighten4)
                        .PaddingVertical(5)
                        .DefaultTextStyle(x => x.FontSize(10).SemiBold().FontColor(Colors.Black));
                });

                // FOOTER
                page.Footer().Element(footer =>
                {
                    footer.Background(Colors.Blue.Medium).Padding(10).Column(col =>
                    {
                        col.Item().AlignCenter().Text("FinNice").FontSize(13).Bold()
                            .FontColor(Colors.White);
                        col.Item().AlignCenter().Text("© 2025 FinNice. Todos os direitos reservados.").FontSize(9)
                            .FontColor(Colors.White);
                        col.Item().AlignCenter().Text("Contato: suporte@finnice.com | +55 (11) 99999-9999").FontSize(9)
                            .FontColor(Colors.White);
                        col.Spacing(5);
                    });
                });
            });
        }).GeneratePdf();
    }
}