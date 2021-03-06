﻿using System;
using FluentNHibernate.Mapping;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woopin.SGC.Model.Compras;

namespace Woopin.SGC.NHMapping
{
    public class OrdenPagoMap : ClassMap<OrdenPago>
    {
        public OrdenPagoMap()
        {
            this.Id(c => c.Id).GeneratedBy.Identity();
            this.Map(c => c.Detalle).Nullable().Not.LazyLoad();
            this.Map(c => c.Estado).Not.Nullable().Not.LazyLoad();
            this.Map(c => c.Total).Not.Nullable().Not.LazyLoad();
            this.Map(c => c.Fecha).Not.Nullable().Not.LazyLoad();
            this.Map(c => c.FechaCreacion).Not.Nullable().Not.LazyLoad();
            this.References(c => c.Proveedor).Not.Nullable().Not.LazyLoad();
            this.References(c => c.Tipo).Not.Nullable().Not.LazyLoad();
            this.References(c => c.Asiento).Nullable().LazyLoad();
            this.References(c => c.Usuario).Not.Nullable().LazyLoad();
            this.References(c => c.Organizacion).Not.Nullable().LazyLoad(); 
            this.HasMany(c => c.Comprobantes).AsBag().KeyColumn("OrdenPago_Id").Cascade.AllDeleteOrphan();
            this.HasMany(c => c.Pagos).AsBag().KeyColumn("OrdenPago_Id").Cascade.AllDeleteOrphan();
        }
    }
}
