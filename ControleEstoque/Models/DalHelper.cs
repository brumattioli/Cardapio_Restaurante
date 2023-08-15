using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Documents;

namespace ControleEstoque.Models
{
    public class DalHelper
    {
        private static SQLiteConnection sqliteConnection;

        public DalHelper()
        { }

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

        public static void CriarTabelaSQlite()
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Produtos(Id int, Nome Varchar(50), Preco double, Quantidade int)";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Add(Produto product)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Produtos(Id, Nome, Preco, Quantidade) values (@Id, @Nome, @Preco, @Quantidade)";
                    cmd.Parameters.AddWithValue("@Id", product.Id);
                    cmd.Parameters.AddWithValue("@Nome", product.Nome);
                    cmd.Parameters.AddWithValue("@Preco", product.Preco);
                    cmd.Parameters.AddWithValue("@Quantidade", product.Quantidade);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public static List<Produto> GetProdutos()
        {
            //SQLiteDataAdapter da = null;
            //DataSet ds = new DataSet();
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
                        op.Id = rdr.GetInt32(0);
                        op.Nome = rdr.GetString(1);
                        op.Preco = rdr.GetDouble(2);
                        op.Quantidade = rdr.GetInt32(3);
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
        public static List<Produto> GetProduto_Nome(string Nome)
        {
            try
            {
                List<Produto> p = new List<Produto>();
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Produtos Where Nome ='"+ Nome +"'";
                    using SQLiteDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Produto op = new Produto();
                        op.Id = rdr.GetInt32(0);
                        op.Nome = rdr.GetString(1);
                        op.Preco = rdr.GetDouble(2);
                        op.Quantidade = rdr.GetInt32(3);
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
        public static List<Produto> GetProduto_Id(int Id)
        {
            try
            {
            List<Produto> p = new List<Produto>();
            using (var cmd = DbConnection().CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Produtos Where Id =" + Id;
                using SQLiteDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Produto op = new Produto();
                    op.Id = rdr.GetInt32(0);
                    op.Nome = rdr.GetString(1);
                    op.Preco = rdr.GetDouble(2);
                    op.Quantidade = rdr.GetInt32(3);
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
    }
}
