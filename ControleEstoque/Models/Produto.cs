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
    public class Produto : BaseModel
    {
        public string Nome { get; set; }
        public int Codigo { get; set; }
        public double Preco { get; set; }
        public int Quantidade{ get; set; }
        public string Status { get; set; }

    }
}
