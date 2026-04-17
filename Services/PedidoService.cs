using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using WpfApp.Models;

namespace WpfApp.Services
{
    public class PedidoService
    {
        private const string CaminhoArquivo = "Data/pedidos.json";

        public List<Pedido> ObterTodos()
        {
            if (!File.Exists(CaminhoArquivo))
                return new List<Pedido>();
            var conteudo = File.ReadAllText(CaminhoArquivo);
            return JsonConvert.DeserializeObject<List<Pedido>>(conteudo);
        }

        public List<Pedido> ObterPorPessoa(int idPessoa)
        {
            return ObterTodos().Where(p => p.IdPessoa == idPessoa).ToList();
        }

        public void Salvar(Pedido pedido)
        {
            Directory.CreateDirectory("Data");
            var lista = ObterTodos();
            pedido.Id = lista.Count > 0 ? lista.Max(p => p.Id) + 1 : 1;
            lista.Add(pedido);
            File.WriteAllText(CaminhoArquivo, JsonConvert.SerializeObject(lista, Formatting.Indented));
        }

        public void AtualizarStatus(int id, string status)
        {
            var lista = ObterTodos();
            var pedido = lista.FirstOrDefault(p => p.Id == id);
            if (pedido != null)
            {
                pedido.Status = status;
                File.WriteAllText(CaminhoArquivo, JsonConvert.SerializeObject(lista, Formatting.Indented));
            }
        }

        public void Excluir(int id)
        {
            var lista = ObterTodos();
            var pedido = lista.FirstOrDefault(p => p.Id == id);
            if (pedido != null && pedido.Status == "Pendente")
            {
                lista.Remove(pedido);
                File.WriteAllText(CaminhoArquivo, JsonConvert.SerializeObject(lista, Formatting.Indented));
            }
        }
    }
}
