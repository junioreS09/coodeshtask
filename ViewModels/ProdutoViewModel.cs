using System.Collections.ObjectModel;
using WpfApp.Models;
using WpfApp.Services;
using System.Linq;

namespace WpfApp.ViewModels
{
    public class ProdutoViewModel : ViewModelBase
    {
        private readonly ProdutoService _produtoService;
        public int Id { get; set; }

        public ProdutoViewModel()
        {
            Produtos = new ObservableCollection<Produto>();
            _produtoService = new ProdutoService();
        }

        private bool _modoEdicao;
        public bool ModoEdicao
        {
            get { return _modoEdicao; }
            set { _modoEdicao = value; OnPropertyChanged(nameof(ModoEdicao)); }
        }

        private bool _formularioHabilitado;
        public bool FormularioHabilitado
        {
            get { return _formularioHabilitado; }
            set { _formularioHabilitado = value; OnPropertyChanged(nameof(FormularioHabilitado)); }
        }

        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; OnPropertyChanged(nameof(Nome)); }
        }

        private string _codigo;
        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; OnPropertyChanged(nameof(Codigo)); }
        }

        private decimal? _valor;        
        public decimal? Valor
        {
            get { return _valor; }
            set { _valor = value; OnPropertyChanged(nameof(Valor)); }
        }

        private string _nomeFiltro;
        public string NomeFiltro
        {
            get { return _nomeFiltro; }
            set { _nomeFiltro = value; OnPropertyChanged(nameof(NomeFiltro)); }
        }

        private string _codigoFiltro;
        public string CodigoFiltro
        {
            get { return _codigoFiltro; }
            set { _codigoFiltro = value; OnPropertyChanged(nameof(CodigoFiltro)); }
        }

        private decimal? _valorMinFiltro;
        public decimal? ValorMinFiltro
        {
            get { return _valorMinFiltro; }
            set { _valorMinFiltro = value; OnPropertyChanged(nameof(ValorMinFiltro)); }
        }

        private decimal? _valorMaxFiltro;
        public decimal? ValorMaxFiltro
        {
            get { return _valorMaxFiltro; }
            set { _valorMaxFiltro = value; OnPropertyChanged(nameof(ValorMaxFiltro)); }
        }

        private ObservableCollection<Produto> _produtos;
        public ObservableCollection<Produto> Produtos
        {
            get { return _produtos; }
            set { _produtos = value; OnPropertyChanged(nameof(Produtos)); }
        }

        public void Buscar()
        {
            var lista = _produtoService.Buscar(NomeFiltro, CodigoFiltro, ValorMinFiltro, ValorMaxFiltro);
            Produtos = new ObservableCollection<Produto>(lista);
        }

        public void Salvar()
        {
            var produto = new Produto { Nome = Nome, Codigo = Codigo, Valor = Valor ?? 0 };
            _produtoService.Salvar(produto);
            Buscar();
            LimparCampos();
            FormularioHabilitado = false;
        }

        public void Atualizar()
        {
            var produto = _produtoService.ObterPorId(Id);
            if (produto != null)
            {
                produto.Nome = Nome;
                produto.Codigo = Codigo;
                produto.Valor = Valor ?? 0;
                _produtoService.Atualizar(produto);
                Buscar();
                LimparCampos();
                FormularioHabilitado = false;
            }
        }

        public void Excluir(int id)
        {
            _produtoService.Excluir(id);
            Buscar();
            FormularioHabilitado = false;
        }

        public void LimparCampos()
        {
            Nome = string.Empty;
            Codigo = string.Empty;
            Valor = null;
        }
    }
}