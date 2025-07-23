using ChatBotAI.Infrastructure.DatabaseEntities;
using Microsoft.EntityFrameworkCore;

namespace ChatBotAI.Infrastructure.DatabaseContext;

public partial class ChatbotAiContext : DbContext, IChatbotAiContext
{
    public ChatbotAiContext()
    {
    }

    public ChatbotAiContext(DbContextOptions<ChatbotAiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AiEngineResponse> Aiengineresponses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRequest> Userrequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AiEngineResponse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AIENGINE__3214EC27F850CAC3");

            entity
                .ToTable("AIENGINERESPONSE")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("AIENGINERESPONSE_HISTORY", "history");
                        ttb
                            .HasPeriodStart("VALIDFROM")
                            .HasColumnName("VALIDFROM");
                        ttb
                            .HasPeriodEnd("VALIDTO")
                            .HasColumnName("VALIDTO");
                    }));

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Answer).HasColumnName("ANSWER");
            entity.Property(e => e.CreatedDatetime)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("CREATED_DATETIME");
            entity.Property(e => e.ModifiedDatetime)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("MODIFIED_DATETIME");
            entity.Property(e => e.Rating).HasColumnName("RATING");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");
            entity.Property(e => e.UserrequestId).HasColumnName("USERREQUEST_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Aiengineresponses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AIENGINERESPONSE_USER");

            entity.HasOne(d => d.Userrequest).WithMany(p => p.Aiengineresponses)
                .HasForeignKey(d => d.UserrequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AIENGINERESPONSE_USERREQUEST");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USER__3214EC2739849347");

            entity
                .ToTable("USER", "auth")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("USER_HISTORY", "history");
                        ttb
                            .HasPeriodStart("VALIDFROM")
                            .HasColumnName("VALIDFROM");
                        ttb
                            .HasPeriodEnd("VALIDTO")
                            .HasColumnName("VALIDTO");
                    }));

            entity.HasIndex(e => e.UserLogin, "UQ_USER_USER_LOGIN").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.CreatedDatetime)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("CREATED_DATETIME");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.ModifiedDatetime)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("MODIFIED_DATETIME");
            entity.Property(e => e.UserLogin)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("USER_LOGIN");
        });

        modelBuilder.Entity<UserRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USERREQU__3214EC2798B6173D");

            entity
                .ToTable("USERREQUEST")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("USERREQUEST_HISTORY", "history");
                        ttb
                            .HasPeriodStart("VALIDFROM")
                            .HasColumnName("VALIDFROM");
                        ttb
                            .HasPeriodEnd("VALIDTO")
                            .HasColumnName("VALIDTO");
                    }));

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.CreatedDatetime)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("CREATED_DATETIME");
            entity.Property(e => e.Request).HasColumnName("REQUEST");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Userrequests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USERREQUEST_USER");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
