using Gateway.Api.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gateway.Api.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    // --- Catalog / Metamodel
    public DbSet<DataType> DataTypes => Set<DataType>();
    public DbSet<ValueDomain> ValueDomains => Set<ValueDomain>();
    public DbSet<ValueDomainValue> ValueDomainValues => Set<ValueDomainValue>();
    public DbSet<RuleField> RuleFields => Set<RuleField>();
    public DbSet<RuleOperator> RuleOperators => Set<RuleOperator>();
    public DbSet<RuleFieldOperator> RuleFieldOperators => Set<RuleFieldOperator>();
    public DbSet<PolicyConstant> PolicyConstants => Set<PolicyConstant>();

    // --- Rules
    public DbSet<RuleVersion> RuleVersions => Set<RuleVersion>();
    public DbSet<ActualRules> ActualRules => Set<ActualRules>();

    // --- Decisions
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<TransactionResult> TransactionResults => Set<TransactionResult>();
    public DbSet<RuleEvaluation> RuleEvaluations => Set<RuleEvaluation>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        // ============================
        // Catalog / Metamodel
        // ============================

        // DataType
        b.Entity<DataType>()
            .HasIndex(x => x.Code)
            .IsUnique();

        // ValueDomain
        b.Entity<ValueDomain>()
            .HasIndex(x => x.Code)
            .IsUnique();

        // ValueDomainValue (уникальность значения в рамках домена)
        b.Entity<ValueDomainValue>()
            .HasIndex(x => new { x.ValueDomainId, x.Code })
            .IsUnique();

        // Связь ValueDomain (1) -> (N) ValueDomainValue
        b.Entity<ValueDomainValue>()
            .HasOne(v => v.ValueDomain)
            .WithMany(d => d.Values)
            .HasForeignKey(v => v.ValueDomainId)
            .OnDelete(DeleteBehavior.Restrict);

        // Связь ValueDomainValue (N) -> (1) DataType
        b.Entity<ValueDomainValue>()
            .HasOne(v => v.DataType)
            .WithMany()
            .HasForeignKey(v => v.DataTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        // RuleField
        b.Entity<RuleField>()
            .HasIndex(x => x.Name)
            .IsUnique();

        b.Entity<RuleField>()
            .HasOne(x => x.DataType)
            .WithMany()
            .HasForeignKey(x => x.DataTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        b.Entity<RuleField>()
            .HasOne(x => x.ValueDomain)
            .WithMany()
            .HasForeignKey(x => x.ValueDomainId)
            .OnDelete(DeleteBehavior.Restrict);

        // RuleOperator
        b.Entity<RuleOperator>()
            .HasIndex(x => x.Code)
            .IsUnique();

        // RuleFieldOperator (join-таблица поле<->оператор)
        b.Entity<RuleFieldOperator>()
            .HasKey(x => new { x.FieldId, x.OperatorId });

        b.Entity<RuleFieldOperator>()
            .HasOne(x => x.Field)
            .WithMany(f => f.Operators)
            .HasForeignKey(x => x.FieldId)
            .OnDelete(DeleteBehavior.Cascade);

        b.Entity<RuleFieldOperator>()
            .HasOne(x => x.Operator)
            .WithMany(o => o.Fields)
            .HasForeignKey(x => x.OperatorId)
            .OnDelete(DeleteBehavior.Cascade);

        // PolicyConstant
        b.Entity<PolicyConstant>()
            .HasIndex(x => x.Code)
            .IsUnique();
        b.Entity<PolicyConstant>()
            .HasOne(x => x.Type)
            .WithMany()
            .HasForeignKey(x => x.TypeId)
            .OnDelete(DeleteBehavior.Restrict);

        // ============================
        // Rules
        // ============================

        // RuleVersion
        b.Entity<RuleVersion>()
            .HasIndex(x => new { x.RuleKey, x.Version })
            .IsUnique();

        // JSONB для DSL-описания правила
        b.Entity<RuleVersion>()
            .Property(x => x.Description)
            .HasColumnType("jsonb");

        // ActualRules (указатель на активную версию правила)
        b.Entity<ActualRules>()
            .HasKey(x => x.RuleKey);

        b.Entity<ActualRules>()
            .HasOne(x => x.RuleVersion)
            .WithMany()
            .HasForeignKey(x => x.RuleVersionId)
            .OnDelete(DeleteBehavior.Restrict);

        // ============================
        // Transactions / Results / Evaluations
        // ============================

        // Transaction (идемпотентность по GlobalId)
        b.Entity<Transaction>()
            .HasIndex(x => x.GlobalId)
            .IsUnique();

        // FK на валюту (ValueDomainValue), запрет каскадного удаления
        b.Entity<Transaction>()
            .HasOne(t => t.Currency)
            .WithMany()
            .HasForeignKey(t => t.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        // TransactionResult: один результат на транзакцию
        b.Entity<TransactionResult>()
            .HasIndex(x => x.TransactionId)
            .IsUnique();

        b.Entity<TransactionResult>()
            .HasOne(x => x.Transaction)
            .WithMany()
            .HasForeignKey(x => x.TransactionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        b.Entity<TransactionResult>()
            .HasOne(x => x.Currency)
            .WithMany()
            .HasForeignKey(x => x.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        // RuleEvaluation: уникальность оценки конкретной версии правила для конкретного результата
        b.Entity<RuleEvaluation>()
            .HasIndex(x => new { x.TransactionResultId, x.RuleVersionId })
            .IsUnique();

        b.Entity<RuleEvaluation>()
            .HasOne(x => x.TransactionResult)
            .WithMany(r => r.Evaluations)
            .HasForeignKey(x => x.TransactionResultId)
            .OnDelete(DeleteBehavior.Cascade);

        b.Entity<RuleEvaluation>()
            .HasOne(x => x.RuleVersion)
            .WithMany()
            .HasForeignKey(x => x.RuleVersionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
