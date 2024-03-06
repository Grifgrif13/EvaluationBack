using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluationBack.Services.Contracts.DTO.Up
{
    public class EvenementUpDto
    {
        /// <summary>
        /// Obtient ou définit le nom de l'évènement.
        /// </summary>
        [Required]
        public string? Title { get; set; }

        /// <summary>
        /// Obtient ou définit la description de l'évènement.
        /// </summary>
        [Required]
        public string? Description { get; set; }

        /// <summary>
        /// Obtient ou définit la date de l'évènement.
        /// </summary>
        [Required]
        public DateTime? Date { get; set; }

        /// <summary>
        /// Obtient ou définit le lieu de l'évènement.
        /// </summary>
        [Required]
        public string? Location { get; set; }
    }
}
