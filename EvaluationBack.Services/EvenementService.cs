using EvaluationBack.DAL.Contracts;
using EvaluationBack.Models;
using EvaluationBack.Services.Contracts;
using EvaluationBack.Services.Contracts.DTO.Down;
using EvaluationBack.Services.Contracts.DTO.Up;
using System.ComponentModel.DataAnnotations;

namespace EvaluationBack.Services
{
    public class EvenementService : IEvenementService
    {
        private readonly IEvenementRepository evenementRepository;

        public EvenementService(IEvenementRepository profileRepository)
        {
            this.evenementRepository = evenementRepository;
        }

        // <inheritdoc/>
        public async Task DeleteEvenement(int idEvenement)
        {
            var evenement = this.evenementRepository.GetEvenementById(idEvenement).Result;

            await this.evenementRepository.DeleteEvenement(evenement);
        }

        // <inheritdoc/>
        public async Task<List<Evenement>> GetAllEvenements()
        {
            return await Task.Run(() =>
            {
                return this.evenementRepository.GetAllEvenements().ToList();
            });
        }

        // <inheritdoc/>
        public async Task<EvenementDownDetailedDto> GetEvenementById(int idEvenement)
        {
            var evenement = await this.evenementRepository.GetEvenementById(idEvenement);

            return new EvenementDownDetailedDto()
            {
                IdEvenement = evenement.Id,
                Title = evenement.Title,
                Description = evenement.Description,
                Date = evenement.Date,
                Location = evenement.Location,
            };
        }

        // <inheritdoc/>
        /// <exception cref="Exception"></exception>
        public async Task<EvenementDownDetailedDto> SaveEvenement(EvenementUpDto evenementDto)
        {
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(evenementDto, new ValidationContext(evenementDto), validationResults))
            {
                throw new Exception("Tous les champs doivent être renseignés");
            }

            Evenement evenement = new Evenement()
            {
                Title = evenementDto.Title!,
                Description = evenementDto.Description!,
                Date = evenementDto.Date!.Value,
                Location = evenementDto.Location!,
            };

            var evenementCreated = await this.evenementRepository.CreateEvenement(evenement);

            return new EvenementDownDetailedDto()
            {
                IdEvenement = evenementCreated.Id,
                Title = evenementCreated.Title,
                Description = evenementCreated.Description,
                Date = evenementCreated.Date,
                Location = evenementCreated.Location,
            };
        }

        // <inheritdoc/>
        /// <exception cref="Exception"></exception>
        public async Task<EvenementDownDetailedDto> UpdateEvenement(EvenementUpDetailedDto evenementUpdateUpDto)
        {
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(evenementUpdateUpDto, new ValidationContext(evenementUpdateUpDto), validationResults))
            {
                throw new Exception("Tous les champs doivent être renseignés");
            }

            var existingEvenement = await this.evenementRepository.GetEvenementById(evenementUpdateUpDto.IdEvenement!.Value) ;

            existingEvenement.Title = evenementUpdateUpDto.Title!;
            existingEvenement.Description = evenementUpdateUpDto.Description!;
            existingEvenement.Date = evenementUpdateUpDto.Date!.Value;
            existingEvenement.Location = evenementUpdateUpDto.Location!;

            var updatedEvenement = await evenementRepository.UpdateEvenement(existingEvenement);

            return new EvenementDownDetailedDto()
            {
                IdEvenement = updatedEvenement.Id,
                Title = updatedEvenement.Title,
                Description = updatedEvenement.Description,
                Date = updatedEvenement.Date,
                Location = updatedEvenement.Location,
            };
        }
    }
}
