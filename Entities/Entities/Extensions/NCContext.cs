using NetCore.Core.Boundaries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Core.Entities
{
    partial class NCContext
    {
        private readonly IUsernameProvider _usernameProvider = null;

        public NCContext(DbContextOptions<NCContext> options, IUsernameProvider usernameProvider) : base(options)
        {
            _usernameProvider = usernameProvider;
        }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified)
                .GroupBy(x => x.State, x => x.Entity);

            var now = DateTime.Now;
            string currentUser = _usernameProvider?.GetUsername() ?? "System";

            foreach (var entityState in entities)
            {
                foreach (var item in entityState)
                {
                    if (entityState.Key == EntityState.Added)
                    {
                        this.SetPropertyValue(item, "CreatedDate", now);
                    }

                    this.SetPropertyValue(item, "ModifiedDate", now);
                    this.SetPropertyValue(item, "ModifiedBy", currentUser);
                }
            }

            return base.SaveChanges();
        }


        private void SetPropertyValue(object entity, string propertyName, object value)
        {
            var entityType = entity.GetType();

            var property = entityType.GetProperty(propertyName);

            if (property != null)
            {
                property.SetValue(entity, value);
            }
        }

    }
}
