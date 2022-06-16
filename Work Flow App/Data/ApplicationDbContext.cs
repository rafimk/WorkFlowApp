using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Work_Flow_App.Domain;
using Work_Flow_App.Infrastructure.Interfaces;

namespace Work_Flow_App.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        private readonly ICurrentUserService _currentUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
                                    ICurrentUserService currentUser) : base(options)
        {
            _currentUser = currentUser;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Request>()
               .HasQueryFilter(r => !r.IsDeleted)
               .HasOne(r => r.User)
               .WithMany(u => u.Requests)
               .HasForeignKey(r => r.CreatedBy)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<RequestWorkFlow>()
                     .HasOne(w => w.User)
                     .WithMany(u => u.RequestWorkFlows)
                     .HasForeignKey(w => w.ActionBy)
                     .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }

        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestWorkFlow> RequestWorkFlows { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInformation();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            this.ApplyAuditInformation();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void ApplyAuditInformation()
        { 
           this.ChangeTracker
               .Entries()
               .ToList()
               .ForEach(entry =>
               {
                   var userId = _currentUser.GetId();

                   if (entry.Entity is AuditEntity deletableEntity)
                   {
                       if (entry.State == EntityState.Deleted)
                       {
                           deletableEntity.DeletedOn = DateTime.UtcNow;
                           deletableEntity.DeletedBy = userId;
                           deletableEntity.IsDeleted = true;

                           entry.State = EntityState.Modified;

                           return;
                       }
                   }

                   if (entry.Entity is AuditEntity entity)
                   {
                       if (entry.State == EntityState.Added)
                       {
                           entity.CreatedOn = DateTime.UtcNow;
                           entity.CreatedBy = userId;
                       }
                       else if (entry.State == EntityState.Modified)
                       {
                           entity.ModifiedOn = DateTime.UtcNow;
                           entity.ModifiedBy = userId;
                       }
                   }
               });       
        }
    }
}
