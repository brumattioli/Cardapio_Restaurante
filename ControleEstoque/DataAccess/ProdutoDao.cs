using ControleEstoque.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstoque.DataAccess
{
    public class ProdutoDao
    {
        private static SQLiteConnection sqliteConnection;

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
        public int GetCodigo(int cod)
        {
            try
            {
                int codigo = 0;
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT Codigo FROM Produtos Where Codigo =" + cod;
                    using SQLiteDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        codigo = rdr.GetInt32(0);
                    }
                    rdr.Close();
                    return codigo;
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
        public string Create(Produto pro)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Produtos(Codigo, Nome, Preco, Quantidade, Status) values (@Codigo, @Nome, @Preco, @Quantidade, @Status)";
                    cmd.Parameters.AddWithValue("@Codigo", pro.Codigo);
                    cmd.Parameters.AddWithValue("@Nome", pro.Nome);
                    cmd.Parameters.AddWithValue("@Preco", pro.Preco);
                    cmd.Parameters.AddWithValue("@Quantidade", pro.Quantidade);
                    cmd.Parameters.AddWithValue("@Status", "Ativo");
                    cmd.ExecuteNonQuery();
                    return "Produto cadastrado com sucesso!";
                }
            }
            catch (Exception ex)
            {
                return "Erro: " + ex.Message;
            }
        }
        public List<Produto> GetProduto_Nome(string Nome)
        {
            try
            {
                List<Produto> p = new List<Produto>();
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Produtos Where Nome LIKE '%" + Nome + "%'";
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
                    rdr.Close();
                    return p;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Produto GetProduto_Codigo(int Codigo)
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
                    rdr.Close();
                    return p;
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
        public List<Produto> GetProdutos()
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
        /// Classe que remove produtos no Banco de Dados a partir do Código do produto
        /// </summary>
        /// <param name="produto"></param>
        public string Remover_Item(int codigo)
        {
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    cmd.CommandText = "UPDATE Produtos SET Status=@Status, Quantidade=@Quantidade WHERE Codigo=@Codigo";
                    cmd.Parameters.AddWithValue("@Codigo", codigo);
                    cmd.Parameters.AddWithValue("@Quantidade", 0);
                    cmd.Parameters.AddWithValue("@Status", "Inativo");
                    cmd.ExecuteNonQuery();
                    return "O produto foi removido com sucesso!";
                };
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
        public bool Update_Quantidade(int codigo, int quantidade)
        {
            try
            {
                bool retorno = false;
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    cmd.CommandText = "UPDATE Produtos SET Quantidade=@Quantidade WHERE Codigo=@Codigo";
                    cmd.Parameters.AddWithValue("@Codigo", codigo);
                    cmd.Parameters.AddWithValue("@Quantidade", quantidade);
                    cmd.ExecuteNonQuery();
                    retorno = true;
                };
                return retorno;
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
        public bool Update_Preco(int codigo, double preco)
        {
            try
            {
                bool retorno = false;
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    cmd.CommandText = "UPDATE Produtos SET Preco=@Preco WHERE Codigo=@Codigo";
                    cmd.Parameters.AddWithValue("@Codigo", codigo);
                    cmd.Parameters.AddWithValue("@Preco", preco);
                    cmd.ExecuteNonQuery();
                    retorno = true;
                };
                return retorno;
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
        public bool Update_Nome(int codigo, string nome)
        {
            try
            {
                bool retorno = false;
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    cmd.CommandText = "UPDATE Produtos SET Nome=@Nome WHERE Codigo=@Codigo";
                    cmd.Parameters.AddWithValue("@Codigo", codigo);
                    cmd.Parameters.AddWithValue("@Nome", nome);
                    cmd.ExecuteNonQuery();
                    retorno = true;
                };
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}