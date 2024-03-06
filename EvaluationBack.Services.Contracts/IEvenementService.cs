using EvaluationBack.Models;
using EvaluationBack.Services.Contracts.DTO.Down;
using EvaluationBack.Services.Contracts.DTO.Up;

namespace EvaluationBack.Services.Contracts
{
    public interface IEvenementService
    {
        /// <summary>
        /// Instanciation une nouvelle application.
        /// </summary>
        /// <param name="evenementDto">Évènement que l'on va sauvegarder.</param>
        /// <returns>Retourne l'évènement créé.</returns>
        Task<EvenementDownDetailedDto> SaveEvenement(EvenementUpDto evenementDto);

        /// <summary>
        /// Suppression d'un évènement.
        /// </summary>
        /// <param name="idEvenement">Identifiant de l'évènement à supprimer.</param>
        /// <returns>Retourne une <see cref="Task"/>.</returns>
        Task DeleteEvenement(int idEvenement);


        /// <summary>
        /// Récupérer un évènement par son Identifiant.
        /// </summary>
        /// <param name="idEvenement">Identifiant de l'évènement.</param>
        /// <returns>Retourne l'évènement dans un DTO.</returns>
        Task<EvenementDownDetailedDto> GetEvenementById(int idEvenement);


        /// <summary>
        /// Récupère tous les évènements.
        /// </summary>
        /// <returns>Retourne la liste des évènements.</returns>
        Task<List<Evenement>> GetAllEvenements();

        /// <summary>
        /// Update a profile.
        /// </summary>
        /// <param name="profileUpdateUpDto">Profile information to modify.</param>
        /// <returns>Returns the profile updated.</returns>
        Task<EvenementDownDetailedDto> UpdateEvenement(EvenementUpDetailedDto profileUpdateUpDto);
    }
}
