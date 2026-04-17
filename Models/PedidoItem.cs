namespace WpfApp.Models
{
    public class PedidoItem
    {
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal Subtotal => ValorUnitario * Quantidade;
    }
}