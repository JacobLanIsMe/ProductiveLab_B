﻿namespace prjProductiveLab_B.Dtos
{
    public class TreatmentDto
    {
        public int treatmentId { get; set; }
        public string? ovumSituationName { get; set; }
        public string? ovumSourceName { get; set; }
        public string? ovumOperationName { get; set; }
        public string? spermSituationName { get; set; }
        public string? spermSourceName { get; set; }
        public string? spermOperationName { get; set; }
        public string? spermRetrievalMethodName { get; set; }
        public string? embryoSituationName { get; set; }
        public string? embryoOperationName { get; set; }
    }
}
