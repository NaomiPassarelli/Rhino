﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woopin.SGC.Common.Validations;
using Woopin.SGC.Model.Common;
using Woopin.SGC.Model.Contabilidad;

namespace Woopin.SGC.Model.Sueldos
{
    public class Adicional : ISecuredEntity
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        [DisplayName("Descripción")]
        public virtual string Descripcion { get; set; }
        public virtual string AdditionalDescription { get; set; }
        [RegularExpression("^[0-9]+[0-9]*(.[0-9]{0,2})?$", ErrorMessage = "El Porcentaje debe ser un número mayor a cero, puede contener el caracter punto (.) y con dos decimales")]
        public virtual decimal? Porcentaje { get; set; }
        [RegularExpression("^[0-9]+[0-9]*(.[0-9]{0,2})?$", ErrorMessage = "El Valor debe ser un número mayor a cero, puede contener el caracter punto (.) y con dos decimales")]
        public virtual decimal? Valor { get; set; }
        public virtual bool Suma { get; set; }

        [DoNotValidate]
        public virtual Organizacion Organizacion { get; set; }
        [Required(ErrorMessage = "El tipo es requerido")]
        [DisplayName("Tipo de Liquidacion")]
        public virtual TypeLiquidacion TipoLiquidacion { get; set; }
        [DoNotValidate]
        public virtual Cuenta Cuenta { get; set; }

        public virtual bool OnlyAutomatic { get; set; } //true se puede agregar solo automaticamente por el sistema

        public Adicional()
        {
            this.OnlyAutomatic = false;
        }

        
    }
    public enum TypeLiquidacion
    {
        Remunerativo = 0,
        NoRemunerativo = 1,
        Descuento = 2
    }
    
}
