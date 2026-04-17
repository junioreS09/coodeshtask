using System.Windows;
using System.Windows.Controls;
using WpfApp.Models;
using WpfApp.ViewModels;

namespace WpfApp.Views
{
    public partial class ProdutoView : Window
    {
        private ProdutoViewModel _viewModel;

        public ProdutoView()
        {
            InitializeComponent();
            _viewModel = new ProdutoViewModel();
            DataContext = _viewModel;
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Buscar();
        }

        private void BtnIncluir_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.LimparCampos();
            TxtValor.Text = string.Empty;
            _viewModel.ModoEdicao = false;
            _viewModel.FormularioHabilitado = true;
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.Valor == null || string.IsNullOrWhiteSpace(_viewModel.Nome) || string.IsNullOrWhiteSpace(_viewModel.Codigo))
            {
                MessageBox.Show("Todos os campos são obrigatórios. Por favor, preencha todos os campos.", "Erro de Validação", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_viewModel.ModoEdicao)
                _viewModel.Atualizar();
            else
                _viewModel.Salvar();

            TxtValor.Text = string.Empty;
            _viewModel.ModoEdicao = false;
            _viewModel.FormularioHabilitado = false;
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var produto = GridProdutos.SelectedItem as Produto;
            if (produto == null)
            {
                MessageBox.Show("Selecione um produto para editar.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            _viewModel.Id = produto.Id;
            _viewModel.Nome = produto.Nome;
            _viewModel.Codigo = produto.Codigo;
            _viewModel.Valor = produto.Valor;
            TxtValor.Text = produto.Valor.ToString();
            _viewModel.ModoEdicao = true;
            _viewModel.FormularioHabilitado = true;
        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e)
        {
            var produto = GridProdutos.SelectedItem as Produto;
            if (produto == null)
            {
                MessageBox.Show("Selecione um produto para excluir.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (MessageBox.Show("Tem certeza que deseja excluir este produto?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                _viewModel.Excluir(produto.Id);
        }

        private void TxtValor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (decimal.TryParse(TxtValor.Text, out decimal valor))
                _viewModel.Valor = valor;
            else
                _viewModel.Valor = null;
        }

        private void TxtValorMin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (decimal.TryParse(TxtValorMin.Text, out decimal valor))
                _viewModel.ValorMinFiltro = valor;
            else
                _viewModel.ValorMinFiltro = null;
        }

        private void TxtValorMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (decimal.TryParse(TxtValorMax.Text, out decimal valor))
                _viewModel.ValorMaxFiltro = valor;
            else
                _viewModel.ValorMaxFiltro = null;
        }
    }
}
