namespace MinhaLojaExpress.Testes.Integracao.API.Controllers
{
    [Collection("Teste API")]
    public class DescontoControllerTeste
    {
        private readonly HttpClient Client;

        public DescontoControllerTeste(ApiFactory api)
        {
            Client = api.Client;
        }
    }
}
