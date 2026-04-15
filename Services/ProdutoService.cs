using System.Linq;
using WpfApp.Models;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace WpfApp.Services
{
    public class ProdutoService
    {
        private const string CaminhoArquivo = "Data/produtos.json";

        public List<Produto> ObterTodos()
        {
            if (!File.Exists(CaminhoArquivo))
            {
                return new List<Produto>();
            }
            else
            {
                var conteudo = File.ReadAllText(CaminhoArquivo);
                return JsonConvert.DeserializeObject<List<Produto>>(conteudo);
            }
        }
        public Produto ObterPorId(int id)
        {
            var lista = ObterTodos();
            return lista.FirstOrDefault(p => p.Id == id);
        }
        public List<Produto> Buscar(string nome, string codigo, decimal? valorMin, decimal? valorMax)
        {
            var lista = ObterTodos();
            return lista.Where(p =>
                (string.IsNullOrEmpty(nome) || (!string.IsNullOrEmpty(p.Nome) && p.Nome.IndexOf(nome, System.StringComparison.OrdinalIgnoreCase) >= 0)) &&
                (string.IsNullOrEmpty(codigo) || (!string.IsNullOrEmpty(p.Codigo) && p.Codigo.IndexOf(codigo, System.StringComparison.OrdinalIgnoreCase) >= 0)) &&
                (!valorMin.HasValue || p.Valor >= valorMin.Value) &&
                (!valorMax.HasValue || p.Valor <= valorMax.Value)
            ).ToList();

        }
        public void Salvar(Produto produto)
        {
            Directory.CreateDirectory("Data");

            var lista = ObterTodos();
            produto.Id = lista.Count > 0 ? lista.Max(p => p.Id) + 1 : 1; // Atribui um ID baseado na contagem atual da lista
            lista.Add(produto);

            var json = JsonConvert.SerializeObject(lista, Formatting.Indented);
            File.WriteAllText(CaminhoArquivo, json);
        }
        public void Atualizar(Produto produto)
        {
            var lista = ObterTodos();
            var produtoExistente = lista.FirstOrDefault(p => p.Id == produto.Id);

            if (produtoExistente != null)
            {
                produtoExistente.Nome = produto.Nome;
                produtoExistente.Codigo = produto.Codigo;
                produtoExistente.Valor = produto.Valor;

                var json = JsonConvert.SerializeObject(lista, Formatting.Indented);
                File.WriteAllText(CaminhoArquivo, json);
            }
        }
        public void Excluir(int id)
        {
            var lista = ObterTodos();
            var produtoExistente = lista.FirstOrDefault(p => p.Id == id);

            if (produtoExistente != null)
            {
                lista.Remove(produtoExistente);

                var json = JsonConvert.SerializeObject(lista, Formatting.Indented);
                File.WriteAllText(CaminhoArquivo, json);
            }
        }
    }
}