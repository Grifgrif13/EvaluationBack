using EvaluationBack.DAL.Contracts;
using EvaluationBack.Models;
using Microsoft.EntityFrameworkCore;

namespace EvaluationBack.DAL.Repository
{
    public class EvenementRepository : IEvenementRepository
    {
        private readonly DbContextEntity dbContext;

        public EvenementRepository(DbContextEntity dbContext) 
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        /// <exception cref="Exception"></exception>
        public async Task<Evenement> CreateEvenement(Evenement evenement)
        {
            try
            {
                var eventSaved = this.dbContext.Evenement.Add(evenement);
                await this.dbContext.SaveChangesAsync();

                return eventSaved.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <inheritdoc/>
        /// <exception cref="Exception"></exception>
        public async Task DeleteEvenement(Evenement evenement)
        {
            try
            {
                this.dbContext.Evenement.Remove(evenement);
                await this.dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <inheritdoc/>
        /// <exception cref="Exception"></exception>
        public IQueryable<Evenement> GetAllEvenements()
        {
            try
            {
                return this.dbContext.Evenement.AsQueryable();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <inheritdoc/>
        /// <exception cref="Exception"></exception>
        public async Task<Evenement> GetEvenementById(int idEvenement)
        {
            try
            {
                return await this.dbContext.Evenement.SingleAsync(p => p.Id == idEvenement);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }

        /// <inheritdoc/>
        /// <exception cref="Exception"></exception>
        public async Task<Evenement> UpdateEvenement(Evenement evenement)
        {
            try
            {
                this.dbContext.Evenement.Update(evenement);
                await this.dbContext.SaveChangesAsync();

                return evenement;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
