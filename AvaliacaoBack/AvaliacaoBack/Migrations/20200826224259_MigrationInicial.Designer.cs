﻿// <auto-generated />
using AvaliacaoBack.Enum;
using AvaliacaoBack.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AvaliacaoBack.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200826224259_MigrationInicial")]
    partial class MigrationInicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:Enum:enum_tipo_endereco", "cobranca,comercial,correspondencia,entrega,residencial")
                .HasAnnotation("Npgsql:Enum:enum_tipo_pessoa", "fisica,juridica")
                .HasAnnotation("Npgsql:Enum:enum_tipo_telefone", "celular,residencial,comercial")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("AvaliacaoBack.Model.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("PessoaId")
                        .HasColumnType("integer");

                    b.Property<EnumTipoEndereco>("TipoEndereco")
                        .HasColumnType("enum_tipo_endereco");

                    b.Property<string>("bairro")
                        .HasColumnType("text");

                    b.Property<string>("cidade")
                        .HasColumnType("text");

                    b.Property<string>("complemento")
                        .HasColumnType("text");

                    b.Property<string>("logradouro")
                        .HasColumnType("text");

                    b.Property<int>("numero")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PessoaId");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("AvaliacaoBack.Model.Pessoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CpfCnpj")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<string>("NomeRazaoSocial")
                        .HasColumnType("text");

                    b.Property<EnumTipoPessoa>("TipoPessoa")
                        .HasColumnType("enum_tipo_pessoa");

                    b.HasKey("Id");

                    b.ToTable("Pessoas");
                });

            modelBuilder.Entity("AvaliacaoBack.Model.Telefone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("PessoaId")
                        .HasColumnType("integer");

                    b.Property<EnumTipoTelefone>("TipoTelefone")
                        .HasColumnType("enum_tipo_telefone");

                    b.Property<string>("ddd")
                        .HasColumnType("character varying(2)")
                        .HasMaxLength(2);

                    b.Property<string>("telefone")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PessoaId");

                    b.ToTable("Telefones");
                });

            modelBuilder.Entity("AvaliacaoBack.Model.Endereco", b =>
                {
                    b.HasOne("AvaliacaoBack.Model.Pessoa", "Pessoa")
                        .WithMany("Enderecos")
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AvaliacaoBack.Model.Telefone", b =>
                {
                    b.HasOne("AvaliacaoBack.Model.Pessoa", "Pessoa")
                        .WithMany("Telefones")
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
