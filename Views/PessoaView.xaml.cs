using System.Windows;
using System.Windows.Controls;
using WpfApp.Models;
using WpfApp.ViewModels;

namespace WpfApp.Views
{
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
            var tela = new PessoaDetalheView(new Pessoa())
            {
                Owner = this 
            };
            tela.ShowDialog();
            _viewModel.Buscar();
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var pessoa = GridPessoas.SelectedItem as Pessoa;
            if (pessoa == null)
            {
                MessageBox.Show("Selecione uma pessoa para editar.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var tela = new PessoaDetalheView(pessoa)
            {
                Owner = this 
            };
            tela.ShowDialog();
            _viewModel.Buscar();
        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e)
        {
            var pessoa = GridPessoas.SelectedItem as Pessoa;
            if (pessoa == null)
            {
                MessageBox.Show("Selecione uma pessoa para excluir.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (_viewModel.PossuiPedidos(pessoa.Id))
            {
                MessageBox.Show("Não é possível excluir esta pessoa pois ela possui pedidos vinculados.", "Operação não permitida", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (MessageBox.Show("Tem certeza que deseja excluir esta pessoa?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                _viewModel.Excluir(pessoa.Id);
            }
        }
    }
}
