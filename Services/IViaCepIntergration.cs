using TodoList.Presentation;

namespace TodoList.Services;
public interface IViaCepIntergration
{
    Task<RefitResponse> GetDataCep(string cep);
}