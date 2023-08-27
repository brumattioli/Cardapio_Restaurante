using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ControleEstoque.Models;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.Data;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ControleEstoque.ViewModel
{
    public class ProdutoViewModel : BaseViewModel
    {
        private string _nome;
        private int _codigo;
        private double _preco;
        private int _quantidade;
        private string _status;
        private ObservableCollection<Produto> _produtos = new ObservableCollection<Produto>();
        private int _codigoVenda;
        private ProdutoViewModel _produto;
        private string _data;
        private int _quantidadeVenda;
        private double _precoVenda;

        public int CodigoVenda
        {
            get { return _codigoVenda; }
            set
            {
                if (_codigo != value)
                {
                    _codigo = value;
                    NotifyPropertyChanged(nameof(CodigoVenda));
                }
            }
        }

        public ProdutoViewModel Produto
        {
            get { return _produto; }
            set
            {
                if (_produto != value)
                {
                    _produto = value;
                    NotifyPropertyChanged(nameof(Produto));
                }
            }
        }

        public string Data
        {
            get { return _data; }
            set
            {
                if (_data != value)
                {
                    _data = value;
                    NotifyPropertyChanged(nameof(Data));
                }
            }
        }

        public int QuantidadeVenda
        {
            get { return _quantidadeVenda; }
            set
            {
                if (_quantidadeVenda != value)
                {
                    _quantidadeVenda = value;
                    NotifyPropertyChanged(nameof(QuantidadeVenda));
                }
            }
        }

        public double PrecoVenda
        {
            get { return _precoVenda; }
            set
            {
                if (_precoVenda != value)
                {
                    _precoVenda = value;
                    NotifyPropertyChanged(nameof(PrecoVenda));
                }
            }
        }

        public string Nome
        {
            get { return _nome; }
            set
            {
                if (_nome != value)
                {
                    _nome = value;
                    NotifyPropertyChanged(nameof(Nome));
                }
            }
        }

        public int Codigo
        {
            get { return _codigo; }
            set
            {
                if (_codigo != value)
                {
                    _codigo = value;
                    NotifyPropertyChanged(nameof(Codigo));
                }
            }
        }

        public double Preco
        {
            get { return _preco; }
            set
            {
                if (_preco != value)
                {
                    _preco = value;
                    NotifyPropertyChanged(nameof(Preco));
                }
            }
        }

        public int Quantidade
        {
            get { return _quantidade; }
            set
            {
                if (_quantidade != value)
                {
                    _quantidade = value;
                    NotifyPropertyChanged(nameof(Quantidade));
                }
            }
        }

        public string Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    NotifyPropertyChanged(nameof(Status));
                }
            }
        }

        public ObservableCollection<Produto> Produtos
        {
            get { return _produtos; }
            set
            {
                if (_produtos != value)
                {
                    _produtos = value;
                    NotifyPropertyChanged(nameof(Produtos));
                }
            }
        }
        

        /// <summary>
        /// Valida��o de n�meros nos campos de entrada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex r = new Regex("[^0-9]+");
            e.Handled = r.IsMatch(e.Text);
        }
        /// <summary>
        /// Valida��o de textos nos campos de entrada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TextValidation(object sender, TextCompositionEventArgs e)
        {
            Regex r = new Regex("[^a-zA-Z��������������������]");
            e.Handled = r.IsMatch(e.Text);
        }
        /// <summary>
        /// Valida��o de double nos campos de entrada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DoubleValidation(object sender, TextCompositionEventArgs e)
        {
            Regex r = new Regex("[^0-9.,]+");
            e.Handled = r.IsMatch(e.Text);
        }
        /// <summary>
        /// Bot�o que faz intera��o com o Banco de dados e realiza o cadastro dos produtos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CadastrarProduto_Click(string codigo_Cadastrar, string nome_Cadastrar, string preco_Cadastrar, string quantidade_Cadastrar)
        {
            if (!Valida(nome_Cadastrar, codigo_Cadastrar, preco_Cadastrar, quantidade_Cadastrar))
            {
                MessageBox.Show("Informe os dados do produto a incluir");
                return;
            }
            try
            {
                MessageBoxResult dr = MessageBox.Show("Deseja cadastrar as informações abaixo?" + "\n\r" + "Código: " + codigo_Cadastrar + "\n\r" + "Nome: " + nome_Cadastrar + "\n\r" + "Preço: " + preco_Cadastrar + "\n\r" + "Quantidade: " + quantidade_Cadastrar, "Confirmação", MessageBoxButton.OKCancel);
                switch (dr)
                {
                    case MessageBoxResult.OK:
                        Produto produtoBD = new Produto();
                        produtoBD.Codigo = Convert.ToInt32(codigo_Cadastrar);
                        Produto p = DalHelper.GetProduto_Codigo(produtoBD.Codigo);
                        if (p.Codigo == 0)
                        {
                            p = new Produto();
                            p.Codigo = Convert.ToInt32(codigo_Cadastrar);
                            p.Nome = nome_Cadastrar;
                            p.Preco = Convert.ToDouble(preco_Cadastrar);
                            p.Quantidade = Convert.ToInt32(quantidade_Cadastrar);
                            DalHelper.Add(p);
                            MessageBox.Show("Produto cadastrado com sucesso!");
                            emptyBoxesReg(nome_Cadastrar, codigo_Cadastrar, preco_Cadastrar, quantidade_Cadastrar);
                            break;
                        }
                        else MessageBox.Show("C�digo já existente no Banco de Dados!");
                        break;
                    case MessageBoxResult.Cancel:
                        MessageBox.Show("Produto não cadastrado, insira as informações novamente.");
                        emptyBoxesReg(nome_Cadastrar, codigo_Cadastrar, preco_Cadastrar, quantidade_Cadastrar);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }

        }
        /// <summary>
        /// Bot�o que faz intera��o com o banco de dados e exibe os dados dos produtos na tela
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BtnExibirDados_Click()
        {
            try
            {
                List<Produto> values = DalHelper.GetProdutos();
                Produtos = new ObservableCollection<Produto>(values);
                Debug.WriteLine("Produtos: " + Produtos.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }

        /// <summary>
        /// Classe que valida se todos os dados do cadastro foram preenchidos
        /// </summary>
        /// <returns></returns>
        public bool Valida(string nome_Cadastrar, string codigo_Cadastrar, string preco_Cadastrar, string quantidade_Cadastrar)
        {
            if (string.IsNullOrEmpty(nome_Cadastrar) && string.IsNullOrEmpty(codigo_Cadastrar) && string.IsNullOrEmpty(preco_Cadastrar) && string.IsNullOrEmpty(quantidade_Cadastrar))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Classe que limpa os campos de cadastrar produtos quando o usu�rio solicita que o produto n�o seja cadastrado
        /// </summary>
        public void emptyBoxesReg(string nome_Cadastrar, string codigo_Cadastrar, string preco_Cadastrar, string quantidade_Cadastrar)
        {
            try
            {
                nome_Cadastrar = "";
                codigo_Cadastrar = "";
                preco_Cadastrar = "";
                quantidade_Cadastrar = "";
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message.ToString());
            }
        }

        /// <summary>
        /// Bot�o que limpa os campos de filtro da tela Buscar Produtos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void noFilter_Buscar_Click(ref string nomeBusca_Buscar, ref string codigoBusca_Buscar)
        {
            try
            {
                nomeBusca_Buscar = "";
                codigoBusca_Buscar = "";
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message.ToString());
            }
        }
        /// <summary>
        /// Bot�o que limpa os campos de filtro da tela Editar Produtos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void noFilter_Editar_Click(string NomeBusca, string CodigoBusca)
        {
            try
            {
                NomeBusca = "";
                CodigoBusca = "";
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message.ToString());
            }
        }

        /// <summary>
        /// Bot�o que limpa os campos de filtro da tela Remover Produtos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void noFilter_Remover_Click(string nomeBusca_Remover, string codigoBusca_Remover)
        {
            try
            {
                nomeBusca_Remover = "";
                codigoBusca_Remover = "";
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message.ToString());
            }
        }
        /// <summary>
        /// Bot�o que limpa os campos de filtro da tela Simular Venda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void noFilter_Simular_Venda_Click(string nomeBusca_Simular_Venda, string codigoBusca_Simular_Venda, IEnumerable stockView_Simular_Venda)
        {
            try
            {
                nomeBusca_Simular_Venda = "";
                codigoBusca_Simular_Venda = "";
                stockView_Simular_Venda = "";
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message.ToString());
            }
        }

        /// <summary>
        /// Bot�o que filtra os Produtos da tela Buscar Produtos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void filterButton_Buscar_Click(string nomeBusca_Buscar, string codigoBusca_Buscar, IEnumerable stockView_Buscar_ItemsSource)
        {
            if (string.IsNullOrEmpty(codigoBusca_Buscar) && string.IsNullOrEmpty(nomeBusca_Buscar))
            {
                MessageBox.Show("Informe o código ou o nome do produto a ser localizado");
                return;
            }
            try
            {
                if (!string.IsNullOrEmpty(codigoBusca_Buscar))
                {
                    int Codigo = Convert.ToInt32(codigoBusca_Buscar);
                    List<Produto> l = new List<Produto>();
                    l.Add(DalHelper.GetProduto_Codigo(Codigo));
                    stockView_Buscar_ItemsSource = l;
                }
                else
                {
                    string Nome = nomeBusca_Buscar;
                    List<Produto> l = DalHelper.GetProduto_Nome(Nome);
                    stockView_Buscar_ItemsSource = l;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }

        /// <summary>
        /// Bot�o que filtra os Produtos da tela Editar Produtos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void filterButton_Editar_Click(string nomeBusca_Editar, string codigoBusca_Editar, IEnumerable stockView_Editar_ItemsSource)
        {
            if (string.IsNullOrEmpty(codigoBusca_Editar) && string.IsNullOrEmpty(nomeBusca_Editar))
            {
                MessageBox.Show("Informe o c�digo ou o nome do produto a ser editado");
                return;
            }
            try
            {
                if (!string.IsNullOrEmpty(codigoBusca_Editar))
                {
                    int Codigo = Convert.ToInt32(codigoBusca_Editar);
                    List<Produto> l = new List<Produto>();
                    l.Add(DalHelper.GetProduto_Codigo(Codigo));
                    stockView_Editar_ItemsSource = l;
                }
                else
                {
                    string Nome = nomeBusca_Editar;
                    List<Produto> l = DalHelper.GetProduto_Nome(Nome);
                    stockView_Editar_ItemsSource = l;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }

        //private void Selecionar_Item(object sender, RoutedEventArgs e)
        //{
        //    Produto produtoAnterior = (Produto)stockView_Editar.SelectedItem;

        //}

        /// <summary>
        /// Bot�o que faz a edi��o/atualiza��o de dados no Banco de Dados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Editar_Click(string codigo_Editar, string nome_Editar, string preco_Editar, string quantidade_Editar, IEnumerable stockView_Editar_ItemsSource, IEnumerable stockView_Editar_SelectedItem)
        {
            try
            {
                if (stockView_Editar_ItemsSource == null)
                {
                    MessageBox.Show("� necess�rio selecionar uma linha para editar.");
                    codigo_Editar = "";
                }
                else
                {
                    if (string.IsNullOrEmpty(quantidade_Editar) && string.IsNullOrEmpty(nome_Editar) && string.IsNullOrEmpty(preco_Editar))
                    {
                        MessageBox.Show("Para editar um produto, voc� precisa informar um novo nome, quantidade ou pre�o para o item.");
                        codigo_Editar = "";
                    }
                    Produto produtoAnterior = (Produto)stockView_Editar_SelectedItem;
                    Produto produto = new Produto();

                    if (quantidade_Editar != "")
                    {
                        produto.Codigo = Convert.ToInt32(produtoAnterior.Codigo);
                        produto.Quantidade = Convert.ToInt32(quantidade_Editar);
                        DalHelper.Update_Quantidade(produto, produtoAnterior);
                        MessageBox.Show("A quantidade do produto foi atualizada!");
                    }
                    if (nome_Editar != "")
                    {
                        produto.Codigo = Convert.ToInt32(produtoAnterior.Codigo);
                        produto.Nome = nome_Editar;
                        DalHelper.Update_Nome(produto, produtoAnterior);
                        MessageBox.Show("O nome do produto foi atualizado!");
                    }
                    if (preco_Editar != "")
                    {
                        produto.Codigo = Convert.ToInt32(produtoAnterior.Codigo);
                        produto.Preco = Convert.ToDouble(preco_Editar);
                        DalHelper.Update_Preco(produto, produtoAnterior);
                        MessageBox.Show("O pre�o do produto foi atualizado!");
                    }
                    filterButton_Editar_Click(nome_Editar, codigo_Editar, stockView_Editar_ItemsSource);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }
        /// <summary>
        /// Bot�o que remove Produtos no Banco de Dados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Remover_Click(IEnumerable stockView_Remover_SelectedItem)
        {
            try
            {
                if (stockView_Remover_SelectedItem == null)
                {
                    MessageBox.Show("� necess�rio selecionar uma linha para remover.");
                }
                else
                {
                    Produto produto = (Produto)stockView_Remover_SelectedItem;
                    produto.Codigo = Convert.ToInt32(produto.Codigo);
                    DalHelper.Remover_Item(produto);
                    MessageBox.Show("O produto de c�digo " + produto.Codigo + " foi removido com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }

        /// <summary>
        /// Bot�o que limpa os campos de filtro da tela Remover Produtos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void filterButton_Remover_Click(string nomeBusca_Remover, string codigoBusca_Remover, IEnumerable stockView_Remover_ItemsSource)
        {
            if (string.IsNullOrEmpty(codigoBusca_Remover) && string.IsNullOrEmpty(nomeBusca_Remover))
            {
                MessageBox.Show("Informe o c�digo ou o nome do produto a ser removido");
                return;
            }
            try
            {
                if (!string.IsNullOrEmpty(codigoBusca_Remover))
                {
                    int Codigo = Convert.ToInt32(codigoBusca_Remover);
                    List<Produto> l = new List<Produto>();
                    l.Add(DalHelper.GetProduto_Codigo(Codigo));
                    stockView_Remover_ItemsSource = l;
                }
                else
                {
                    string Nome = nomeBusca_Remover;
                    List<Produto> l = DalHelper.GetProduto_Nome(Nome);
                    stockView_Remover_ItemsSource = l;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }

        /// <summary>
        /// Bot�o que simula a Venda de produtos realizando a intera��o com o Banco de Dados para buscar a 
        /// quantidade existente daquele produto e chama a fun��o Update do DalHelper para atualizar a quantidade no Banco de Dados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void simularVenda_Click(string codigo_Simular_Venda, string nome_Simular_Venda, string preco_Simular_Venda, string quantidade_Simular_Venda, IEnumerable stockView_Simular_Venda_ItemsSource, IEnumerable stockView_Simular_Venda_SelectedItem)
        {
            try
            {
                Produto produtoAnterior = (Produto)stockView_Simular_Venda_SelectedItem;
                Produto produto = DalHelper.GetProduto_Codigo(Convert.ToInt32(codigo_Simular_Venda));
                Produto? p = stockView_Simular_Venda_SelectedItem as Produto;
                if (p.Quantidade == 0)
                {
                    MessageBox.Show("O produto n�o possui estoque para realizar a venda.");
                }
                else
                {
                    MessageBoxResult dr = MessageBox.Show("Deseja realizar a venda conforme as informa��es abaixo?" + "\n\r" + "C�digo: " + codigo_Simular_Venda + "\n\r" + "Nome: " + nome_Simular_Venda + "\n\r" + "Pre�o: " + preco_Simular_Venda + "\n\r" + "Quantidade: " + quantidade_Simular_Venda, "Confirma��o", MessageBoxButton.OKCancel);
                    switch (dr)
                    {
                        case MessageBoxResult.OK:
                            if (quantidade_Simular_Venda == "")
                            {
                                MessageBox.Show("Digite a quantidade para realizar a venda.");
                            }
                            else if (quantidade_Simular_Venda != "")
                            {
                                int quantidade = produto.Quantidade - Convert.ToInt32(quantidade_Simular_Venda);
                                if (quantidade >= 0)
                                {
                                    produto.Codigo = Convert.ToInt32(produtoAnterior.Codigo);
                                    produto.Quantidade = produto.Quantidade - Convert.ToInt32(quantidade_Simular_Venda);
                                    DalHelper.Update_Quantidade(produto, produtoAnterior);
                                    string cd = DalHelper.Add_Venda(produto);
                                    MessageBox.Show("A venda foi realizada! Venda c�digo " + cd + "!");
                                    codigo_Simular_Venda = "";
                                    nome_Simular_Venda = "";
                                    preco_Simular_Venda = "";
                                    quantidade_Simular_Venda = "";
                                    stockView_Simular_Venda_ItemsSource = "";
                                }
                                else
                                {
                                    MessageBox.Show("Venda n�o realizada, a quantidade informada � superior � dispon�vel em estoque.");
                                }
                            }
                            break;
                        case MessageBoxResult.Cancel:
                            MessageBox.Show("Venda n�o realizada, insira as informa��es novamente.");
                            codigo_Simular_Venda = "";
                            nome_Simular_Venda = "";
                            preco_Simular_Venda = "";
                            quantidade_Simular_Venda = "";
                            stockView_Simular_Venda_ItemsSource = "";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }
        public void getItem(string codigo_Simular_Venda, string nome_Simular_Venda, string preco_Simular_Venda, string quantidade_Simular_Venda, IEnumerable stockView_Simular_Venda_SelectedItem)
        {
            Produto? p = stockView_Simular_Venda_SelectedItem as Produto;
            codigo_Simular_Venda = p.Codigo.ToString();
            nome_Simular_Venda = p.Nome.ToString();
            preco_Simular_Venda = p.Preco.ToString();
        }

        /// <summary>
        /// Bot�o que filtra os Produtos da tela Simular Venda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void filterButton_Simular_Venda_Click(string codigoBusca_Simular_Venda, string nomeBusca_Simular_Venda, string codigo_Simular_Venda, string nome_Simular_Venda, string preco_Simular_Venda, string quantidade_Simular_Venda, IEnumerable stockView_Simular_Venda_ItemsSource, IEnumerable stockView_Simular_Venda_SelectedItem)
        {
            if (string.IsNullOrEmpty(codigoBusca_Simular_Venda) && string.IsNullOrEmpty(nomeBusca_Simular_Venda))
            {
                MessageBox.Show("Informe o c�digo ou o nome do produto a ser vendido");
                return;
            }
            try
            {
                codigo_Simular_Venda = "";
                nome_Simular_Venda = "";
                preco_Simular_Venda = "";
                quantidade_Simular_Venda = "";

                if (!string.IsNullOrEmpty(codigoBusca_Simular_Venda))
                {
                    int Codigo = Convert.ToInt32(codigoBusca_Simular_Venda);
                    List<Produto> l = new List<Produto>();
                    l.Add(DalHelper.GetProduto_Codigo(Codigo));
                    stockView_Simular_Venda_ItemsSource = l;
                }
                else
                {
                    string Nome = nomeBusca_Simular_Venda;
                    List<Produto> l = DalHelper.GetProduto_Nome(Nome);
                    stockView_Simular_Venda_ItemsSource = l;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }
        public void filterButton_Buscar_Vendas_Click(string codigoBusca_Buscar_Vendas, string codigoBusca_Buscar_Vendas_Produto, string nomeBusca_Buscar_Vendas, IEnumerable stockView_Buscar_Vendas_ItemsSource, IEnumerable quantidade_Registros_Content)
        {
            if (string.IsNullOrEmpty(codigoBusca_Buscar_Vendas) && string.IsNullOrEmpty(codigoBusca_Buscar_Vendas_Produto) && string.IsNullOrEmpty(nomeBusca_Buscar_Vendas))
            {
                MessageBox.Show("Informe o c�digo da venda, c�digo do produto ou o nome do produto a ser buscado");
                return;
            }
            try
            {
                if (!string.IsNullOrEmpty(codigoBusca_Buscar_Vendas))
                {
                    int cod = Convert.ToInt32(codigoBusca_Buscar_Vendas);
                    List<Venda> vendas = new List<Venda>();
                    vendas.Add(DalHelper.GetVenda_Codigo(cod));
                    stockView_Buscar_Vendas_ItemsSource = vendas;
                }
                else
                {
                    if (!string.IsNullOrEmpty(codigoBusca_Buscar_Vendas_Produto))
                    {
                        int cod = Convert.ToInt32(codigoBusca_Buscar_Vendas_Produto);
                        List<Venda> vendas = new List<Venda>();
                        vendas.Add(DalHelper.GetVenda_Codigo_Produto(cod));
                        stockView_Buscar_Vendas_ItemsSource = vendas;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(nomeBusca_Buscar_Vendas))
                        {
                            string nome = nomeBusca_Buscar_Vendas;
                            List<Venda> vendas = new List<Venda>();
                            vendas = DalHelper.GetVenda_Nome_Produto(nome);
                            stockView_Buscar_Vendas_ItemsSource = vendas;
                            quantidade_Registros_Content = ("Quantidade de Vendas: " + vendas.Count.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }
        public void noFilter_Buscar_Vendas_Click(string codigoBusca_Buscar_Vendas, string codigoBusca_Buscar_Vendas_Produto, string nomeBusca_Buscar_Vendas, IEnumerable stockView_Buscar_Vendas_ItemsSource)
        {
            codigoBusca_Buscar_Vendas = "";
            codigoBusca_Buscar_Vendas_Produto = "";
            nomeBusca_Buscar_Vendas = "";
            stockView_Buscar_Vendas_ItemsSource = "";
        }
        public void getItem_Editar(string codigo_Editar, IEnumerable stockView_Editar_SelectedItem)
        {
            Produto? p = stockView_Editar_SelectedItem as Produto;
            codigo_Editar = p.Codigo.ToString();
        }
    }
    
}

