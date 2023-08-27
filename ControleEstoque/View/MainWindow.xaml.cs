using ControleEstoque.Models;
using ControleEstoque.ViewModel;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ControleEstoque
{
    /// <summary>
    /// Lógica de interação com a MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        ViewModel.ProdutoViewModel ProdutoVM;

        public MainWindow()
        {
            InitializeComponent();
            DalHelper.CriarBancoSQLite();
            DalHelper.CriarTabelaSQlite();
            ProdutoVM = new ViewModel.ProdutoViewModel();
            DataContext = ProdutoVM;
        }

        
       private void NumberValidation(object sender, TextCompositionEventArgs e)
       {
            ProdutoVM.NumberValidation(sender, e);
       }

      

        private void TextValidation(object sender, TextCompositionEventArgs e)
        {
            ProdutoVM.TextValidation(sender, e);
        }

        private void DoubleValidation(object sender, TextCompositionEventArgs e)
        {
            ProdutoVM.DoubleValidation(sender, e);
        }

        private void Cadastrar_Click(object sender, RoutedEventArgs e)
        {
            ProdutoVM.CadastrarProduto_Click(codigo_Cadastrar.Text, nome_Cadastrar.Text, preco_Cadastrar.Text, quantidade_Cadastrar.Text);
        }

        private void btnExibirDados_Click(object sender, EventArgs e)
        {
            ProdutoVM.BtnExibirDados_Click();
        }

        private bool Valida()
        {
            if(ProdutoVM.Valida(nome_Cadastrar.Text, codigo_Cadastrar.Text, preco_Cadastrar.Text, quantidade_Cadastrar.Text))
            {
                return true;
            }
            else
            {
                return false;
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

        private void emptyBoxesReg()
        {
            ProdutoVM.emptyBoxesReg(nome_Cadastrar.Text, codigo_Cadastrar.Text, preco_Cadastrar.Text, quantidade_Cadastrar.Text);
        }

        private void noFilter_Buscar_Click(object sender, RoutedEventArgs e)
        {
            string nome = nomeBusca_Buscar.Text;
            string codigo = codigoBusca_Buscar.Text;
            ProdutoVM.noFilter_Buscar_Click(ref nome, ref codigo);
            nomeBusca_Buscar.Text = nome;
            codigoBusca_Buscar.Text = codigo;  
        }

        private void noFilter_Editar_Click(object sender, RoutedEventArgs e)
        {
            ProdutoVM.noFilter_Editar_Click(nomeBusca_Editar.Text, codigoBusca_Editar.Text);
        }

        private void noFilter_Remover_Click(object sender, RoutedEventArgs e)
        {
            ProdutoVM.noFilter_Remover_Click(nomeBusca_Remover.Text, codigoBusca_Remover.Text);
        }

        private void noFilter_Simular_Venda_Click(object sender, RoutedEventArgs e)
        {
            ProdutoVM.noFilter_Simular_Venda_Click(nomeBusca_Simular_Venda.Text, codigoBusca_Simular_Venda.Text, stockView_Buscar.ItemsSource);
        }

        private void filterButton_Buscar_Click(object sender, EventArgs e)
        {
            ProdutoVM.filterButton_Buscar_Click(nomeBusca_Buscar.Text, codigoBusca_Buscar.Text, stockView_Buscar.ItemsSource);
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

        private void filterButton_Editar_Click(object sender, RoutedEventArgs e)
        {
            ProdutoVM.filterButton_Editar_Click(nomeBusca_Editar.Text, codigoBusca_Editar.Text, stockView_Editar.ItemsSource);
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            ProdutoVM.Editar_Click(codigo_Editar.Text, nome_Editar.Text, preco_Editar.Text, quantidade_Editar.Text, stockView_Editar.ItemsSource, (System.Collections.IEnumerable)stockView_Editar.SelectedItem);
        }

        private void Remover_Click(object sender, RoutedEventArgs e)
        {
            ProdutoVM.Remover_Click((System.Collections.IEnumerable)stockView_Remover.SelectedItem);
        }

        private void filterButton_Remover_Click(object sender, RoutedEventArgs e)
        {
            ProdutoVM.filterButton_Remover_Click(nomeBusca_Remover.Text, codigoBusca_Remover.Text, stockView_Remover.ItemsSource);
        }

        private void simularVenda_Click(object sender, RoutedEventArgs e)
        {
            ProdutoVM.simularVenda_Click(codigo_Simular_Venda.Text, nome_Simular_Venda.Text, preco_Simular_Venda.Text, quantidade_Simular_Venda.Text, stockView_Simular_Venda.ItemsSource, (System.Collections.IEnumerable)stockView_Simular_Venda.SelectedItem);
        }
        private void getItem(object sender, MouseButtonEventArgs e)
        {
            ProdutoVM.getItem(codigo_Simular_Venda.Text, nome_Simular_Venda.Text,preco_Simular_Venda.Text, quantidade_Simular_Venda.Text, (System.Collections.IEnumerable)stockView_Simular_Venda.SelectedItem);
        }

        private void filterButton_Simular_Venda_Click(object sender, RoutedEventArgs e)
        {
            ProdutoVM.filterButton_Simular_Venda_Click(codigoBusca_Simular_Venda.Text, nomeBusca_Simular_Venda.Text, codigo_Simular_Venda.Text, nome_Simular_Venda.Text, preco_Simular_Venda.Text, quantidade_Simular_Venda.Text, stockView_Simular_Venda.ItemsSource, (System.Collections.IEnumerable)stockView_Simular_Venda.SelectedItem);
        }
        private void filterButton_Buscar_Vendas_Click(object sender, RoutedEventArgs e)
        {
            ProdutoVM.filterButton_Buscar_Vendas_Click(codigoBusca_Buscar_Vendas.Text, codigoBusca_Buscar_Vendas_Produto.Text, nomeBusca_Buscar_Vendas.Text, stockView_Buscar_Vendas.ItemsSource, (System.Collections.IEnumerable)quantidade_Registros.Content);
        }
        private void noFilter_Buscar_Vendas_Click(object sender, RoutedEventArgs e)
        {
            ProdutoVM.noFilter_Buscar_Vendas_Click(codigoBusca_Buscar_Vendas.Text, codigoBusca_Buscar_Vendas_Produto.Text, nomeBusca_Buscar_Vendas.Text, stockView_Buscar_Vendas.ItemsSource);
        }
        private void getItem_Editar(object sender, MouseButtonEventArgs e)
        {
            ProdutoVM.getItem_Editar(codigo_Editar.Text, (System.Collections.IEnumerable)stockView_Editar.SelectedItem);
        }
    }
}