using AvaliacaoBack.Enum;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvaliacaoBack.VO
{
    public class EnderecoVO
    {
        public int id { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public int numero { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public int tipoEndereco { get; set; }

        
        public int pessoaId { get; set; }
    }
    public class TelefoneVO
    {
        
        public int id { get; set; }
        public string ddd { get; set; }
        public string telefone { get; set; }
        public int tipoTelefone { get; set; }
        public int pessoaId { get; set; }
    }
    public class PessoaVO 
    {
        public int id { get; set; }
        public string cpfCnpj { get; set; }
        public string nomeRazaoSocial { get; set; }
        public int tipoPessoa { get; set; }

        public virtual List<TelefoneVO> telefones { get; set; }
        public virtual List<EnderecoVO> enderecos { get; set; }
    }
}
