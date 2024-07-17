using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherCreation.DAL
{
    public partial class PiVMSContext
    {
        bool IsConnectionString { get; set; }
        string ConnectionString { get; set; }

        public PiVMSContext(string ConnectionString)
        {
            if (string.IsNullOrEmpty(ConnectionString) == false)
            {
                IsConnectionString = true;
                this.ConnectionString = ConnectionString;
            }
        }


        protected void OnModelCreatingCustom(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }



    }

}
