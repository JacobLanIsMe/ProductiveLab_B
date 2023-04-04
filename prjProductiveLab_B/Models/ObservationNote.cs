using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class ObservationNote
    {
        public ObservationNote()
        {
            ObservationNotePhotos = new HashSet<ObservationNotePhoto>();
        }

        public int SqlId { get; set; }
        public Guid ObservationNoteId { get; set; }
        public Guid OvumPickupDetailId { get; set; }
        public DateTime ObservationTime { get; set; }
        public Guid Embrologist { get; set; }
        public int OvumMaturationId { get; set; }
        public int ObservationTypeId { get; set; }
        public int OvumAbnormalityId { get; set; }
        public int FertilisationResultId { get; set; }
        public int BlastomereScoreCId { get; set; }
        public int BlastomereScoreGId { get; set; }
        public int BlastomereScoreFId { get; set; }
        public int EmbryoStatusId { get; set; }
        public int BlastocystScoreExpantionId { get; set; }
        public int BlastocystScoreIceId { get; set; }
        public int BlastocystScoreTeId { get; set; }
        public int Kidscore { get; set; }
        public string? Memo { get; set; }
        public int OperationTypeId { get; set; }
        public string? Pgtanumber { get; set; }
        public string? Pgtaresult { get; set; }
        public string? Pgtmresult { get; set; }

        public virtual BlastocystScoreExpantion BlastocystScoreExpantion { get; set; } = null!;
        public virtual BlastocystScoreIce BlastocystScoreIce { get; set; } = null!;
        public virtual BlastocystScoreTe BlastocystScoreTe { get; set; } = null!;
        public virtual BlastomereScoreC BlastomereScoreC { get; set; } = null!;
        public virtual BlastomereScoreF BlastomereScoreF { get; set; } = null!;
        public virtual BlastomereScoreG BlastomereScoreG { get; set; } = null!;
        public virtual Employee EmbrologistNavigation { get; set; } = null!;
        public virtual EmbryoStatus EmbryoStatus { get; set; } = null!;
        public virtual FertilisationResult FertilisationResult { get; set; } = null!;
        public virtual ObservationType ObservationType { get; set; } = null!;
        public virtual OperationType OperationType { get; set; } = null!;
        public virtual OvumAbnormality OvumAbnormality { get; set; } = null!;
        public virtual OvumMaturation OvumMaturation { get; set; } = null!;
        public virtual OvumPickupDetail OvumPickupDetail { get; set; } = null!;
        public virtual ICollection<ObservationNotePhoto> ObservationNotePhotos { get; set; }
    }
}
