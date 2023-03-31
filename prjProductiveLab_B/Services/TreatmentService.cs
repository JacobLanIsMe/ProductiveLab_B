using Microsoft.EntityFrameworkCore;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Interfaces;
using prjProductiveLab_B.Models;
using System.Transactions;

namespace prjProductiveLab_B.Services
{
    public class TreatmentService : ITreatmentService
    {
        private readonly ReproductiveLabContext dbContext;
        public TreatmentService(ReproductiveLabContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public BaseResponseDto AddOvumPickupNote(AddOvumPickupNoteDto ovumPickupNote)
        {
            BaseResponseDto result = new BaseResponseDto();
            string errorMessage = OvumPickupNoteValidation(ovumPickupNote);
            if (string.IsNullOrEmpty(errorMessage))
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        OvumPickup ovumPickup = new OvumPickup()
                        {
                            CourseOfTreatmentId = Guid.Parse(ovumPickupNote.courseOfTreatmentId),
                            TriggerTime = (DateTime)ovumPickupNote.operationTime.triggerTime,
                            StartTime = (DateTime)ovumPickupNote.operationTime.startTime,
                            EndTime = (DateTime)ovumPickupNote.operationTime.endTime,
                            CocGrade5 = ovumPickupNote.ovumPickupNumber.coc_Grade5,
                            CocGrade4 = ovumPickupNote.ovumPickupNumber.coc_Grade4,
                            CocGrade3 = ovumPickupNote.ovumPickupNumber.coc_Grade3,
                            CocGrade2 = ovumPickupNote.ovumPickupNumber.coc_Grade2,
                            CocGrade1 = ovumPickupNote.ovumPickupNumber.coc_Grade1,
                            Embryologist = Guid.Parse(ovumPickupNote.embryologist),
                            UpdateTime = DateTime.Now
                        };
                        dbContext.OvumPickups.Add(ovumPickup);
                        dbContext.SaveChanges();
                        Guid latestOvumPickupId = dbContext.OvumPickups.OrderByDescending(x=>x.SqlId).Select(x=>x.OvumPickupId).FirstOrDefault();
                        int ovumTotalNumber = ovumPickupNote.ovumPickupNumber.coc_Grade1 + ovumPickupNote.ovumPickupNumber.coc_Grade2 + ovumPickupNote.ovumPickupNumber.coc_Grade3 + ovumPickupNote.ovumPickupNumber.coc_Grade4 + ovumPickupNote.ovumPickupNumber.coc_Grade5;
                        for (int i = 1; i<=ovumTotalNumber; i++)
                        {
                            OvumPickupDetail ovumPickupDetail = new OvumPickupDetail()
                            {
                                OvumPickupId = latestOvumPickupId,
                                OvumNumber = i,
                                OvumPickupDetailStatusId = 1,
                                FertilizationStatusId = 1
                            };
                            dbContext.Add(ovumPickupDetail);
                        }
                        dbContext.SaveChanges();
                        scope.Complete();
                        
                    }
                    result.SetSuccess();
                }
                catch(TransactionAbortedException ex)
                {
                    result.SetError(ex.Message);
                }

            }
            else
            {
                result.SetError(errorMessage);
            }
            return result;



        }
        // OvumPickupNote 表格的驗證，若有錯誤回傳錯誤訊息，若無錯誤回傳空字串。
        public string OvumPickupNoteValidation(AddOvumPickupNoteDto ovumPickupNote)
        {
            string errorMessage = "";
            if (!Guid.TryParse(ovumPickupNote.courseOfTreatmentId, out Guid transformedCourseId))
            {
                errorMessage += "會員有誤，請重新選擇客戶。\n";
            }
            if (ovumPickupNote.operationTime == null)
            {
                errorMessage += "操作時間有誤。\n";
            }
            if (ovumPickupNote.ovumPickupNumber == null)
            {
                errorMessage += "取卵結果有誤。\n";
            }
            else
            {
                int coc_grade5 = ovumPickupNote.ovumPickupNumber.coc_Grade5;
                int coc_grade4 = ovumPickupNote.ovumPickupNumber.coc_Grade4;
                int coc_grade3 = ovumPickupNote.ovumPickupNumber.coc_Grade3;
                int coc_grade2 = ovumPickupNote.ovumPickupNumber.coc_Grade2;
                int coc_grade1 = ovumPickupNote.ovumPickupNumber.coc_Grade1;
                if ((coc_grade1 + coc_grade2 + coc_grade3 + coc_grade4 + coc_grade5) != ovumPickupNote.ovumPickupNumber.totalOvumNumber)
                {
                    errorMessage += "取卵數總和有誤。\n";
                }
            }
            if (!Guid.TryParse(ovumPickupNote.embryologist, out Guid embryologist))
            {
                errorMessage += "胚胎師選項有誤。\n";
            }
            return errorMessage;
        }

        public async Task<BaseTreatmentInfoDto> GetBaseTreatmentInfo(Guid courseOfTreatmentId)
        {
            var result = await dbContext.CourseOfTreatments.Where(x => x.CourseOfTreatmentId == courseOfTreatmentId).Select(x => new BaseTreatmentInfoDto
            {
                courseOfTreatmentSqlId = x.SqlId,
                customerSqlId = x.Customer.SqlId,
                customerName = x.Customer.Name,
                birthday = x.Customer.Birthday,
                spouseSqlId = x.Customer.SpouseNavigation.SqlId,
                spouseName = x.Customer.SpouseNavigation.Name,
                doctor = x.DoctorNavigation.Name,
                treatmentName = x.Treatment.Name,
                memo = x.Memo
            }).AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<TreatmentSummaryDto>> GetTreatmentSummary(Guid courseOfTreatmentId)
        {
            return await dbContext.OvumPickupDetails.Where(x => x.OvumPickup.CourseOfTreatmentId == courseOfTreatmentId).Select(x => new TreatmentSummaryDto
            {
                ovumPickupDetailId = x.OvumPickupDetailId,
                courseOfTreatmentSqlId = x.OvumPickup.CourseOfTreatment.SqlId,
                ovumFromCourseOfTreatmentSqlId = dbContext.CourseOfTreatments.Where(z=>z.CourseOfTreatmentId == x.OvumPickup.CourseOfTreatment.OvumFromCourseOfTreatmentId).Select(z=>z.SqlId).FirstOrDefault(),
                ovumPickupDetailStatus = x.OvumPickupDetailStatus.Name,
                dateOfEmbryo = (DateTime.Now.Date - x.OvumPickup.CourseOfTreatment.SurgicalTime.Date).Days,
                ovumNumber = x.OvumNumber,
                fertilizationStatus = x.FertilizationStatus.Name,
                observationNote = dbContext.ObservationNotes.Where(y => y.OvumPickupDetailId == x.OvumPickupDetailId).OrderByDescending(y => y.SqlId).Select(y => y.Note).FirstOrDefault()
            }).OrderBy(x=>x.ovumNumber).AsNoTracking().ToListAsync();
        }

        public async Task<List<TreatmentDto>> GetAllTreatment()
        {
            var treatments = await dbContext.Treatments.Select(x => new TreatmentDto
            {
                treatmentId = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.treatmentId).AsNoTracking().ToListAsync();
            return treatments;
        }

        public async Task<BaseResponseDto> AddCourseOfTreatment(AddCourseOfTreatmentDto input)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    CourseOfTreatment course = new CourseOfTreatment
                    {
                        Doctor = input.doctorId,
                        CustomerId = input.customerId,
                        TreatmentId = input.treatmentId,
                        SurgicalTime = input.surgicalTime,
                        TreatmentStatusId = 1,
                        Memo = input.memo,
                    };
                    dbContext.CourseOfTreatments.Add(course);
                    dbContext.SaveChanges();
                    scope.Complete();
                }
                result.SetSuccess();
            }
            catch(Exception ex)
            {
                result.SetError(ex.Message);
            }
            return result;
        }
    }
}
