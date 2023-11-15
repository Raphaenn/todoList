using TodoList.Presentation;
using TodoList.Presentation.Refit;

namespace TodoList.Services;

public class ViaCepIntegration : IViaCepIntergration
{
    private readonly IViaCepIntegrationRefit _viaCepIntegrationRefit;

    public ViaCepIntegration(IViaCepIntegrationRefit viaCepIntegrationRefit)
    {
        _viaCepIntegrationRefit = viaCepIntegrationRefit;
    }
    
    public async Task<RefitResponse> GetDataCep(string cep)
    {
        var responseData = await _viaCepIntegrationRefit.GetDataViaCep(cep);
        if (responseData != null && responseData.IsSuccessStatusCode)
        {
            return responseData.Content;
        }

        return null;
    }
}