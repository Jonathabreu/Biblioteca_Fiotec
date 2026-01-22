using Biblioteca.Domain.Entities;
using Biblioteca.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infra.Persistencia
{
    public class BibliotecaDbContext : DbContext
    {
        public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Livro> Livros => Set<Livro>();
        public DbSet<Emprestimo> Emprestimos => Set<Emprestimo>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Livro>(b =>
            {
                b.ToTable("Livros");
                b.HasKey(x => x.Id);
                b.Property(x => x.Titulo).HasMaxLength(200).IsRequired();
                b.Property(x => x.Autor).HasMaxLength(100).IsRequired();
                b.Property(x => x.QuantidadeDisponivel).IsRequired();
            });

            modelBuilder.Entity<Emprestimo>(b =>
            {
                b.ToTable("Emprestimos");
                b.HasKey(x => x.Id);
                b.Property(x => x.NomeSolicitante).HasMaxLength(100).IsRequired();
                b.Property(x => x.DataEmprestimo).IsRequired();
                b.Property(x => x.DataDevolucao);

                b.HasOne(x => x.Livro)
                    .WithMany()
                    .HasForeignKey(x => x.LivroId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
