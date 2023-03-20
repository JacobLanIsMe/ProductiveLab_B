﻿namespace prjProductiveLab_B.Dtos
{
    public class FunctionDto
    {
        public int functionId { get; set; }
        public string? name { get; set; }
        public string? route { get; set; }
        public int functionTypeId { get; set; }
        public List<FunctionDto>? subFunctions { get; set; }
    }
    
    
}
