using WpfApp.Services;
using WpfApp.Models;
using System.Linq;
using System.Collections.ObjectModel;

namespace WpfApp.ViewModels
{
    public class PessoaViewModel : ViewModelBase
    {
        private bool _formularioHabilitado;
        private bool _modoEdicao; // false = novo, true = editando
        public bool ModoEdicao
        {
            get { return _modoEdicao; }
            set
            {
                _modoEdicao = value;
                OnPropertyChanged(nameof(ModoEdicao));
            }
        }

        public bool FormularioHabilitado
        {
            get { return _formularioHabilitado; }
            set
            {
                _formularioHabilitado = value;
                OnPropertyChanged(nameof(FormularioHabilitado));
            }
        }
        private readonly PessoaService _service;
        public PessoaViewModel()
        {
            _service = new PessoaService();
            Pessoas = new ObservableCollection<Pessoa>();
        }
        public int Id{ get; set;}
        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set
            {
                _nome = value;
                OnPropertyChanged(nameof(Nome));
            }
        }
        private string _cpf;
        public string Cpf
        {
            get { return _cpf; }
            set
            {
                _cpf = value;
                OnPropertyChanged(nameof(Cpf));
            }
        }
        private string _endereco;
        public string Endereco
        {
            get { return _endereco; }
            set
            {
                _endereco = value;
                OnPropertyChanged(nameof(Endereco));
            }
        }
        private string _nomeFiltro;
        public string NomeFiltro
        {
            get { return _nomeFiltro; }
            set
            {
                _nomeFiltro = value;
                OnPropertyChanged(nameof(NomeFiltro));
            }
        }
        private string _cpfFiltro;
        public string CpfFiltro
        {
            get { return _cpfFiltro; }
            set
            {
                _cpfFiltro = value;
                OnPropertyChanged(nameof(CpfFiltro));
            }
        }
        private ObservableCollection<Pessoa> _pessoas;
        public ObservableCollection<Pessoa> Pessoas
        {
            get { return _pessoas; }
            set
            {
                _pessoas = value;
                OnPropertyChanged(nameof(Pessoas));
            }
        }
        public void Salvar()
        {
            var pessoa = new Pessoa
            {
                Nome = this.Nome,
                Cpf = this.Cpf,
                Endereco = this.Endereco
            };
            _service.Salvar(pessoa);
            Pessoas = new ObservableCollection<Pessoa>(_service.ObterTodos());
            LimparCampos();
        }
        public void Atualizar()
        {
            var pessoa = _service.ObterPorId(Id);
            if (pessoa != null)
            {
                pessoa.Nome = this.Nome;
                pessoa.Cpf = this.Cpf;
                pessoa.Endereco = this.Endereco;

                _service.Atualizar(pessoa);
                Pessoas = new ObservableCollection<Pessoa>(_service.ObterTodos());
                LimparCampos();
            }
        }
        public void Buscar()
        {
            var resultados = _service.Buscar(NomeFiltro, CpfFiltro);
            Pessoas = new ObservableCollection<Pessoa>(resultados);
        }
        public void Excluir(int id)
        {
                _service.Excluir(id);
                Pessoas = new ObservableCollection<Pessoa>(_service.ObterTodos());
                LimparCampos();
        }
        public void LimparCampos()
        {
            Nome = string.Empty;
            Cpf = string.Empty;
            Endereco = string.Empty;
        }
    }
}