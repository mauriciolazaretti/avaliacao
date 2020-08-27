using AvaliacaoBack.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AvaliacaoBack.Model
{
    public class Pessoa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(20)]
        public string CpfCnpj { get; set; }
        public string NomeRazaoSocial { get; set; }
        public EnumTipoPessoa TipoPessoa { get; set; }
        [ForeignKey("PessoaId")]
        public virtual List<Telefone> Telefones { get; set; }
        public virtual List<Endereco> Enderecos { get; set; }
    }
}
