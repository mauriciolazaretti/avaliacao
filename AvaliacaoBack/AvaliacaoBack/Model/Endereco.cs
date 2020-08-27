using AvaliacaoBack.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AvaliacaoBack.Model
{
    public class Endereco
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public int numero { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public EnumTipoEndereco TipoEndereco { get; set; }

        [ForeignKey("Pessoa")]
        public int PessoaId { get; set; }

        public virtual Pessoa  Pessoa { get; set; }

    }
}
