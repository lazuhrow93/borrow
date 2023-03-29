﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Borrow.Models;

namespace Borrow.Data
{
    public class BorrowContext : DbContext
    {
        public BorrowContext (DbContextOptions<BorrowContext> options)
            : base(options)
        {
        }

        public DbSet<Borrow.Models.User> User { get; set; } = default!;
    }
}
