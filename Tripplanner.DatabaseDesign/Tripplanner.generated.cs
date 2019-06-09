//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
//
//     Produced by Entity Framework Visual Editor
//     https://github.com/msawczyn/EFDesigner
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;

namespace Tripplanner.DatabaseDesign
{
   /// <inheritdoc/>
   public partial class Tripplanner : System.Data.Entity.DbContext
   {
      #region DbSets
      public virtual System.Data.Entity.DbSet<global::Tripplanner.DatabaseDesign.Accommodation> Accommodations { get; set; }
      public virtual System.Data.Entity.DbSet<global::Tripplanner.DatabaseDesign.Activity> Activities { get; set; }
      public virtual System.Data.Entity.DbSet<global::Tripplanner.DatabaseDesign.CustomItem> CustomItems { get; set; }
      public virtual System.Data.Entity.DbSet<global::Tripplanner.DatabaseDesign.ExchangeRate> ExchangeRates { get; set; }
      public virtual System.Data.Entity.DbSet<global::Tripplanner.DatabaseDesign.ExternalLink> ExternalLinks { get; set; }
      public virtual System.Data.Entity.DbSet<global::Tripplanner.DatabaseDesign.Transportation> Transportations { get; set; }
      public virtual System.Data.Entity.DbSet<global::Tripplanner.DatabaseDesign.TripInfo> TripInfoes { get; set; }
      public virtual System.Data.Entity.DbSet<global::Tripplanner.DatabaseDesign.WeatherForecast> WeatherForecasts { get; set; }
      #endregion DbSets

      #region Constructors

      partial void CustomInit();

      #warning Default constructor not generated for Tripplanner since no default connection string was specified in the model

      /// <inheritdoc />
      public Tripplanner(string connectionString) : base(connectionString)
      {
         Configuration.LazyLoadingEnabled = true;
         Configuration.ProxyCreationEnabled = true;
         System.Data.Entity.Database.SetInitializer<Tripplanner>(new TripplannerDatabaseInitializer());
         CustomInit();
      }

      /// <inheritdoc />
      public Tripplanner(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model) : base(connectionString, model)
      {
         Configuration.LazyLoadingEnabled = true;
         Configuration.ProxyCreationEnabled = true;
         System.Data.Entity.Database.SetInitializer<Tripplanner>(new TripplannerDatabaseInitializer());
         CustomInit();
      }

      /// <inheritdoc />
      public Tripplanner(System.Data.Common.DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
      {
         Configuration.LazyLoadingEnabled = true;
         Configuration.ProxyCreationEnabled = true;
         System.Data.Entity.Database.SetInitializer<Tripplanner>(new TripplannerDatabaseInitializer());
         CustomInit();
      }

      /// <inheritdoc />
      public Tripplanner(System.Data.Common.DbConnection existingConnection, System.Data.Entity.Infrastructure.DbCompiledModel model, bool contextOwnsConnection) : base(existingConnection, model, contextOwnsConnection)
      {
         Configuration.LazyLoadingEnabled = true;
         Configuration.ProxyCreationEnabled = true;
         System.Data.Entity.Database.SetInitializer<Tripplanner>(new TripplannerDatabaseInitializer());
         CustomInit();
      }

      /// <inheritdoc />
      public Tripplanner(System.Data.Entity.Infrastructure.DbCompiledModel model) : base(model)
      {
         Configuration.LazyLoadingEnabled = true;
         Configuration.ProxyCreationEnabled = true;
         System.Data.Entity.Database.SetInitializer<Tripplanner>(new TripplannerDatabaseInitializer());
         CustomInit();
      }

      /// <inheritdoc />
      public Tripplanner(System.Data.Entity.Core.Objects.ObjectContext objectContext, bool dbContextOwnsObjectContext) : base(objectContext, dbContextOwnsObjectContext)
      {
         Configuration.LazyLoadingEnabled = true;
         Configuration.ProxyCreationEnabled = true;
         System.Data.Entity.Database.SetInitializer<Tripplanner>(new TripplannerDatabaseInitializer());
         CustomInit();
      }

      #endregion Constructors

      partial void OnModelCreatingImpl(System.Data.Entity.DbModelBuilder modelBuilder);
      partial void OnModelCreatedImpl(System.Data.Entity.DbModelBuilder modelBuilder);

      /// <inheritdoc />
      protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder);
         OnModelCreatingImpl(modelBuilder);

