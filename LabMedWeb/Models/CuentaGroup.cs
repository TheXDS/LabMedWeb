/*
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

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LabMedWeb.Models
{
    /// <summary>
    /// Modelo de una tabla que define un grupo que puede utilizarse para ver
    /// un conjunto específico de cuentas.
    /// </summary>
    public class CuentaGroup
    {
        /// <summary>
        /// Campo llave principal de este elemento.
        /// </summary>
        [Key] public int ID { get; set; }
        /// <summary>
        /// Nombre del grupo de cuentas.
        /// </summary>
        [Display(Name="Nombre de grupo")]public string Name { get; set; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="CuentaGroup"/>.
        /// </summary>
        public CuentaGroup() { }
    }
}