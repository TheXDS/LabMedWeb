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

using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace LabMedWeb.Logic
{
    /// <summary>
    /// Extensiones genéricas especiales para objetos <see cref="DbContext"/>.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Permite utilizar una transacción, ya sea una existente o una nueva.
        /// </summary>
        /// <param name="db">
        /// <see cref="DbContext"/> en el cual se desea efectuar una 
        /// transacción.
        /// </param>
        /// <returns>
        /// Un <see cref="DbContextTransaction"/> que representa la transacción
        /// obtenida.
        /// </returns>
        public static DbContextTransaction GetTransaction(this DbContext db) => db.Database.CurrentTransaction ?? db.Database.BeginTransaction();
        /// <summary>
        /// Permite utilizar una transacción, ya sea una existente o una nueva,
        /// e indica si fue creada.
        /// </summary>
        /// <param name="db">
        /// <see cref="DbContext"/> en el cual se desea efectuar una 
        /// transacción.
        /// </param>
        /// <param name="IsNew">
        /// Parámetro de salida. Un valor de <c>true</c> indica que la
        /// transacción es nueva, un valor de <c>false</c> indica que la
        /// transacción devuelta es una transacción existente.
        /// </param>
        /// <returns>
        /// Un <see cref="DbContextTransaction"/> que representa la transacción
        /// obtenida.
        /// </returns>
        public static DbContextTransaction GetTransaction(this DbContext db, out bool IsNew)
        {
            if (ReferenceEquals(db.Database.CurrentTransaction, null))
            {
                IsNew = true;
                return db.Database.BeginTransaction();
            }
            IsNew = false;
            return db.Database.CurrentTransaction;
        }
        /// <summary>
        /// Obtiene una transacción y ejecuta una acción relacionada al
        /// <see cref="DbContext"/> especificado, cometiendo los datos al
        /// final de la operación.
        /// </summary>
        /// <param name="Db">
        /// <see cref="DbContext"/> en el cual se desea efectuar una 
        /// transacción.
        /// </param>
        /// <param name="action">
        /// Acción a ejecutar, será ejecutada asíncronamente con espera.
        /// </param>
        public static void DoTransact(this DbContext Db, Action action)
        {
            DbContextTransaction tr = Db.GetTransaction(out bool isNew);
            try
            {
                action.Invoke();
                Db.SaveChanges();
                if (isNew) tr.Commit();
            }
            catch
            {
                if (isNew) tr.Rollback();
                throw;
            }
            finally
            {
                if (isNew) tr.Dispose();
                tr = null;
            }
        }

    }
}