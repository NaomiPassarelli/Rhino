﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woopin.SGC.Model.Tesoreria;

namespace Woopin.SGC.Repositories.Tesoreria
{
    public interface IChequeRepository : IRepository<Cheque>
    {
        IList<Cheque> GetAllChequesEnCartera();

        IList<Cheque> GetChequeFilter(int IdCliente, DateTime start, DateTime end, FilterCheque filter, int IdBanco);

    }
}
