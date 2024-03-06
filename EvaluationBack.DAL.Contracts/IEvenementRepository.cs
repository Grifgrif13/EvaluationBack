using EvaluationBack.Models;

namespace EvaluationBack.DAL.Contracts
{
    public interface IEvenementRepository
    {
        /// <summary>
        /// Obtient tous les évènements dans une liste.
        /// </summary>
        /// <returns>Retourne une liste d'évènement.</returns>
        IQueryable<Evenement> GetAllEvenements();

        /// <summary>
        /// Obtient une application par son id.
        /// </summary>
        /// <param name="idEvenement">Id de l'évènement.</param>
        /// <returns>Retourne l'évènement depuis son id.</returns>
        Task<Evenement> GetEvenementById(int idEvenement);

        /// <summary>
        /// Créé un évènement.
        /// </summary>
        /// <param name="evenement">Évènement que l'on va sauvegarder.</param>
        /// <returns>Retourne le nouvel évènement créé.</returns>
        Task<Evenement> CreateEvenement(Evenement evenement);

        /// <summary>
        /// Met à jour un évènement.
        /// </summary>
        /// <param name="evenement">Evenement dont on va modifier les valeurs.</param>
        /// <returns>Retourne l'évènement modifié.</returns>
        Task<Evenement> UpdateEvenement(Evenement evenement);

        /// <summary>
        /// Supprime un évènement.
        /// </summary>
        /// <param name="application"><see cref="Evenement"/> que l'on veut supprimer.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        Task DeleteEvenement(Evenement application);

    }
}
