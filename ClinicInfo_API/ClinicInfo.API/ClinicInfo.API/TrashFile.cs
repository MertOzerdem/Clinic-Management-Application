using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicInfo.API
{
    public class TrashFile
    {
        //// Return all ServiceTypes which service uses
        //public List<ServiceType> GetServiceTypes(int _ServiceId)
        //{

        //    var serviceOrders = _context.ServiceOrders.Where(p => p.Service.ServiceId == _ServiceId)
        //                        .Include(o => o.ServiceType).ToList();

        //    var results = new List<ServiceType>();

        //    foreach (var a in serviceOrders)
        //    {
        //        results.Add(a.ServiceType);
        //    }
        //    return results;
        //}

        //// Return single ServiceType which service uses
        //public ServiceType GetServiceType(int _ServiceId, int _ServiceTypeId)
        //{
        //    var serviceType = GetServiceTypes(_ServiceId);

        //    var result = serviceType.Where(p => p.ServiceTypeId == _ServiceTypeId).FirstOrDefault();

        //    return result;
        //}
    }
}
