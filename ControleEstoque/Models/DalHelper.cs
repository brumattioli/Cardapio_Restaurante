using Microsoft.Data.Sqlite;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Documents;

namespace ControleEstoque.Models
{
    public class DalHelper
    {
        private static SQLiteConnection sqliteConnection;

        public DalHelper()
        { }

        /// <summary>
        /// Classe que realiza a conexão com o Banco de Dados
        /// </summary>
        /// <returns></returns>
        private static SQLiteConnection DbConnection()
        {
            string baseConnectionString = "Data Source=db_controle_estoque";
            var connectionString = new SqliteConnectionStringBuilder(baseConnectionString)
            {
                Mode = SqliteOpenMode.ReadWriteCreate,
            }.ToString();

            sqliteConnection = new SQLiteConnection(connectionString);
            sqliteConnection.Open();
            return sqliteConnection;
        }

        /// <summary>
        /// Classe que cria o Banco de Dados no SQLite
        /// </summary>
        public static void CriarBancoSQLite()
        {
            try
            {
                string dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "db_controle_estoque");
                bool exists = File.Exists(dbPath);
                if (!exists)
                    SQLiteConnection.CreateFile(dbPath);
                //SQLiteConnection.CreateFile(@"C:\Users\bruma\Downloads\db_controle_estoque");
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Classe que cria a tabela Produtos no SQLite caso esta não exista
        /// </summary>
        public static void CriarTabelaSQlite()
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    //cmd.CommandText = "DROP TABLE Produtos";
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Produtos(Codigo int UNIQUE, Nome Varchar(50), Preco double, Quantidade int, Status Varchar(7))";
                    cmd.ExecuteNonQuery();
                }
                using (var cmd = DbConnection().CreateCommand())
                {
                    //cmd.CommandText = "DROP TABLE Vendas";
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Vendas(Codigo_Venda INTEGER PRIMARY KEY AUTOINCREMENT, Codigo_Produto int, Preco_Venda double, Quantidade_Venda int, Data_Venda Varchar(19))";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Classe que insere produtos no Banco de Dados
        /// </summary>
        /// <param name="product"></param>
        public static void Add(Produto product)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Produtos(Codigo, Nome, Preco, Quantidade, Status) values (@Codigo, @Nome, @Preco, @Quantidade, @Status)";
                    cmd.Parameters.AddWithValue("@Codigo", product.Codigo);
                    cmd.Parameters.AddWithValue("@Nome", product.Nome);
                    cmd.Parameters.AddWithValue("@Preco", product.Preco);
                    cmd.Parameters.AddWithValue("@Quantidade", product.Quantidade);
                    cmd.Parameters.AddWithValue("@Status", "Ativo");
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// Classe que retorna todos os produtos cadastrados no Banco de Dados
        /// </summary>
        /// <returns></returns>
        public static List<Produto> GetProdutos()
        {
            try
            {
                List<Produto> p = new List<Produto>();
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Produtos";
                    using SQLiteDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Produto op = new Produto();
                        op.Codigo = rdr.GetInt32(0);
                        op.Nome = rdr.GetString(1);
                        op.Preco = rdr.GetDouble(2);
                        op.Quantidade = rdr.GetInt32(3);
                        op.Status = rdr.GetString(4);
                        p.Add(op);
                    }
                    return p;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Classe que retorna os produtos no Banco de Dados a partir do nome do produto
        /// </summary>
        /// <param name="Nome"></param>
        /// <returns></returns>
        public static List<Produto> GetProduto_Nome(string Nome)
        {
            try
            {
                List<Produto> p = new List<Produto>();
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Produtos Where Nome LIKE '%"+ Nome +"%'";
                    using SQLiteDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Produto op = new Produto();
                        op.Codigo = rdr.GetInt32(0);
                        op.Nome = rdr.GetString(1);
                        op.Preco = rdr.GetDouble(2);
                        op.Quantidade = rdr.GetInt32(3);
                        op.Status = rdr.GetString(4);
                        p.Add(op);
                    }
                    return p;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Classe que retorna os produtos no Banco de Dados a partir do código do produto
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public static Produto GetProduto_Codigo(int Codigo)
        {
            try
            {
                Produto p = new Produto();
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Produtos Where Codigo =" + Codigo;
                    using SQLiteDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        p.Codigo = rdr.GetInt32(0);
                        p.Nome = rdr.GetString(1);
                        p.Preco = rdr.GetDouble(2);
                        p.Quantidade = rdr.GetInt32(3);
                        p.Status = rdr.GetString(4);
                    }
                    return p;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Classe que atualiza a quantidade de produtos no Banco de Dados a partir do Código do produto
        /// </summary>
        /// <param name="produto"></param>
        /// <param name="produtoAnterior"></param>
        public static void Update_Quantidade(Produto produto, Produto produtoAnterior)
        {
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    
                    if (produto.Codigo != null && produto.Quantidade != produtoAnterior.Quantidade)
                    {
                        cmd.CommandText = "UPDATE Produtos SET Quantidade=@Quantidade WHERE Codigo=@Codigo";
                        cmd.Parameters.AddWithValue("@Codigo", produto.Codigo);
                        cmd.Parameters.AddWithValue("@Quantidade", produto.Quantidade);
                        cmd.ExecuteNonQuery();
                    }
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Classe que atualiza o preço de produtos no Banco de Dados a partir do Código do produto
        /// </summary>
        /// <param name="produto"></param>
        /// <param name="produtoAnterior"></param>
        public static void Update_Preco(Produto produto, Produto produtoAnterior)
        {
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {

                    if (produto.Codigo != null && produto.Preco != produtoAnterior.Preco)
                    {
                        cmd.CommandText = "UPDATE Produtos SET Preco=@Preco WHERE Codigo=@Codigo";
                        cmd.Parameters.AddWithValue("@Codigo", produto.Codigo);
                        cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                        cmd.ExecuteNonQuery();
                    }
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Classe que atualiza o nome de produtos no Banco de Dados a partir do Código do produto
        /// </summary>
        /// <param name="produto"></param>
        /// <param name="produtoAnterior"></param>
        public static void Update_Nome(Produto produto, Produto produtoAnterior)
        {
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {

                    if (produto.Codigo != null && produto.Nome != produtoAnterior.Nome)
                    {
                        cmd.CommandText = "UPDATE Produtos SET Nome=@Nome WHERE Codigo=@Codigo";
                        cmd.Parameters.AddWithValue("@Codigo", produto.Codigo);
                        cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                        cmd.ExecuteNonQuery();
                    }
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Classe que remove produtos no Banco de Dados a partir do Código do produto
        /// </summary>
        /// <param name="produto"></param>
        public static void Remover_Item(Produto produto)
        {
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {

                    if (produto.Codigo != null)
                    {
                        cmd.CommandText = "UPDATE Produtos SET Status=@Status, Quantidade=@Quantidade WHERE Codigo=@Codigo";
                        cmd.Parameters.AddWithValue("@Codigo", produto.Codigo);
                        cmd.Parameters.AddWithValue("@Quantidade", 0);
                        cmd.Parameters.AddWithValue("@Status", "Inativo");
                        cmd.ExecuteNonQuery();
                    }
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Venda GetVenda_Codigo(int codigo)
        {
            try
            {
                Venda v = new Venda();
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Vendas Where Codigo_Venda =" + codigo;
                    using SQLiteDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        v.codigo = rdr.GetInt32(0);
                        v.produto = GetProduto_Codigo(rdr.GetInt32(1));
                        v.preco_venda = rdr.GetDouble(2);
                        v.quantidade_venda = rdr.GetInt32(3);
                        v.data = rdr.GetString(4);
                    }
                    return v;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Venda GetVenda_Codigo_Produto(int codigo)
        {
            try
            {
                Venda v = new Venda();
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Vendas Where Codigo_Produto =" + codigo;
                    using SQLiteDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        v.codigo = rdr.GetInt32(0);
                        v.produto = GetProduto_Codigo(rdr.GetInt32(1));
                        v.preco_venda = rdr.GetDouble(2);
                        v.quantidade_venda = rdr.GetInt32(3);
                        v.data = rdr.GetString(4);
                    }
                    return v;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<Venda> GetVenda_Nome_Produto(string nome)
        {
            try
            {
                List<Venda> l = new List<Venda>();
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT Vendas.Codigo_Venda, Produtos.Codigo, Vendas.Preco_Venda, Vendas.Quantidade_Venda, Vendas.Data_Venda FROM Vendas INNER JOIN Produtos ON Vendas.Codigo_Produto = Produtos.Codigo Where Produtos.Nome LIKE '%" + nome + "%'";
                    using SQLiteDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Venda v = new Venda();
                        v.codigo = rdr.GetInt32(0);
                        v.produto = GetProduto_Codigo(rdr.GetInt32(1));
                        v.preco_venda = rdr.GetDouble(2);
                        v.quantidade_venda = rdr.GetInt32(3);
                        v.data = rdr.GetString(4);
                        l.Add(v);
                    }
                    return l;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Classe que insere produtos no Banco de Dados
        /// </summary>
        /// <param name="product"></param>
        public static string Add_Venda(Produto product)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Vendas(Codigo_Produto, Preco_Venda, Quantidade_Venda, Data_Venda) values (@Codigo_Produto, @Preco_Venda, @Quantidade_Venda, @Data_Venda)";
                    cmd.Parameters.AddWithValue("@Codigo_Produto", product.Codigo);
                    cmd.Parameters.AddWithValue("@Preco_Venda", product.Preco);
                    cmd.Parameters.AddWithValue("@Quantidade_Venda", product.Quantidade);
                    cmd.Parameters.AddWithValue("@Data_Venda", DateTime.Now.ToString());
                    cmd.ExecuteNonQuery();
                    return get_VendaId();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string get_VendaId()
        {
            try
            {
                string id = "0";
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT MAX(Codigo_Venda) FROM Vendas";
                    id = cmd.ExecuteScalar().ToString();
                }
                return id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}