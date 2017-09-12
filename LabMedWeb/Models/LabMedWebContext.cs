using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

using static LabMedWeb.Logic.Commands;

namespace LabMedWeb.Models
{
    public class LabMedWebContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public LabMedWebContext() : base("LabMed.Contabilidad")
        {
            if (!Database.Exists() || Categorias.Count() < 3) InitialSetupAsync(this);
            Activo = Categorias.Find(1);
            Pasivo = Categorias.Find(2);
            Capital = Categorias.Find(3);
        }
        /// <summary>
        /// Obtiene una referencia a la categoría de cuentas del Activo.
        /// </summary>
        public readonly Categoria Activo;
        /// <summary>
        /// Obtiene una referencia a la categoría de cuentas del Pasivo.
        /// </summary>
        public readonly Categoria Pasivo;
        /// <summary>
        /// Obtiene una referencia a la categoría de cuentas del Capital.
        /// </summary>
        public readonly Categoria Capital;

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<CuentaGroup> CuentaGroups { get; set; }

        public DbSet<Cuenta> Cuentas { get; set; }
    }
}
