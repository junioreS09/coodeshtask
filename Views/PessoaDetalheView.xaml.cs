using System.Windows;
using System.Windows.Controls;
using WpfApp.Helpers;
using WpfApp.Models;
using WpfApp.ViewModels;

namespace WpfApp.Views
{
    public partial class PessoaDetalheView : Window
    {
        private PessoaDetalheViewModel _viewModel;

        public PessoaDetalheView(Pessoa pessoa)
        {
            InitializeComponent();
            _viewModel = new PessoaDetalheViewModel(pessoa);
            DataContext = _viewModel;
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidadorCpf.Validar(_viewModel.Cpf))
            {
                MessageBox.Show("CPF inválido. Por favor, insira um CPF válido.", "Erro de Validação", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(_viewModel.Nome))
            {
                MessageBox.Show("O nome é obrigatório. Por favor, insira um nome.", "Erro de Validação", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _viewModel.Salvar();
            MessageBox.Show("Pessoa salva com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnIncluirPedido_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.IsNova)
            {
                MessageBox.Show("Salve a pessoa antes de incluir um pedido.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var tela = new PedidoView(_viewModel.Id)
            {
                Owner = this
            };
            tela.ShowDialog();
            _viewModel.CarregarPedidos();
        }

        private void FiltroStatus_Checked(object sender, RoutedEventArgs e)
        {
            if (_viewModel == null) return;

            string status = "";
            if (RbPendente.IsChecked == true) status = "Pendente";
            else if (RbPago.IsChecked == true) status = "Pago";
            else if (RbRecebido.IsChecked == true) status = "Recebido";

            _viewModel.FiltrarPedidos(status);
        }

        private void BtnPago_Click(object sender, RoutedEventArgs e)
        {
            var pedido = (sender as Button).DataContext as Pedido;
            if (pedido != null)
                _viewModel.AtualizarStatus(pedido.Id, "Pago");
        }

        private void BtnEnviado_Click(object sender, RoutedEventArgs e)
        {
            var pedido = (sender as Button).DataContext as Pedido;
            if (pedido != null)
                _viewModel.AtualizarStatus(pedido.Id, "Enviado");
        }

        private void BtnRecebido_Click(object sender, RoutedEventArgs e)
        {
            var pedido = (sender as Button).DataContext as Pedido;
            if (pedido != null)
                _viewModel.AtualizarStatus(pedido.Id, "Recebido");
        }
    }
}
