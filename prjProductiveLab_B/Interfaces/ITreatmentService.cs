﻿using prjProductiveLab_B.Dtos;

namespace prjProductiveLab_B.Interfaces
{
    public interface ITreatmentService
    {
        BaseResponseDto AddOvumPickupNote(AddOvumPickupNoteDto ovumPickupNote);
        Task<BaseTreatmentInfoDto> GetBaseTreatmentInfo(Guid courseOfTreatmentId);
        Task<List<TreatmentSummaryDto>> GetTreatmentSummary(Guid courseOfTreatmentId);
        Task<List<TreatmentDto>> GetAllTreatment();
        Task<BaseResponseDto> AddCourseOfTreatment(AddCourseOfTreatmentDto input);
        Task<BaseResponseDto> AddOvumFreeze(AddOvumFreezeDto input);
        Task<Guid> GetOvumOwnerCustomerId(Guid courseOfTreatmentId);
        Task<BaseCustomerInfoDto> GetOvumOwnerInfo(Guid courseOfTreatmentId);
        Task<List<CommonDto>> GetTopColors();
    }
}
