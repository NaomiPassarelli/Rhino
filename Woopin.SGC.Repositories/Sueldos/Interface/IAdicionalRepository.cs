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
    public interface IAdicionalRepository : IRepository<Adicional>
    {
        IList<Adicional> GetAllByFilter(SelectComboRequest req);
        IList<Adicional> GetAllByFilter(PagingRequest req);
    }
}
