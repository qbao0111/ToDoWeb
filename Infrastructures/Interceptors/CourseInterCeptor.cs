using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ToDoWeb.Domains.Entities;
//soft delete , deleteby deletedAt, isDeleted = 1 thi an khoi nguoi dung , ko xoa khoi database vans
namespace ToDoWeb.Infrastructures.Interceptors
{
    public class CourseInterCepTor : SaveChangesInterceptor
    {
        List<EntityEntry> addedEntities = new List<EntityEntry>();
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            var context = eventData.Context as ApplicationDbContext;
            var auditLogs = new List<AuditLog>();
            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.Entity is CourseAuditLog) continue;
                var log = new CourseAuditLog
                {
                    CreatedAt = DateTime.Now,
                    UploadedAt = DateTime.Now,
                };
                if (entry.State == EntityState.Added)
                {
                    addedEntities.Add(entry);
                    //log.NewValue = JsonSerializer.Serialize(entry.CurrentValues.ToObject());
                }
                if (entry.State == EntityState.Modified)
                {
                    log.OldValue = JsonSerializer.Serialize(entry.OriginalValues.ToObject());
                    log.NewValue = JsonSerializer.Serialize(entry.CurrentValues.ToObject());
                    auditLogs.Add(log);
                }
                if (entry.State == EntityState.Deleted)
                {
                    log.OldValue = JsonSerializer.Serialize(entry.OriginalValues.ToObject());
                    auditLogs.Add(log);
                }


            }
            if (auditLogs.Any())
            {
                context.AuditLog.AddRange(auditLogs);
            }
            return base.SavingChanges(eventData, result);
        }

        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            var context = eventData.Context as ApplicationDbContext;




            if (addedEntities.Any())
            {
                var auditLogs = addedEntities.Select(entity => new CourseAuditLog
                {
                    CreatedAt = DateTime.Now,
                    UploadedAt = DateTime.Now
                });
                context.AuditLog.AddRange(auditLogs);
                addedEntities.Clear();
                context.SaveChanges();
            }
            return base.SavedChanges(eventData, result);
        }
    }
}
