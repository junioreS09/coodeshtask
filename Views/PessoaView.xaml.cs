using System.Windows;
using WpfApp.Models;
using WpfApp.ViewModels;
using WpfApp.Helpers;

namespace WpfApp.Views
{
    /// <summary>
    /// Lógica interna para PessoaView.xaml
    /// </summary>
    public partial class PessoaView : Window
    {
        private PessoaViewModel _viewModel;
        public PessoaView()
        {
            InitializeComponent();
            _viewModel = new PessoaViewModel();
            DataContext = _viewModel;
        }
        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Buscar();
        }
        private void BtnIncluir_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.LimparCampos();
            _viewModel.ModoEdicao = false;
            _viewModel.FormularioHabilitado = true;
        }
        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.ModoEdicao)
            {
                _viewModel.Atualizar();
            }
            else
            {
                if (!ValidadorCpf.Validar(_viewModel.Cpf))
                {
                    MessageBox.Show("CPF inválido. Por favor, insira um CPF válido.", "Erro de Validação", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                _viewModel.Salvar();
            }
            _viewModel.ModoEdicao = false;
            _viewModel.FormularioHabilitado = false;
        }
        public void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var pessoa = GridPessoas.SelectedItem as Pessoa;
            if (pessoa != null)
            {
                _viewModel.Id = pessoa.Id;
                _viewModel.Nome = pessoa.Nome;
                _viewModel.Cpf = pessoa.Cpf;
                _viewModel.Endereco = pessoa.Endereco;
                _viewModel.ModoEdicao = true;
                _viewModel.FormularioHabilitado = true;
            }
        }
        public void BtnExcluir_Click(object sender, RoutedEventArgs e)
        {
            var pessoa = GridPessoas.SelectedItem as Pessoa;
            if ((pessoa != null) && (MessageBox.Show("Tem certeza que deseja excluir esta pessoa?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes))
            {
                _viewModel.Excluir(pessoa.Id); 
                _viewModel.ModoEdicao = false;
                _viewModel.FormularioHabilitado = false;               
            }
            
        }
    }
}
