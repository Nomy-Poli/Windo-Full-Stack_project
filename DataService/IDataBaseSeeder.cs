using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataService
{
    public interface IDataBaseSeeder
    {
        void SeedAsync(ModelBuilder modelBuilder);
    }
}
