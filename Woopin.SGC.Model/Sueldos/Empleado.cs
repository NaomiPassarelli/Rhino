﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woopin.SGC.Common.Validations;
using Woopin.SGC.Model.Common;

namespace Woopin.SGC.Model.Sueldos
{
    public class Empleado : ISecuredEntity
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Es Necesario un Nombre")]
        public virtual string Nombre { get; set; }

        [Required(ErrorMessage = "Es Necesario un Apellido")]
        public virtual string Apellido { get; set; }

        [DisplayName("Dirección")]
        public virtual string Direccion { get; set; }

        [DisplayName("Número")]
        public virtual string Numero { get; set; }
        public virtual string Piso { get; set; }
        public virtual string Departamento { get; set; }

        [DisplayName("Codigo Postal")]
        public virtual string CodigoPostal { get; set; }

        [DoNotValidate]
        public virtual Localizacion Localizacion { get; set; }

        [DoNotValidate]
        public virtual ComboItem Nacionalidad { get; set; }

        [DisplayName("Estado Civil")]
        [DoNotValidate]
        public virtual ComboItem EstadoCivil { get; set; }

        [DoNotValidate]
        public virtual ComboItem Sexo { get; set; }

        public virtual string Telefono { get; set; }

        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
        ErrorMessage = "El email no es valido.")]
        public virtual string Email { get; set; }

        [Required(ErrorMessage = "Es Necesario un CUIT")]
        [RegularExpression(@"[0-9]{2}-[0-9]{8}-[0-9]{1}",
        ErrorMessage = "El CUIT no es valido.")]
        public virtual string CUIT { get; set; }

        [Required(ErrorMessage = "Es Necesario un DNI")]
        [RegularExpression(@"[0-9]{8}",
        ErrorMessage = "El DNI no es valido.")]
        public virtual string DNI { get; set; }

        public virtual bool Activo { get; set; }

        [DoNotValidate]
        public virtual Organizacion Organizacion { get; set; }

        [DisplayName("Sueldo Bruto Mensual")]
        [RegularExpression("^[0-9]+[0-9]*(.[0-9]{0,2})?$", ErrorMessage = "El Sueldo Bruto Mensual debe ser un número mayor a cero, puede contener el caracter punto (.) y con dos decimales")]
        public virtual decimal? SueldoBrutoMensual { get; set; }

        [DisplayName("Sueldo Bruto Hora")]
        [RegularExpression("^[0-9]+[0-9]*(.[0-9]{0,2})?$", ErrorMessage = "El Sueldo Bruto Hora debe ser un número mayor a cero, puede contener el caracter punto (.) y con dos decimales")]
        public virtual decimal? SueldoBrutoHora { get; set; }

        [DisplayName("Fecha de Ingreso")]
        [Required(ErrorMessage = "Es Necesario una Fecha de Ingreso")]
        public virtual DateTime FechaIngreso { get; set; }

        [DisplayName("Fecha de Nacimiento")]
        public virtual DateTime? FechaNacimiento { get; set; }

        [DoNotValidate]
        public virtual ComboItem Categoria { get; set; }

        [DoNotValidate]
        public virtual ComboItem Tarea { get; set; }

        [DoNotValidate]
        public virtual ComboItem Sindicato { get; set; }

        [DisplayName("Obra Social")]
        [DoNotValidate]
        public virtual ComboItem ObraSocial { get; set; }

        public Empleado()
        {
            this.Activo = true;
        }
    }
}