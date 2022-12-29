using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchList.Domain;

namespace WatchItem.Infrastructure.Database.Configurations;

public abstract class EntityConfigurationBase<T> : IEntityTypeConfiguration<T> 
    where T : Entity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id);
        ConfigureCore(builder);
    }

    protected abstract void ConfigureCore(EntityTypeBuilder<T> builder);
}