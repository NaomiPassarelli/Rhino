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
    public class Recibo : ISecuredEntity
    {
        public virtual int Id { get; set; }
        [DoNotValidateOnlyId]
        public virtual Empleado Empleado { get; set; }
        //[DoNotValidate]
        //del empleado porque si se llega a modificar el sind, obra social o bco deposito del mismo
            //despues el recibo va a tener informacion erronea
        public virtual string Sindicato { get; set; }

        [DisplayName("Obra Social")]
        //[DoNotValidate]
        public virtual string ObraSocial { get; set; }
        [DisplayName("Banco de Deposito")]
        //[DoNotValidate]
        public virtual string BancoDeposito { get; set; }
        public virtual Organizacion Organizacion { get; set; }
        public virtual IList<AdicionalRecibo> AdicionalesRecibo { get; set; }
        [DisplayName("Total Remunerativo")]
        public virtual decimal TotalRemunerativo { get; set; }
        [DisplayName("Total No Remunerativo")]
        public virtual decimal TotalNoRemunerativo { get; set; }
        [DisplayName("Total Descuento")]
        public virtual decimal TotalDescuento { get; set; }
        public virtual decimal Total { get; set; }
        [Required(ErrorMessage = "Es Necesario una fecha de creación")]
        [DisplayName("Fecha Creación")]
        public virtual DateTime FechaCreacion { get; set; }
        [DisplayName("Fecha Inicio")]
        public virtual DateTime FechaInicio { get; set; }
        [DisplayName("Fecha Fin")]
        public virtual DateTime FechaFin { get; set; }
        [Required(ErrorMessage = "Es Necesario un Período")]
        [DisplayName("Período")]
        public virtual string Periodo { get; set; }
        [DisplayName("Fecha de Pago")]
        [Required(ErrorMessage = "Es Necesario una Fecha de Pago")]
        public virtual DateTime FechaPago { get; set; } //en la que se va a pagar este recibo

        [DisplayName("Período Anterior")] 
        public virtual string PeriodoAnterior { get; set; }
        [DisplayName("Fecha de Pago Anterior")]
        public virtual DateTime? FechaPagoAnterior { get; set; }

        [DisplayName("Observación")]
        public virtual string Observacion { get; set; }
        [DisplayName("Numero Referencia")]
        public virtual int NumeroReferencia { get; set; }
        public virtual string DomicilioEmpresa { get; set; }

        //Estado - creado, pagado, habria que ver si se puede anular
        //Asiento

        [DisplayName("Tipo de Recibo")]
        public virtual TypeRecibo TipoRecibo { get; set; }
        [DisplayName("Dias Trabajados")]
        public virtual int DiasTrabajados { get; set; } 
        //El resto lo puede sacar de los adicionales
        [DisplayName("Remuneración Básica")]
        [Required(ErrorMessage = "Es Necesario una Remuneración Básica")]
        public virtual decimal RemuneracionBasica { get; set; }
        public virtual bool Activo { get; set; }
        [DoNotValidate]
        public virtual Asiento Asiento { get; set; }

        public Recibo()
        {
            TipoRecibo = TypeRecibo.Sueldo;
            FechaCreacion = DateTime.Now;
            Activo = true;
        }
    }
    public enum TypeRecibo
    {
        Sueldo = 0,
        SAC = 1,
        Despido = 2,
        Renuncia = 3,
        Vacaciones = 4
    }

}
