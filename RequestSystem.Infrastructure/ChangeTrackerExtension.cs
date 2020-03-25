using Domain.BaseEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class ChangeTrackerExtension
    {
        public static void ApplyBaseEntityInformation(this ChangeTracker changeTracker)
        {
            foreach(var entry in changeTracker.Entries())
            {
                if (!(entry.Entity is BaseEntity baseEntity)) continue;

                var now = DateTime.Now;
                switch(entry.State)
                {
                    case EntityState.Modified:
                        baseEntity.EditDate = now;
                        break;
                    case EntityState.Added:
                        baseEntity.CreateDate = now;
                        break;
                }
            }
        }
    }
}
