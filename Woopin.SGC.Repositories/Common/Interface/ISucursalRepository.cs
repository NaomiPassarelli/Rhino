﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woopin.SGC.Model.Common;
using Woopin.SGC.Model.Contabilidad;

namespace Woopin.SGC.Repositories.Common
{
    public interface ISucursalRepository : IRepository<Sucursal>
    {
        void SetDefault(int Id);
    }
}
