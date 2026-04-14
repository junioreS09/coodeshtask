using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using WpfApp.Models;

namespace WpfApp.Services
{
    public class PessoaService
    {
        private const string CaminhoArquivo = "Data/pessoas.json";

        public List<Pessoa> ObterTodos()
        {
            if (!File.Exists(CaminhoArquivo))
            {                
                return new List<Pessoa>();
            }
            else
            {
                var conteudo = File.ReadAllText(CaminhoArquivo);
                return JsonConvert.DeserializeObject<List<Pessoa>>(conteudo);
            }
        }
        public Pessoa ObterPorId(int id)
        {
            var lista = ObterTodos();
            return lista.FirstOrDefault(p => p.Id == id);
        }
        public List<Pessoa> Buscar(string nome, string cpf)
        {
            var lista = ObterTodos();
            return lista.Where(p =>
                (string.IsNullOrEmpty(nome) || (!string.IsNullOrEmpty(p.Nome) && p.Nome.IndexOf(nome, StringComparison.OrdinalIgnoreCase) >= 0)) &&
                (string.IsNullOrEmpty(cpf) || (!string.IsNullOrEmpty(p.Cpf) && p.Cpf.IndexOf(cpf, StringComparison.OrdinalIgnoreCase) >= 0))
            ).ToList();

        }
        public void Salvar(Pessoa pessoa)
        {             
            Directory.CreateDirectory("Data");                
          
            var lista = ObterTodos();
            pessoa.Id = lista.Count > 0 ? lista.Max(p => p.Id) + 1 : 1; // Atribui um ID baseado na contagem atual da lista
            lista.Add(pessoa);            

            var json = JsonConvert.SerializeObject(lista, Formatting.Indented);
            File.WriteAllText(CaminhoArquivo, json);
        }
        public void Atualizar(Pessoa pessoa)
        {
            var lista = ObterTodos();
            var pessoaExistente = lista.FirstOrDefault(p => p.Id == pessoa.Id);
           
            if (pessoaExistente != null)
            {                
                pessoaExistente.Nome = pessoa.Nome;
                pessoaExistente.Cpf = pessoa.Cpf;
                pessoaExistente.Endereco = pessoa.Endereco;

                var json = JsonConvert.SerializeObject(lista, Formatting.Indented);
                File.WriteAllText(CaminhoArquivo, json);
            }
        }
        public void Excluir(int id)
        {
            var lista = ObterTodos();
            var pessoaExistente = lista.FirstOrDefault(p => p.Id == id);
            if (pessoaExistente != null)
            {
                lista.Remove(pessoaExistente);
                var json = JsonConvert.SerializeObject(lista, Formatting.Indented);
                File.WriteAllText(CaminhoArquivo, json);
            }
        }
    }
}