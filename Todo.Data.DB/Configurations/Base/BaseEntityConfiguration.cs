using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Data.Domain.Contracts;

namespace Todo.Data.DB.Configurations.Base
{
    public class BaseEntityConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
        where TEntity : class, IEntity<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder) { }
    }
}