using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;


namespace ChamadasWebApi
{
    public class Controle
    {
        private Conversoes converter = new Conversoes();

        internal Produto Consultar(int id)
        {
            HttpClient client = new HttpClient();
            System.Uri produtosUri;

            client.BaseAddress = new System.Uri("http://localhost:83/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/Produto/consultar/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                produtosUri = response.Headers.Location;

                var stringretorno = response.Content.ReadAsStringAsync();

                stringretorno.Wait();
                
                return converter.JsonToObject<Produto>(stringretorno.Result);
                
            }

            return null;
        }


        public List<Produto> listar()
        {
            HttpClient client = new HttpClient();
            System.Uri produtosUri;
            List<Produto> listaProduto = new List<Produto>();

            client.BaseAddress = new System.Uri("http://localhost:83/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/Produto/listar").Result;

            if (response.IsSuccessStatusCode)
            {
                produtosUri = response.Headers.Location;

                var stringretorno = response.Content.ReadAsStringAsync();

                stringretorno.Wait();
                
                listaProduto = converter.JsonToObject<List<Produto>>(stringretorno.Result);
                
            }
            return listaProduto;
        }

        
        public bool Excluir(int id)
        {
            HttpClient client = new HttpClient();
            System.Uri produtosUri;

            client.BaseAddress = new System.Uri("http://localhost:83/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response;

            response = client.DeleteAsync("api/Produto/excluir/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                produtosUri = response.Headers.Location;
                var stringretorno = response.Content.ReadAsStringAsync();
                stringretorno.Wait();
                var retorno = converter.JsonToObject<Boolean>(stringretorno.Result);
                return retorno;
            }
            else
            {
                return false;
            }
        }


        public bool Alterar()
        {
            Produto p = new Produto()  { IdProduto = 9, NomeProduto = "Nome produto",  ValorProduto = 100  };
            HttpClient client = new HttpClient();
            System.Uri produtosUri;

            client.BaseAddress = new System.Uri("http://localhost:83/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var teste = converter.ObjectToJson<Produto>(p);
            
            HttpContent content = new StringContent(teste, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync("api/Produto/alterar/", content).Result;

            if (response.IsSuccessStatusCode)
            {
                produtosUri = response.Headers.Location;
                var stringretorno = response.Content.ReadAsStringAsync();
                stringretorno.Wait();
                var retorno = converter.JsonToObject<Boolean>(stringretorno.Result);
                return retorno;
            }
            else
            {
                return false;
            }

        }



        public List<Produto> inserir()
        {

            Produto p = new Produto() { IdProduto = 9, NomeProduto = "Nome produto", ValorProduto = 100, Descricao = "Decrição" };
            HttpClient client = new HttpClient();
            System.Uri produtosUri;

            client.BaseAddress = new System.Uri("http://localhost:83/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var teste = converter.ObjectToJson<Produto>(p);

            HttpContent content = new StringContent(teste, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("api/Produto/inserir/", content).Result;

            if (response.IsSuccessStatusCode)
            {
                produtosUri = response.Headers.Location;
                var stringretorno = response.Content.ReadAsStringAsync();
                stringretorno.Wait();
                var retorno = converter.JsonToObject<int>(stringretorno.Result);

                List<Produto> lista = this.listar();

                lista.Add(p);

                return lista;
            }
            else
            {
                return null;
            }


        }


    }



    public class Produto
    {

        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public double ValorProduto { get; set; }
        public string Descricao { get; set; }

    }

}
















