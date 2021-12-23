using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tienda.Datos;

namespace Tienda.Negocio
{
    internal class Connection
    {
        private static TiendaEntities _dbTienda;

        public static TiendaEntities DBTienda
        {
            get
            {
                if (_dbTienda == null)
                {
                    _dbTienda = new TiendaEntities();
                }
                return _dbTienda;
            }
        }
    }
}
