using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Dima.Web.Security;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace Dima.Web.Pages.Identity;

public partial class RegisterPage : ComponentBase
{ 
    #region Dependencies 
    
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    
    [Inject]
    public IAccountHandler Handler { get; set; } = null!;
    
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    
    [Inject]
    public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
    
    #endregion

    #region Properties
    
    public bool IsBusy { get; set; } = false;
    public RegisterRequest IputModel { get; set; } = new();
    
    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        Console.WriteLine($"Usuário autenticado? {user.Identity?.IsAuthenticated}");

        if (user.Identity is { IsAuthenticated: true })
        {
            Console.WriteLine("Redirecionando para home...");
            NavigationManager.NavigateTo("/");
        }
    }

    #endregion

    #region Methods
    
    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
         var result = await Handler.RegisterAsync(IputModel);

         if (result.IsSuccess) 
         {
             Snackbar.Add(result.Message, Severity.Success);
             NavigationManager.NavigateTo("/login");
         }else 
         {
             Snackbar.Add(result.Message, Severity.Error);
         }
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }
    
    #endregion
}
