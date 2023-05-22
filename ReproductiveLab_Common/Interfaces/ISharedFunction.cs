using ReproductiveLab_Common.Dtos.ForFreezeSummary;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Interfaces
{
    public interface ISharedFunction
    {
        void ThrowExceptionIfNull<T>(T item, string errorMessage);
        List<GetOvumFreezeSummaryDto> GetOvumDetailInfos(IQueryable<OvumDetail> ovumDetails);
        void ConvertPhotoToBase64String(List<GetOvumFreezeSummaryDto> result);
    }
}
