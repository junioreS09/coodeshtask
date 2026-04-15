using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.ViewModels
{
    public class PessoaDetalheViewModel : ViewModelBase
    {
        private readonly PessoaService _pessoaService;
        private readonly PedidoService _pedidoService;
        private List<Pedido> _todosPedidos;

        public bool IsNova { get; private set; }

        public PessoaDetalheViewModel(Pessoa pessoa)
        {
            _pessoaService = new PessoaService();
            _pedidoService = new PedidoService();

            IsNova = pessoa.Id == 0;
            Id = pessoa.Id;
            Nome = pessoa.Nome;
            Cpf = pessoa.Cpf;
            Endereco = pessoa.Endereco;
            
            Pedidos = new ObservableCollection<Pedido>();
            _todosPedidos = new List<Pedido>(); 
            if (!IsNova)
                CarregarPedidos();
        }

        public int Id { get; set; }

        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; OnPropertyChanged(nameof(Nome)); }
        }

        private string _cpf;
        public string Cpf
        {
            get { return _cpf; }
            set { _cpf = value; OnPropertyChanged(nameof(Cpf)); }
        }

        private string _endereco;
        public string Endereco
        {
            get { return _endereco; }
            set { _endereco = value; OnPropertyChanged(nameof(Endereco)); }
        }

        private ObservableCollection<Pedido> _pedidos;
        public ObservableCollection<Pedido> Pedidos
        {
            get { return _pedidos; }
            set { _pedidos = value; OnPropertyChanged(nameof(Pedidos)); }
        }

        public void Salvar()
        {
            if (IsNova)
            {
                var pessoa = new Pessoa { Nome = Nome, Cpf = Cpf, Endereco = Endereco };
                _pessoaService.Salvar(pessoa);
                Id = pessoa.Id;
                IsNova = false;
            }
            else
            {
                var pessoa = new Pessoa { Id = Id, Nome = Nome, Cpf = Cpf, Endereco = Endereco };
                _pessoaService.Atualizar(pessoa);
            }
        }

        public void CarregarPedidos()
        {
            _todosPedidos = _pedidoService.ObterPorPessoa(Id);
            Pedidos = new ObservableCollection<Pedido>(_todosPedidos);
        }

        public void FiltrarPedidos(string status)
        {
            if (string.IsNullOrEmpty(status) || status == "Todos")
            {
                Pedidos = new ObservableCollection<Pedido>(_todosPedidos);
            }
            else
            {
                var filtrados = _todosPedidos.Where(p => p.Status == status).ToList();
                Pedidos = new ObservableCollection<Pedido>(filtrados);
            }
        }

        public void AtualizarStatus(int idPedido, string status)
        {
            _pedidoService.AtualizarStatus(idPedido, status);
            CarregarPedidos();
        }
    }
}
