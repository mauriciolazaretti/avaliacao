using AvaliacaoBack.Enum;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvaliacaoBack.Model
{
    public class DataContext : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Telefone> Telefones { get; set; }
        public DataContext()
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<EnumTipoPessoa>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<EnumTipoTelefone>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<EnumTipoEndereco>();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5433;Database=avaliacao;User Id=postgres;Password=123;");
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasPostgresEnum<EnumTipoPessoa>();
            builder.HasPostgresEnum<EnumTipoTelefone>();
            builder.HasPostgresEnum<EnumTipoEndereco>();
            //builder.Entity<Pessoa>().Property(o => o.Id).ValueGeneratedOnAdd();
            //builder.Entity<Endereco>().Property(o => o.Id).ValueGeneratedOnAdd();
            //builder.Entity<Telefone>().Property(o => o.Id).ValueGeneratedOnAdd();
        }
    }
}
