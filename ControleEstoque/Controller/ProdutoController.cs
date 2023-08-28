using ControleEstoque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ControleEstoque.Controller
{
    public class ProdutoController
    {
        public string cadastrarProduto(int codigo, string nome, double preco, int quantidade)
        {
            string retorno = string.Empty;
            Produto p = new Produto();
            int checa = p.checaProdutoExiste(codigo);
            if (checa == 0)
            {
                p.Codigo = codigo;
                p.Nome = nome;
                p.Preco = preco;
                p.Quantidade = quantidade;
                retorno = p.cadastraProduto(p);
            }
            else
            {
                retorno = "Já existe um produto cadastrado com o código informado.";
            }
            return retorno;
        }
        public static List<Produto> listarProdutosCodigo(int codigo)
        {
            try
            {
                List<Produto> lista = new List<Produto>();

                lista.Add(Produto.listarProdutoCodigo(codigo));
                return lista;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<Produto> listarProdutosNome(string nome)
        {
            try
            {
                List<Produto> lista = new List<Produto>();
                return Produto.listarProdutoNome(nome);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<Produto> listarProdutos()
        {
            try
            {
                List<Produto> lista = new List<Produto>();
                return Produto.listarProdutos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string removerProdutos(int codigo)
        {
            try
            {
                return Produto.removerProdutos(codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string editarQuantidade(int codigo, int quantidade)
        {
            try
            {
                string retorno = string.Empty;
                bool status = false;
                status = Produto.editarQuantidade(codigo, quantidade);
                if (status)
                    return "A quantidade foi alterada com sucesso!\n";
                else
                    return "Alteração da quantidade não realizada.\n";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string editarPreco(int codigo, double preco)
        {
            try
            {
                string retorno = string.Empty;
                bool status = false;
                status = Produto.editarPreco(codigo, preco);
                if (status)
                    return "O preço foi alterado com sucesso!\n";
                else
                    return "Alteração do preço não realizada.\n";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string editarNome(int codigo, string nome)
        {
            try
            {
                string retorno = string.Empty;
                bool status = false;
                status = Produto.editarNome(codigo, nome);
                if (status)
                    return "O nome foi alterado com sucesso!\n";
                else
                    return "Alteração do nome não realizada.\n";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
} 