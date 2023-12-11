using Api.Commands;
using Business.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.Json;

namespace TestProject
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private WebApplicationFactory<Program> _webApplicationFactory;
        public IntegrationTests(WebApplicationFactory<Program> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }
        [Fact]
        public async Task TarefaAddOK()
        {
            var client = _webApplicationFactory.CreateClient();

            var request = new CadastroTarefaCommand
            {
                Titulo = "Teste 90",
                Concluida = true,
            };

            var body = JsonSerializer.Serialize(request);
            var stringContent = new StringContent(body, Encoding.UTF8, "aplication/json");
            var response = await client.PostAsync("api/tarefas/adicionar-tarefa", stringContent);

            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);


        }

        [Fact]
        public async Task TarefaUpdateOK()
        {
            var client = _webApplicationFactory.CreateClient();     
            int id = 1;

            var request = new AlterarTarefaCommand
            {
                Id = 1,
                Titulo = "Teste 90 Atualizado",
                Concluida = false,
            };

            var body = JsonSerializer.Serialize(request);
            var stringContent = new StringContent(body, Encoding.UTF8, "application/json");

    
            var response = await client.PutAsync($"api/tarefas/atualizar-tarefa/{id}", stringContent);

            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task TarefaUpdateValidacao()
        {
            //Se a tarefa estiver como cloncluida não pode ser alterada novamente para concluida
            var client = _webApplicationFactory.CreateClient();
            int id = 2;

            var request = new AlterarTarefaCommand
            {
                Id = 2,
                Titulo = "Teste 10 Atualizado",
                Concluida = true,
            };

            var body = JsonSerializer.Serialize(request);
            var stringContent = new StringContent(body, Encoding.UTF8, "application/json");


            var response = await client.PutAsync($"api/tarefas/atualizar-tarefa/{id}", stringContent);

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("A tarefa só pode ser atualizada se não estiver como concluída!", content);
        }

        [Fact]
        public async Task TarefaUpdateValidacaoNaoExisteNaBase()
        {           
            var client = _webApplicationFactory.CreateClient();
            int id = 2222;

            var request = new AlterarTarefaCommand
            {
                Id = 2222,
                Titulo = "Teste 10 Atualizado",
                Concluida = true,
            };

            var body = JsonSerializer.Serialize(request);
            var stringContent = new StringContent(body, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/tarefas/atualizar-tarefa/{id}", stringContent);

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("O id da tarefa Não existe na base de dados!", content);
        }

        [Fact]
        public async Task TarefaUpdateValidacaoIDDiferentePassadoNoBody()
        {
            var client = _webApplicationFactory.CreateClient();
            int id = 1;

            var request = new AlterarTarefaCommand
            {
                Id = 2222,
                Titulo = "Teste 10 Atualizado",
                Concluida = true,
            };

            var body = JsonSerializer.Serialize(request);
            var stringContent = new StringContent(body, Encoding.UTF8, "application/json");


            var response = await client.PutAsync($"api/tarefas/atualizar-tarefa/{id}", stringContent);

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("O id informado não é o mesmo que foi passado na query", content);
        }

        [Fact]
        public async Task GetTarefasNaoCocluidaK()
        {
            var client = _webApplicationFactory.CreateClient();

            var response = await client.GetAsync("api/tarefas/obter-tarefas-nao-concluidas");
     
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);


            var content = await response.Content.ReadAsStringAsync();
            var tarefas = JsonSerializer.Deserialize<List<Tarefa>>(content);


            Assert.NotNull(tarefas);
            Assert.True(tarefas.Count > 0);
        }

        [Fact]
        public async Task TarefaDeleteOK()
        {
            var client = _webApplicationFactory.CreateClient();      
            int id = 1;
            var response = await client.DeleteAsync($"api/tarefas/excluir/{id}");

    
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);     
        }

        [Fact]
        public async Task TarefaDeleteValidacao()
        {
            var client = _webApplicationFactory.CreateClient();   
            int id = 1;
      
            var response = await client.DeleteAsync($"api/tarefas/excluir/{id}");

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
     
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("Uma tarefa só pode ser removida se estiver concluída!", content);
        }

        [Fact]
        public async Task GetTareErroNaoExisteNaBaseError()
        {
            var client = _webApplicationFactory.CreateClient();            
            int id = 999;

            var response = await client.GetAsync($"api/tarefas/buscar-por-id-tarefa{id}");

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
      
       
        }

        [Fact]
        public async Task GetTarefaPorIDOK()
        {
            var client = _webApplicationFactory.CreateClient();
            int id = 1;
         
            var response = await client.GetAsync($"api/tarefas/buscar-por-id-tarefa{id}");
       
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
      
            var content = await response.Content.ReadAsStringAsync();
            var tarefa = JsonSerializer.Deserialize<Tarefa>(content);


            Assert.NotNull(tarefa);
            Assert.Equal(id, tarefa.Id);
        }
    }
}