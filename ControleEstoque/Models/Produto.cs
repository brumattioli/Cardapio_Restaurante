using ControleEstoque.DataAccess;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstoque.Models
{
    /// <summary>
    /// Classe que armazena os atributos dos objetos do tipo Produto
    /// </summary>
    public class Produto
    {
        public string Nome { get; set; }
        public int Codigo { get; set; }
        public double Preco { get; set; }
        public int Quantidade{ get; set; }
        public string Status { get; set; }

        public int checaProdutoExiste(int cod)
        {
            ProdutoDao dao = new ProdutoDao();
            return dao.GetCodigo(cod);
        }
        public string cadastraProduto(Produto p)
        {
            ProdutoDao dao = new ProdutoDao();
            return dao.Create(p);
        }
        public static Produto listarProdutoCodigo(int codigo)
        {
            ProdutoDao dao = new ProdutoDao();
            return dao.GetProduto_Codigo(codigo);
        }
        public static List<Produto> listarProdutoNome(string nome)
        {
            ProdutoDao dao = new ProdutoDao();
            return dao.GetProduto_Nome(nome);
        }
        public static List<Produto> listarProdutos()
        {
            ProdutoDao dao = new ProdutoDao();
            return dao.GetProdutos();
        }
        public static string removerProdutos(int codigo)
        {
            ProdutoDao dao = new ProdutoDao();
            return dao.Remover_Item(codigo);
        }
        public static bool editarQuantidade(int codigo, int quantidade)
        {
            ProdutoDao dao = new ProdutoDao();
            return dao.Update_Quantidade(codigo, quantidade);
        }
        public static bool editarPreco(int codigo, double preco)
        {
            ProdutoDao dao = new ProdutoDao();
            return dao.Update_Preco(codigo, preco);
        }
        public static bool editarNome(int codigo, string nome)
        {
            ProdutoDao dao = new ProdutoDao();
            return dao.Update_Nome(codigo, nome);
        }
    }
}