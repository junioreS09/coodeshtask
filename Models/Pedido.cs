using System;
using System.Collections.Generic;

namespace WpfApp.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int IdPessoa { get; set; }
        public List<PedidoItem> Produtos { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataVenda { get; set; }
        public string FormaPagamento { get; set; }
        public int Parcelas { get; set; }
        public string Status { get; set; }

        public string FormaPagamentoCompleto =>
            Parcelas > 1 ? $"{FormaPagamento} {Parcelas}x" : FormaPagamento;

        public Pedido()
        {
            DataVenda = DateTime.Now;
            Status = "Pendente";
            Produtos = new List<PedidoItem>();
        }
    }
}