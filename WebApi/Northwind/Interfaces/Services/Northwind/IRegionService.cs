#region << Usings >>

using ThatConference.Northwind.Common.DataTransferObjects.Northwind;
using ThatConference.Northwind.Common.Filters.Northwind;
using ThatConference.Services.Common.Interfaces;

#endregion

namespace ThatConference.Northwind.Interfaces.Services.Northwind
{
    /// <summary>
    /// This interface defines all methods available for the generic Digimon Entity Service.
    /// </summary>
    public interface IRegionService :
        ISelectableService<RegionDto, RegionFilter>,
        ISaveableWithBulkInsertService<RegionFilter, RegionDto, RegionDto>
    {

    }
}