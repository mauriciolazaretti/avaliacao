using AvaliacaoBack.Enum;
using AvaliacaoBack.Model;
using AvaliacaoBack.VO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvaliacaoBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {

        private DataContext _db;
        public PessoaController(DataContext db)
        {
            this._db = db;
        }
        [HttpPost]
        public async Task<IActionResult> SalvarPessoaAsync([FromBody] PessoaVO pessoaVO)
        {
            try
            {
                if (pessoaVO == null)
                    return Ok(new { sucesso = false, msg = "Faltam campos a preencher" });
                if (string.IsNullOrEmpty(pessoaVO.cpfCnpj))
                    return Ok(new { sucesso = false, msg = "Falta preencher cpf" });
                if(string.IsNullOrEmpty(pessoaVO.nomeRazaoSocial))
                    return Ok(new { sucesso = false, msg = "Falta preencher nome" });
                if (pessoaVO.enderecos.Count == 0)
                    return Ok(new { sucesso = false, msg = "Você deve cadastrar pelo menos 1 endereço!" });
                if (pessoaVO.telefones.Count == 0)
                    return Ok(new { sucesso = false, msg = "Você deve cadastrar pelo menos 1 telefone!" });


                var pessoa = _db.Pessoas.SingleOrDefault(o => o.Id == pessoaVO.id) ??
                new Pessoa
                {
                    CpfCnpj = pessoaVO.cpfCnpj,
                    NomeRazaoSocial = pessoaVO.nomeRazaoSocial,
                    TipoPessoa = (EnumTipoPessoa)pessoaVO.tipoPessoa

                };
                if(pessoa.Id == 0)
                    _db.Pessoas.Add(pessoa);

                await _db.SaveChangesAsync();

                if (pessoa.Enderecos != null)
                {
                    _db.Enderecos.RemoveRange(pessoa.Enderecos);
                    await _db.SaveChangesAsync();
                }


                _db.Enderecos.AddRange(pessoaVO.enderecos.Select(o =>
                    new Endereco
                    {
                        logradouro = o.logradouro,
                        bairro = o.bairro,
                        cidade = o.cidade,
                        complemento = o.complemento,
                        numero = o.numero,
                        PessoaId = pessoa.Id,
                        TipoEndereco = (EnumTipoEndereco)o.tipoEndereco
                    }
                ).ToList());

                await _db.SaveChangesAsync();
                if (pessoa.Telefones != null)
                {
                    _db.Telefones.RemoveRange(pessoa.Telefones);
                    await _db.SaveChangesAsync();
                }

                _db.Telefones.AddRange(pessoaVO.telefones.Select(o =>
                    new Telefone
                    {
                        ddd = o.ddd,
                        PessoaId = pessoa.Id,
                        telefone = o.telefone,
                        TipoTelefone = (EnumTipoTelefone)o.tipoTelefone
                    }
                ).ToList());

                await _db.SaveChangesAsync();

                return Ok(new { sucesso = true});
            }catch(Exception ex)
            {
                return BadRequest();
            }
           
            }

        [HttpGet]
        public async Task<IActionResult> GetPaginado([FromQuery]int pagina, [FromQuery]int qtd, [FromQuery]string sort)
        {
            try
            {
               var lista = _db.Pessoas.Skip(qtd * pagina).Take(qtd).ToList()
                    .Select(o =>
                        new PessoaVO
                        {
                            cpfCnpj = o.CpfCnpj,
                            id = o.Id,
                            nomeRazaoSocial = o.NomeRazaoSocial,
                            tipoPessoa = Convert.ToInt32(o.TipoPessoa),
                            enderecos = o.Enderecos.Select(e =>
                                new EnderecoVO
                                {
                                    id = e.Id,
                                    bairro = e.bairro,
                                    cidade = e.cidade,
                                    complemento = e.complemento,
                                    logradouro = e.logradouro,
                                    numero = e.numero,
                                    pessoaId = o.Id
                                }

                            ).ToList(),
                            telefones = o.Telefones.Select(t =>
                                new TelefoneVO
                                {
                                    id = t.Id,
                                    ddd = t.ddd,
                                    telefone = t.telefone,
                                    tipoTelefone = Convert.ToInt32(t.TipoTelefone),
                                    pessoaId = o.Id
                                }

                            ).ToList()

                        }
                    ).ToList();
                switch (sort)
                {
                    case "id": lista = lista.OrderBy(o => o.id).ToList();
                        break;
                    case "id_desc":
                        lista = lista.OrderByDescending(o => o.id).ToList();
                        break;
                    case "tipoPessoa":
                        lista = lista.OrderBy(o => o.tipoPessoa).ToList();
                        break;
                    case "tipoPessoa_desc":
                        lista = lista.OrderByDescending(o => o.tipoPessoa).ToList();
                        break;
                    case "cpfCnpj":
                        lista = lista.OrderBy(o => o.cpfCnpj).ToList();
                        break;
                    case "cpfCnpj_desc":
                        lista = lista.OrderByDescending(o => o.cpfCnpj).ToList();
                        break;
                    case "nomeRazaoSocial":
                        lista = lista.OrderBy(o => o.nomeRazaoSocial).ToList();
                        break;
                    case "nomeRazaoSocial_desc":
                        lista = lista.OrderByDescending(o => o.nomeRazaoSocial).ToList();
                        break;
                }
                var count = _db.Pessoas.Count();
                return Ok(new { pessoas = lista, total = count});
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
        [HttpGet("delete")]
        public async Task<IActionResult> Delete([FromQuery]int idPessoa)
        {
            try
            {
                var pessoa = _db.Pessoas.SingleOrDefault(o => o.Id == idPessoa);
                if (pessoa == null)
                    return BadRequest();
                _db.Enderecos.RemoveRange(pessoa.Enderecos);
                _db.Telefones.RemoveRange(pessoa.Telefones);
                _db.Pessoas.Remove(pessoa);
                await _db.SaveChangesAsync();
                return Ok(new { sucesso = true });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
    }
}
