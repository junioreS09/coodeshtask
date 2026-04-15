using System.Collections.ObjectModel;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.ViewModels
{
    public class PessoaViewModel : ViewModelBase
    {
        private readonly PessoaService _service;

        public PessoaViewModel()
        {
            _service = new PessoaService();
            Pessoas = new ObservableCollection<Pessoa>();
        }

        private string _nomeFiltro;
        public string NomeFiltro
        {
            get { return _nomeFiltro; }
            set { _nomeFiltro = value; OnPropertyChanged(nameof(NomeFiltro)); }
        }

        private string _cpfFiltro;
        public string CpfFiltro
        {
            get { return _cpfFiltro; }
            set { _cpfFiltro = value; OnPropertyChanged(nameof(CpfFiltro)); }
        }

        private ObservableCollection<Pessoa> _pessoas;
        public ObservableCollection<Pessoa> Pessoas
        {
            get { return _pessoas; }
            set { _pessoas = value; OnPropertyChanged(nameof(Pessoas)); }
        }

        public void Buscar()
        {
            var resultados = _service.Buscar(NomeFiltro, CpfFiltro);
            Pessoas = new ObservableCollection<Pessoa>(resultados);
        }

        public void Excluir(int id)
        {
            _service.Excluir(id);
            Buscar();
        }

        public Pessoa ObterNova() => new Pessoa();

        public Pessoa ObterPorId(int id) => _service.ObterPorId(id);
    }
}
