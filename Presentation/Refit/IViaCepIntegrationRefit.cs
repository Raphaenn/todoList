using Refit;

namespace TodoList.Presentation.Refit;
public interface IViaCepIntegrationRefit
{
    [Get("/ws/{cep}/json")]
    Task<ApiResponse<RefitResponse>> GetDataViaCep(string cep);
}