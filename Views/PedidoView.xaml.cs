using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Models;
using WpfApp.ViewModels;

namespace WpfApp.Views
{
    public partial class PedidoView : Window
    {
        private PedidoViewModel _viewModel;

        public PedidoView(int idPessoa = 0)
        {
            InitializeComponent();
            _viewModel = new PedidoViewModel(idPessoa);
            DataContext = _viewModel;
        }

        private void BtnAdicionarItem_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.ProdutoSelecionado == null)
            {
                MessageBox.Show("Selecione um produto.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (_viewModel.Quantidade <= 0)
            {
                MessageBox.Show("A quantidade deve ser maior que zero.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            _viewModel.AdicionarItem();
        }

        private void BtnRemoverItem_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as Button).DataContext as PedidoItem;
            _viewModel.RemoverItem(item);
        }

        private void CmbPagamento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = CmbPagamento.SelectedItem as ComboBoxItem;
            if (item != null)
                _viewModel.AtualizarParcelas(item.Content.ToString());
        }

        private void BtnFinalizar_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.PessoaSelecionada == null)
            {
                MessageBox.Show("Selecione uma pessoa.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (_viewModel.Itens.Count == 0)
            {
                MessageBox.Show("Adicione pelo menos um produto.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(_viewModel.FormaPagamento))
            {
                MessageBox.Show("Selecione a forma de pagamento.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool sucesso = _viewModel.Finalizar();
            if (sucesso)
            {
                MessageBox.Show("Pedido finalizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else if (_viewModel.VisibilidadeParcelas == System.Windows.Visibility.Visible)
            {
                MessageBox.Show("Selecione o número de parcelas.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!_viewModel.Finalizado && _viewModel.Itens.Count > 0)
            {
                var resultado = MessageBox.Show(
                    "O pedido não foi finalizado e será descartado. Deseja sair mesmo assim?",
                    "Atenção", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.No)
                    e.Cancel = true;
            }
        }
    }
}