         modelBuilder.HasDefaultSchema("dbo");

         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.Accommodation>()
                     .ToTable("Accommodations")
                     .HasKey(t => t.Id);
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.Accommodation>()
                     .Property(t => t.Id)
                     .IsRequired()
                     .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.Accommodation>()
                     .Property(t => t.Address)
                     .IsRequired();

         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.Activity>()
                     .ToTable("Activities")
                     .HasKey(t => t.Id);
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.Activity>()
                     .Property(t => t.Id)
                     .IsRequired()
                     .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.Activity>()
                     .Property(t => t.Place)
                     .IsRequired();

         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.CustomItem>()
                     .ToTable("CustomItems")
                     .HasKey(t => t.Id);
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.CustomItem>()
                     .Property(t => t.Id)
                     .IsRequired()
                     .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.CustomItem>()
                     .Property(t => t.Key)
                     .IsRequired();
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.CustomItem>()
                     .Property(t => t.Value)
                     .IsRequired();

         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.ExchangeRate>()
                     .ToTable("ExchangeRates")
                     .HasKey(t => t.Id);
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.ExchangeRate>()
                     .Property(t => t.Id)
                     .IsRequired()
                     .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.ExchangeRate>()
                     .Property(t => t.FromCurrency)
                     .IsRequired();
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.ExchangeRate>()
                     .Property(t => t.ToCurrency)
                     .IsRequired();

         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.ExternalLink>()
                     .ToTable("ExternalLinks")
                     .HasKey(t => t.Id);
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.ExternalLink>()
                     .Property(t => t.Id)
                     .IsRequired()
                     .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.ExternalLink>()
                     .Property(t => t.URL)
                     .IsRequired();

         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.Transportation>()
                     .ToTable("Transportations")
                     .HasKey(t => t.Id);
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.Transportation>()
                     .Property(t => t.Id)
                     .IsRequired()
                     .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.Transportation>()
                     .Property(t => t.Type)
                     .IsRequired();

         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.TripInfo>()
                     .ToTable("TripInfoes")
                     .HasKey(t => t.Id);
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.TripInfo>()
                     .Property(t => t.Id)
                     .IsRequired()
                     .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.TripInfo>()
                     .Property(t => t.TripId)
                     .IsRequired();
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.TripInfo>()
                     .Property(t => t.Name)
                     .IsRequired();
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.TripInfo>()
                     .Property(t => t.DateFrom)
                     .IsRequired();
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.TripInfo>()
                     .HasMany(x => x.Transportations)
                     .WithRequired()
                     .Map(x => x.MapKey("TripInfo.Transportations_Id"));
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.TripInfo>()
                     .HasMany(x => x.Accommodations)
                     .WithRequired()
                     .Map(x => x.MapKey("TripInfo.Accommodations_Id"));
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.TripInfo>()
                     .HasMany(x => x.Sightseeings)
                     .WithRequired()
                     .Map(x => x.MapKey("TripInfo.Sightseeings_Id"));
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.TripInfo>()
                     .HasMany(x => x.ExchangeRates)
                     .WithRequired()
                     .Map(x => x.MapKey("TripInfo.ExchangeRates_Id"));
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.TripInfo>()
                     .HasMany(x => x.WeatherForecasts)
                     .WithRequired()
                     .Map(x => x.MapKey("TripInfo.WeatherForecasts_Id"));
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.TripInfo>()
                     .HasMany(x => x.Trips)
                     .WithRequired()
                     .Map(x => x.MapKey("TripInfo.Trips_Id"));
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.TripInfo>()
                     .HasMany(x => x.ExternalLinks)
                     .WithRequired()
                     .Map(x => x.MapKey("TripInfo.ExternalLinks_Id"));
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.TripInfo>()
                     .HasMany(x => x.CustomItems)
                     .WithRequired()
                     .Map(x => x.MapKey("TripInfo.CustomItems_Id"));

         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.WeatherForecast>()
                     .ToTable("WeatherForecasts")
                     .HasKey(t => t.Id);
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.WeatherForecast>()
                     .Property(t => t.Id)
                     .IsRequired()
                     .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.WeatherForecast>()
                     .Property(t => t.Location)
                     .IsRequired();
         modelBuilder.Entity<global::Tripplanner.DatabaseDesign.WeatherForecast>()
                     .Property(t => t.LastTemperature)
                     .IsRequired();

         OnModelCreatedImpl(modelBuilder);
      }
   }
}
