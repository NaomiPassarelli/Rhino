﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woopin.SGC.Common.HtmlModel;
using Woopin.SGC.Model.Tesoreria;

namespace Woopin.SGC.Repositories.Tesoreria
{
    public interface ICuentaBancariaRepository : IRepository<CuentaBancaria>
    {
        IList<CuentaBancaria> GetAllEmiteCheque();
    }
}
