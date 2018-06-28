namespace EFDemo.CodeFirstFromDB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using EFDemo.CodeFirstFromDB.Mapping;

    public partial class CodeFirstFromDBContext : DbContext
    {
        public CodeFirstFromDBContext()
            : base("name=CodeFirstFromDB")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<JDCommodity001> JD_Commodity_001 { get; set; }
        public virtual DbSet<JDCommodity002> JD_Commodity_002 { get; set; }
        public virtual DbSet<JDCommodity003> JD_Commodity_003 { get; set; }
        public virtual DbSet<JD_Commodity_004> JD_Commodity_004 { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserMenuMapping> UserMenuMappings { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JDCommodity003>().ToTable("JD_Commodity_003")
                .Property(c => c.ClassId).HasColumnName("CatetgoryId");


            modelBuilder.Configurations.Add(new JDCommodity002Mapping());

            modelBuilder.Entity<Category>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.ParentCode)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Company)
                .WillCascadeOnDelete(false);

             

            modelBuilder.Entity<Menu>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.SourcePath)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Account)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Mobile)
                .IsUnicode(false);
        }
    }
}
