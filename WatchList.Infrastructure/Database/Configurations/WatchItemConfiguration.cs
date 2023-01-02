using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchList.Domain.WatchItems.Entity;

namespace WatchList.Infrastructure.Database.Configurations;

public sealed class WatchItemConfiguration : EntityConfigurationBase<WatchList.Domain.WatchItems.Entity.WatchItem>
{
    protected override void ConfigureCore(EntityTypeBuilder<WatchList.Domain.WatchItems.Entity.WatchItem> builder)
    {
        builder.Property(e => e.Title).IsRequired().HasMaxLength(128);
        builder.Property(e => e.Description);
        builder.Property(e => e.Rating)
            .HasConversion<float?>(rating => rating,
                rating => rating != null ? new Rating(rating.Value) : null);
        
        builder.Property(e => e.ReleaseDate);
        
        builder.Property(e => e.Image)
            .HasConversion(imageBase64 => imageBase64 == null ? null : imageBase64.Value.Url, 
                url => string.IsNullOrEmpty(url) ? null : new ImageBase64(url));
        
        builder.Property(e => e.Status);
        builder.Property(e => e.Type);

        var genreComparator = new ValueComparer<HashSet<string>>(
            (set, hashSet) => (ReferenceEquals(set, null) && ReferenceEquals(hashSet, null)) || (!ReferenceEquals(set, null) && !ReferenceEquals(hashSet, null) && set.SetEquals(hashSet)),
            set => set.Count == 0 ? 5215 : set.Aggregate(5215, HashCode.Combine)
        );
        
        builder.Property(e => e.Genre)
            .HasConversion(set => JsonSerializer.Serialize(set, JsonSerializerOptions.Default), 
            json => JsonSerializer.Deserialize<HashSet<string>>(json, JsonSerializerOptions.Default) ?? new HashSet<string>(),
            genreComparator);
    }
}