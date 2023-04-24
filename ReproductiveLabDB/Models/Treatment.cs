using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class Treatment
    {
        public Treatment()
        {
            CourseOfTreatments = new HashSet<CourseOfTreatment>();
        }

        public int SqlId { get; set; }
        public int? OvumSituationId { get; set; }
        public int? OvumSourceId { get; set; }
        public int? OvumOperationId { get; set; }
        public int? SpermSituationId { get; set; }
        public int? SpermSourceId { get; set; }
        public int? SpermOperationId { get; set; }
        public int? SpermRetrievalMethodId { get; set; }
        public int? EmbryoSituationId { get; set; }
        public int? EmbryoOperationId { get; set; }

        public virtual GermCellOperation? EmbryoOperation { get; set; }
        public virtual GermCellSituation? EmbryoSituation { get; set; }
        public virtual GermCellOperation? OvumOperation { get; set; }
        public virtual GermCellSituation? OvumSituation { get; set; }
        public virtual GermCellSource? OvumSource { get; set; }
        public virtual GermCellOperation? SpermOperation { get; set; }
        public virtual SpermRetrievalMethod? SpermRetrievalMethod { get; set; }
        public virtual GermCellSituation? SpermSituation { get; set; }
        public virtual GermCellSource? SpermSource { get; set; }
        public virtual ICollection<CourseOfTreatment> CourseOfTreatments { get; set; }
    }
}
