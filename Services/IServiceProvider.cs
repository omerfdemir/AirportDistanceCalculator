using DbModel;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IServiceProvider
    {
        IUnitOfWork DbUnitOfWork { get; }

        IAirportService GetAirportService();
    }
}
