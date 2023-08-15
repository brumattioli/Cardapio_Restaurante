using ControleEstoque.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;

namespace ControleEstoque
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DalHelper.CriarBancoSQLite();
            DalHelper.CriarTabelaSQlite();
        }

        private void Cadastrar_Click(object sender, RoutedEventArgs e)
        {
            if (!Valida())
            {
                MessageBox.Show("Informe os dados do produto a incluir");
                return;
            }
            try
            {
                MessageBoxResult dr = MessageBox.Show("Deseja cadastrar as informações abaixo?" + "\n\r" + "Código: " + codigo.Text + "\n\r" + "Nome: " + nome.Text + "\n\r" + "Preço: " + preco.Text + "\n\r" + "Quantidade: " + quantidade.Text, "Confirmação", MessageBoxButton.OKCancel);
                switch (dr)
                {
                    case MessageBoxResult.OK:
                        Produto product = new Produto();
                        product.Id = Convert.ToInt32(codigo.Text);
                        product.Nome = nome.Text;
                        product.Preco = Convert.ToDouble(preco.Text);
                        product.Quantidade = Convert.ToInt32(quantidade.Text);
                        DalHelper.Add(product);
                        MessageBox.Show("Produto cadastrado com sucesso!");
                        emptyBoxesReg();
                        break;
                    case MessageBoxResult.Cancel:
                        MessageBox.Show("Produto não cadastrado, insira as informações novamente.");
                        emptyBoxesReg();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }

        }
        private void btnExibirDados_Click(object sender, EventArgs e)
        {
            ExibirDados();
        }

        private void ExibirDados()
        {
            try
            {
                List<Produto> l = DalHelper.GetProdutos();
                stockView.ItemsSource = l;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }

        private bool Valida()
        {
            if (string.IsNullOrEmpty(nome.Text) && string.IsNullOrEmpty(codigo.Text) && string.IsNullOrEmpty(preco.Text) && string.IsNullOrEmpty(quantidade.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void nome_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void codigo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void preco_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void quantidade_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        //LIMPAR CAMPOS DE CADASTRAR PRODUTO
        private void emptyBoxesReg()
        {
            try
            {
                nome.Text = "";
                codigo.Text = "";
                preco.Text = "";
                quantidade.Text = "";
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message.ToString());
            }
        }

        private void noFilter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                nomeBusca.Text = "";
                codigoBusca.Text = "";
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message.ToString());
            }
        }

        private void filterButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(codigoBusca.Text) && string.IsNullOrEmpty(nomeBusca.Text))
            {
                MessageBox.Show("Informe o código ou o nome do produto a ser Localizado");
                return;
            }
            try
            {
                if (string.IsNullOrEmpty(nomeBusca.Text))
                {
                    int Id = Convert.ToInt32(codigoBusca.Text);
                    List<Produto> l = DalHelper.GetProduto_Id(Id);
                    stockView.ItemsSource = l;
                }
                else
                {
                    string Nome = nomeBusca.Text;
                    List<Produto> l = DalHelper.GetProduto_Nome(Nome);
                    stockView.ItemsSource = l;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }

        private void stockView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void codigoBusca_TextChanged(object sender, RoutedEventArgs e)
        {

        }
        private void nomeBusca_TextChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
