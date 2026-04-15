using System.Windows;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void BtnPessoas_Click(object sender, RoutedEventArgs e)
        {
            var pessoaView = new Views.PessoaView()
            {
                Owner = this // Define a janela atual como dona da nova janela
            };
            pessoaView.ShowDialog();
        }
        private void BtnProdutos_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funcionalidade de Produtos ainda não implementada.");
        }
        private void BtnPedidos_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funcionalidade de Pedidos ainda não implementada."); 
        }
    }
}
