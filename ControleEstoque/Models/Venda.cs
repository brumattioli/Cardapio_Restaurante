using System;

namespace ControleEstoque.Models
{
    public class Venda : BaseModel
    {
        public int codigo { get; set; }
        public Produto produto { get; set; }
        public string data { get; set; }
        public int quantidade_venda { get; set; }
        public double preco_venda { get; set; }
    }
}