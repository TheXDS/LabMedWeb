﻿/*
Copyright © 2017 César Andrés Morgan
Pendiente de licenciamiento
===============================================================================
Este archivo está pensado para uso interno exclusivamente por su autor y otro
personal autorizado. No debe ser distribuido en ningún producto comercial sin
haber antes pasado por un control de calidad adecuado, ni tampoco debe ser
considerado como código listo para producción. El autor se absuelve de toda
responsabilidad y daños causados por el uso indebido de este archivo o de
cualquier parte de su contenido.
*/

using LabMedWeb.Models;
using System;
using System.Threading.Tasks;
using MCART;

namespace LabMedWeb.Logic
{
    public static class Commands
    {
        /// <summary>
        /// Inicializa la base de datos con información básica estructural.
        /// </summary>
        internal static void InitialSetupAsync(LabMedWebContext db)
        {
            db.Database.Initialize(true);
            db.DoTransact(() =>
            {
                /* Estas categorías se crean manualmente ya que son las
                 * raíces de toda la base de categorías, y por lo tanto, no
                 * tienen padre.*/
                db.Categorias.Add(new Categoria() { DisplayName = "Activo", Prefix = 1 });
                db.Categorias.Add(new Categoria() { DisplayName = "Pasivo", Prefix = 2 });
                db.Categorias.Add(new Categoria() { DisplayName = "Capital", Prefix = 3 });
            });
        }

        /// <summary>
        /// Agrega una nueva categoría de cuentas a la base de datos.
        /// </summary>
        /// <param name="db">
        /// Contexto de datos en el cual agregar la información.
        /// </param>
        /// <param name="nombre">Nombre de la categoría.</param>
        /// <param name="parent">Pariente de la categoría.</param>
        /// <param name="prefix">Prefijo de la categoría.</param>
        public static void AddCategoria(this LabMedWebContext db, string nombre, Categoria parent, int prefix)
        {
            if (db.IsNull()) throw new ArgumentNullException(nameof(db));
            if (nombre.IsEmpty()) throw new ArgumentNullException(nameof(nombre));
            if (parent.IsNull()) throw new ArgumentNullException(nameof(parent));
            if (prefix < 1) throw new ArgumentOutOfRangeException(nameof(prefix));
            db.DoTransact(() =>
            {
                //parent.SubCategorias.Add(new Categoria { DisplayName = nombre, Prefix = prefix });
            });
        }

        public static void AddCuenta(this LabMedWebContext db, string nombre, Categoria parent, int prefix, params CuentaGroup[] memberOf)
        {
            if (ReferenceEquals(db, null)) throw new ArgumentNullException(nameof(db));
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentNullException(nameof(nombre));
            if (ReferenceEquals(parent, null)) throw new ArgumentNullException(nameof(parent));
            if (prefix < 1) throw new ArgumentOutOfRangeException(nameof(prefix));
            db.DoTransact(() =>
            {
                Cuenta cuenta = new Cuenta { DisplayName = nombre, Prefix = prefix };
                foreach (var j in memberOf) cuenta.MemberOf.Add(j);
                //parent.Cuentas.Add(cuenta);
            });
        }

        public static void AddCuentaGroup(this LabMedWebContext db, string nombre)
        {
            if (ReferenceEquals(db, null)) throw new ArgumentNullException(nameof(db));
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentNullException(nameof(nombre));
            db.DoTransact(() =>
            {
                //db.CuentaGroups.Add(new CuentaGroup { Name = nombre });
            });
        }
        
        public static void AddPartida(this LabMedWebContext db, string synopsys, params Movimiento[] movimientos)
        {
            if (ReferenceEquals(db, null)) throw new ArgumentNullException(nameof(db));
            if (string.IsNullOrWhiteSpace(synopsys)) throw new ArgumentNullException(nameof(synopsys));
            db.DoTransact(() =>
            {
                Partida partida = new Partida
                {
                    TimeStamp = DateTime.Now,
                    Synopsys = synopsys,
                };
                foreach (var j in movimientos)                
                    partida.Movimientos.Add(j);

                if (!partida.IsValid) throw new ArgumentException(nameof(movimientos));
                //db.LibroDiario.Add(partida);
            });
        }

        /*
        En una base de datos contable, en teoría no deben existir métodos de
        edición ni de borrado. Sin embargo, cabe la posibilidad de mover los
        datos a una base de datos de archivado. Eventualmente, se piensa
        implementar una instrucción especial que hará el trabajo de forma
        totalmente automática.
         */
    }
}