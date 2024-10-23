using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyBasket.API.Entities
{
    public class AuditLogger
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey(nameof(UpatedByUser))]
        public string? UpdatedByUserId {  get; set; }

        [ForeignKey(nameof(CreatedByUserId))]
        public string CreatedByUserId { get; set; }

        public ApplicationUser CreatedByUser { get; set; } = null!;
        public ApplicationUser? UpatedByUser { get; set; }

    }
}
