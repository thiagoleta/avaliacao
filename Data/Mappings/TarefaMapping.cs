using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class TarefaMapping : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Titulo)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(c => c.Concluida)
                .IsRequired()
                .HasColumnType("bit");   
         

        }
    }
}
