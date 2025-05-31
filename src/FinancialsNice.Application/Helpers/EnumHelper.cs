namespace FinancialsNice.Application.Helpers;

public static class EnumHelper
{
    private static readonly Dictionary<string, string> _translations = new(StringComparer.OrdinalIgnoreCase)
    {
        // Transaction Status
        { "PENDING", "Pendente" },
        { "COMPLETED", "Concluído" },
        { "FAILED", "Falhou" },

        // Payment Type
        { "PIX", "Pix" },
        { "CREDIT", "Crédito" },
        { "DEBIT", "Débito" },
        { "BANKSLIP", "Boleto" },
        { "TED", "TED" },
        { "CASH", "Dinheiro" },

        // Categories
        { "SERVICES", "Serviços" },
        { "FOOD", "Alimentação" },
        { "TRANSPORT", "Transporte" },
        { "HOUSING", "Moradia" },
        { "HEALTH", "Saúde" },
        { "EDUCATION", "Educação" },
        { "LEISURE", "Lazer" },
        { "SHOPPING", "Compras" },
        { "SALARY", "Salário" },
        { "TAXES", "Impostos" },
        { "INVESTMENTS", "Investimentos" },
        { "ENTERTAINMENT", "Entretenimento" },
        { "DONATIONS", "Doações" },
        { "GIFTS", "Presentes" },
        { "TRAVEL", "Viagem" },
        { "OTHER", "Outro" },
        
        // Status
        { "ACTIVE ", "Ativo" },
        { "INACTIVE ", "Inativo" },
        
        // Months
        { "JANUARY", "Janeiro" },
        { "FEBRUARY", "Fevereiro" },
        { "MARCH", "Março" },
        { "APRIL", "Abril" },
        { "MAY", "Maio" },
        { "JUNE", "Junho" },
        { "JULY", "Julho" },
        { "AUGUST", "Agosto" },
        { "SEPTEMBER", "Setembro" },
        { "OCTOBER", "Outubro" },
        { "NOVEMBER", "Novembro" },
        { "DECEMBER", "Dezembro" },
        
        // UserType
        { "PERSON", "Pessoa Física" },
        { "COMPANY", "Empresa" },
        
        // TransactionType
        { "PAY", "Pagamento" },
        { "RECEIVE", "Recebimento" }
        
    };
    
    public static string TranslateEnumValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return value;

        if (_translations.TryGetValue(value.Trim(), out var translated))
            return translated;

        // Fallback: PascalCase the input
        return ToPascalCase(value);
    }
    
    private static string ToPascalCase(string input)
    {
        return string.Join("",
            input
                .ToLowerInvariant()
                .Split(new[] { '_', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => char.ToUpper(s[0]) + s.Substring(1)));
    }
}
