namespace WebApplication3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Configuration;

    [Table("evaluation")]
    public partial class evaluation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EvalID { get; set; }

        public int? ScheduleID { get; set; }

        public int? StudentIDWriter { get; set; }

        public int? StudentIDReceiver { get; set; }


       
        public int? Knowledge_Score { get; set; }


        [Range(1, 4, ErrorMessage = "Please assure your inputs have been selected.")]
        public int? CriticalThinkingProblemSolving_Score { get; set; }

        [Range(1, 4, ErrorMessage = "Please assure your inputs have been selected.")]
        public int? InnovativeEntrepreneurialSkills_Score { get; set; }

        [Range(1, 4, ErrorMessage = "Please assure your inputs have been selected.")]
        public int? CollaborationLeadership_Score { get; set; }

        [Range(1, 4, ErrorMessage = "Please assure your inputs have been selected.")]
        public int? Communication_Score { get; set; }

        [Range(1, 4, ErrorMessage = "Please assure your inputs have been selected.")]
        public int? Intercultural_Score { get; set; }

        [Range(1, 4, ErrorMessage = "Please assure your inputs have been selected.")]
        public int? DevelopmentInAsia_Score { get; set; }

        [Range(1, 4, ErrorMessage = "Please assure your inputs have been selected.")]
        public int? EthicsSocialResponsibility_Score { get; set; }

        [Range(1, 4, ErrorMessage = "Please assure your inputs have been selected.")]
        public int? ResiliencePositivity_Score { get; set; }

        [Range(1, 4, ErrorMessage = "Please assure your inputs have been selected.")]
        public int? SelfdirectednessMetalearning_Score { get; set; }

        public DateTime? CompletionTime { get; set; }
    }
}
