﻿using CleanArchitecture.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.ProjectDtos
{
    public class RiskAssessmentDto
    {
        public int ProjectId { get; set; }
        public RiskLevel AssessedRiskLevel { get; set; }
        public string? AssessmentNotes { get; set; }
    }

}