using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchList.Domain.WatchItems;

namespace WatchItem.Infrastructure.Database.Configurations;

public sealed class WatchItemConfiguration : EntityConfigurationBase<WatchList.Domain.WatchItems.WatchItem>
{
    protected override void ConfigureCore(EntityTypeBuilder<WatchList.Domain.WatchItems.WatchItem> builder)
    {
        builder.Property(e => e.Title).IsRequired().HasMaxLength(128);
        builder.Property(e => e.Description);
        builder.Property(e => e.Rating);
        builder.Property(e => e.ReleaseDate);
        
        builder.Property(e => e.Image)
            .HasConversion(imageBase64 => imageBase64 == null ? null : imageBase64.Value.Url, 
                url => string.IsNullOrEmpty(url) ? null : new ImageBase64(url));
        
        builder.Property(e => e.Status);
        builder.Property(e => e.Type);
        builder.Property(e => e.Genre).HasConversion(set => JsonSerializer.Serialize(set, JsonSerializerOptions.Default), 
            json => JsonSerializer.Deserialize<HashSet<string>>(json, JsonSerializerOptions.Default) ?? new HashSet<string>());
    }
}