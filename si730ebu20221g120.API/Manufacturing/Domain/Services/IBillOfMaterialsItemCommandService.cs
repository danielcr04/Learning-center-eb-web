using si730ebu20221g120.API.Manufacturing.Domain.Model.Aggregates;
using si730ebu20221g120.API.Manufacturing.Domain.Model.Commands;

namespace si730ebu20221g120.API.Manufacturing.Domain.Services;

public interface IBillOfMaterialsItemCommandService
{
    Task<BillOfMaterialsItem> Handle(CreateBillOfMaterialsItemCommand command);
}