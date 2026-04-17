using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.ViewModels
{
    public class PedidoViewModel : ViewModelBase
    {
        private readonly PedidoService _pedidoService;
        private readonly PessoaService _pessoaService;
        private readonly ProdutoService _produtoService;

        public PedidoViewModel(int idPessoa = 0)
        {
            _pedidoService = new PedidoService();
            _pessoaService = new PessoaService();
            _produtoService = new ProdutoService();

            Itens = new ObservableCollection<PedidoItem>();
            Pessoas = new ObservableCollection<Pessoa>(_pessoaService.ObterTodos());
            Produtos = new ObservableCollection<Produto>(_produtoService.ObterTodos());

            if (idPessoa != 0)
            {
                PessoaSelecionada = Pessoas.FirstOrDefault(p => p.Id == idPessoa);
                PessoaHabilitada = false;
            }
            else
            {
                PessoaHabilitada = true;
            }
        }

        private Pessoa _pessoaSelecionada;
        public Pessoa PessoaSelecionada
        {
            get { return _pessoaSelecionada; }
            set { _pessoaSelecionada = value; OnPropertyChanged(nameof(PessoaSelecionada)); }
        }

        private bool _pessoaHabilitada;
        public bool PessoaHabilitada
        {
            get { return _pessoaHabilitada; }
            set { _pessoaHabilitada = value; OnPropertyChanged(nameof(PessoaHabilitada)); }
        }

        private ObservableCollection<Pessoa> _pessoas;
        public ObservableCollection<Pessoa> Pessoas
        {
            get { return _pessoas; }
            set { _pessoas = value; OnPropertyChanged(nameof(Pessoas)); }
        }

        private ObservableCollection<Produto> _produtos;
        public ObservableCollection<Produto> Produtos
        {
            get { return _produtos; }
            set { _produtos = value; OnPropertyChanged(nameof(Produtos)); }
        }

        private Produto _produtoSelecionado;
        public Produto ProdutoSelecionado
        {
            get { return _produtoSelecionado; }
            set { _produtoSelecionado = value; OnPropertyChanged(nameof(ProdutoSelecionado)); }
        }

        private int _quantidade = 1;
        public int Quantidade
        {
            get { return _quantidade; }
            set { _quantidade = value; OnPropertyChanged(nameof(Quantidade)); }
        }

        private ObservableCollection<PedidoItem> _itens;
        public ObservableCollection<PedidoItem> Itens
        {
            get { return _itens; }
            set { _itens = value; OnPropertyChanged(nameof(Itens)); }
        }

        private decimal _valorTotal;
        public decimal ValorTotal
        {
            get { return _valorTotal; }
            set { _valorTotal = value; OnPropertyChanged(nameof(ValorTotal)); }
        }

        private string _formaPagamento;
        public string FormaPagamento
        {
            get { return _formaPagamento; }
            set { _formaPagamento = value; OnPropertyChanged(nameof(FormaPagamento)); }
        }

        private List<string> _parcelasOpcoes;
        public List<string> ParcelasOpcoes
        {
            get { return _parcelasOpcoes; }
            set { _parcelasOpcoes = value; OnPropertyChanged(nameof(ParcelasOpcoes)); }
        }

        private string _parcelaSelecionada;
        public string ParcelaSelecionada
        {
            get { return _parcelaSelecionada; }
            set { _parcelaSelecionada = value; OnPropertyChanged(nameof(ParcelaSelecionada)); }
        }

        private Visibility _visibilidadeParcelas = Visibility.Collapsed;
        public Visibility VisibilidadeParcelas
        {
            get { return _visibilidadeParcelas; }
            set { _visibilidadeParcelas = value; OnPropertyChanged(nameof(VisibilidadeParcelas)); }
        }

        public void AtualizarParcelas(string formaPagamento)
        {
            FormaPagamento = formaPagamento;
            if (formaPagamento == "Cartão")
            {
                ParcelasOpcoes = GerarParcelas(12);
                VisibilidadeParcelas = Visibility.Visible;
            }
            else if (formaPagamento == "Boleto")
            {
                ParcelasOpcoes = GerarParcelas(3);
                VisibilidadeParcelas = Visibility.Visible;
            }
            else
            {
                ParcelasOpcoes = null;
                ParcelaSelecionada = null;
                VisibilidadeParcelas = Visibility.Collapsed;
            }
        }

        private List<string> GerarParcelas(int max)
        {
            var lista = new List<string>();
            for (int i = 1; i <= max; i++)
                lista.Add(i == 1 ? "À vista" : $"{i}x");
            return lista;
        }

        private bool _finalizado;
        public bool Finalizado
        {
            get { return _finalizado; }
            set { _finalizado = value; OnPropertyChanged(nameof(Finalizado)); }
        }

        public void AdicionarItem()
        {
            if (ProdutoSelecionado == null || Quantidade <= 0) return;

            var itemExistente = Itens.FirstOrDefault(i => i.IdProduto == ProdutoSelecionado.Id);
            if (itemExistente != null)
            {
                itemExistente.Quantidade += Quantidade;
                Itens = new ObservableCollection<PedidoItem>(Itens);
            }
            else
            {
                Itens.Add(new PedidoItem
                {
                    IdProduto = ProdutoSelecionado.Id,
                    NomeProduto = ProdutoSelecionado.Nome,
                    Quantidade = Quantidade,
                    ValorUnitario = ProdutoSelecionado.Valor
                });
            }

            CalcularTotal();
            Quantidade = 1;
        }

        public void RemoverItem(PedidoItem item)
        {
            if (item == null) return;
            Itens.Remove(item);
            CalcularTotal();
        }

        public void CalcularTotal()
        {
            ValorTotal = Itens.Sum(i => i.ValorUnitario * i.Quantidade);
        }

        public bool Finalizar()
        {
            if (PessoaSelecionada == null || !Itens.Any() || string.IsNullOrEmpty(FormaPagamento))
                return false;

            if (VisibilidadeParcelas == Visibility.Visible && string.IsNullOrEmpty(ParcelaSelecionada))
                return false;

            int parcelas = 1;
            if (!string.IsNullOrEmpty(ParcelaSelecionada) && ParcelaSelecionada != "À vista")
                int.TryParse(ParcelaSelecionada.Replace("x", ""), out parcelas);

            var pedido = new Pedido
            {
                IdPessoa = PessoaSelecionada.Id,
                Produtos = Itens.ToList(),
                ValorTotal = ValorTotal,
                FormaPagamento = FormaPagamento,
                Parcelas = parcelas
            };

            _pedidoService.Salvar(pedido);
            Finalizado = true;
            return true;
        }
    }
}
