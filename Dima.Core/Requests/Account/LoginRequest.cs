using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Account;

public class LoginRequest : Request
{
    [Required(ErrorMessage = "E-mail")]
    [EmailAddress(ErrorMessage = "E-mail invalido")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Senha invalida")]
    public string Password { get; set; } = string.Empty;
}