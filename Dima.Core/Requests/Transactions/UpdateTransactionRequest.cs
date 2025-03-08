using System.ComponentModel.DataAnnotations;
using Dima.Core.Enums;

namespace Dima.Core.Requests.Transactions;

public class UpdateTransactionRequest : Request
{
    public long Id { get; set; }
    
    [Required(ErrorMessage = "Titulo invalido")]
    public string Title { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Tipo invalido")]
    public ETransactionType Type { get; set; }
    
    [Required(ErrorMessage = "Valor invalido")]
    public Decimal Amount { get; set; }

    [Required(ErrorMessage = "Categoria invalida")]
    public long CategoryId { get; set; }

    [Required(ErrorMessage = "Data invalida")]
    public DateTime? PaidOrReceivedAt { get; set; }
}