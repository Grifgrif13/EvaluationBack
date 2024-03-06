namespace EvaluationBack.Models
{
    public class Evenement
    {
        /// <summary>
        /// Obtient ou définit l'id de l'évènement.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit le nom de l'évènement.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Obtient ou définit la description de l'évènement.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Obtient ou définit la date de l'évènement.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Obtient ou définit le lieu de l'évènement.
        /// </summary>
        public string Location { get; set; }
    }
}
