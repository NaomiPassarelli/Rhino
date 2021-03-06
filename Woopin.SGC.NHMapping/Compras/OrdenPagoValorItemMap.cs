﻿using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woopin.SGC.Model.Compras;

namespace Woopin.SGC.NHMapping
{
    public class OrdenPagoValorItemmMap : ClassMap<OrdenPagoValorItem>
    {
        public OrdenPagoValorItemmMap()
        {
            this.Id(c => c.Id).GeneratedBy.Identity();
            this.References(c => c.Valor).Not.Nullable().Not.LazyLoad().Cascade.SaveUpdate();          
        }
    }
}
