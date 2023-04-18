using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class ObservationNote
    {
        public ObservationNote()
        {
            ObservationNoteEmbryoStatuses = new HashSet<ObservationNoteEmbryoStatus>();
            ObservationNoteOperations = new HashSet<ObservationNoteOperation>();
            ObservationNoteOvumAbnormalities = new HashSet<ObservationNoteOvumAbnormality>();
            ObservationNotePhotos = new HashSet<ObservationNotePhoto>();
        }

        public int SqlId { get; set; }
        public Guid ObservationNoteId { get; set; }
        public Guid OvumPickupDetailId { get; set; }
        public DateTime ObservationTime { get; set; }
        public Guid Embryologist { get; set; }
        public int? OvumMaturationId { get; set; }
        public int? ObservationTypeId { get; set; }
        public int? FertilisationResultId { get; set; }
        public int? BlastomereScoreCId { get; set; }
        public int? BlastomereScoreGId { get; set; }
        public int? BlastomereScoreFId { get; set; }
        public int? BlastocystScoreExpansionId { get; set; }
        public int? BlastocystScoreIceId { get; set; }
        public int? BlastocystScoreTeId { get; set; }
        public decimal? Kidscore { get; set; }
        public string? Memo { get; set; }
        public int? Pgtanumber { get; set; }
        public string? Pgtaresult { get; set; }
        public string? Pgtmresult { get; set; }
        public int Day { get; set; }
        public bool IsDeleted { get; set; }

        public virtual BlastocystScoreExpansion? BlastocystScoreExpansion { get; set; }
        public virtual BlastocystScoreIce? BlastocystScoreIce { get; set; }
        public virtual BlastocystScoreTe? BlastocystScoreTe { get; set; }
        public virtual BlastomereScoreC? BlastomereScoreC { get; set; }
        public virtual BlastomereScoreF? BlastomereScoreF { get; set; }
        public virtual BlastomereScoreG? BlastomereScoreG { get; set; }
        public virtual Employee EmbryologistNavigation { get; set; } = null!;
        public virtual FertilisationResult? FertilisationResult { get; set; }
        public virtual ObservationType? ObservationType { get; set; }
        public virtual OvumMaturation? OvumMaturation { get; set; }
        public virtual OvumPickupDetail OvumPickupDetail { get; set; } = null!;
        public virtual ICollection<ObservationNoteEmbryoStatus> ObservationNoteEmbryoStatuses { get; set; }
        public virtual ICollection<ObservationNoteOperation> ObservationNoteOperations { get; set; }
        public virtual ICollection<ObservationNoteOvumAbnormality> ObservationNoteOvumAbnormalities { get; set; }
        public virtual ICollection<ObservationNotePhoto> ObservationNotePhotos { get; set; }
    }
}
