﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woopin.SGC.Common.HtmlModel;
using Woopin.SGC.Model.Common;
using Woopin.SGC.Model.Sueldos;

namespace Woopin.SGC.Repositories.Sueldos
{
    public interface IReciboRepository : IRepository<Recibo>
    {
        IList<Recibo> GetAllByFilter(SelectComboRequest req);
        IList<Recibo> GetAllByFilter(PagingRequest req);
        Recibo GetCompleto(int Id);
        int GetProximoNumeroReferencia();
        Recibo GetReciboAnterior(int IdEmpleado);
        decimal GetMejorRemuneracion(int IdEmpleado);
        decimal[] GetPromedioRemunerativo(int IdEmpleado);
    }
}
