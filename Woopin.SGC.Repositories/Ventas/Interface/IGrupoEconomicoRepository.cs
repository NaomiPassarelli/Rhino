﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woopin.SGC.Common.HtmlModel;
using Woopin.SGC.Model.Ventas;

namespace Woopin.SGC.Repositories.Ventas
{
    public interface IGrupoEconomicoRepository : IRepository<GrupoEconomico>
    {
        IList<GrupoEconomico> GetAllByFilter(SelectComboRequest req);
    }
}
