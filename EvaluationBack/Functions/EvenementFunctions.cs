using System.Net;
using System.Text;
using System.Text.Json;
using EvaluationBack.Services.Contracts;
using EvaluationBack.Services.Contracts.DTO.Up;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace EvaluationBack.Azure.Functions
{
    public class EvenementFunctions
    {
        private readonly ILogger logger;
        private readonly IEvenementService evenementService;

        public EvenementFunctions(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<EvenementFunctions>();
        }

        [Function("Function1")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }

        /// <summary>
        /// Réccupérer un évènement par son Id.
        /// </summary>
        /// <param name="req">Requête entrante.</param>
        /// <param name="id">Identifiant unique de l'évènement.</param>
        /// <returns>Retour de la fonction.</returns>
        [Function("GetEventById")]
        public async Task<HttpResponseData> GetEventById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Evenements/{id}")] HttpRequestData req, int id)
        {
            logger.LogInformation("C# HTTP trigger function processed a request for application with id: {id}.", id);
            string errorMessage = "Error event by id:";

            var response = req.CreateResponse();

            try
            {
                var application = await this.evenementService.GetEvenementById(id);

                if (application == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                }
                else
                {
                    await response.WriteAsJsonAsync(application);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("{errorMessage} {ex.Message}", errorMessage, ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                response.Body = new MemoryStream(Encoding.UTF8.GetBytes($"{errorMessage} {ex.Message}"));
            }

            return response;
        }

        /// <summary>
        /// Supprimer un évènement.
        /// </summary>
        /// <param name="req">Requête entrante.</param>
        /// <param name="id">Identifiant de l'évènement à delete.</param>
        /// <returns>Retourne une <see cref="Task"/> de type <seealso cref="HttpResponseData"/>.</returns>
        [Function("DeleteEvenement")]
        public async Task<HttpResponseData> DeleteEvenement([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "Evenements/{id}")] HttpRequestData req, int id)
        {
            this.logger.LogInformation("Starting of DeleteEvenement method");
            string errorMessage = "Error deleting identifiants:";

            var response = req.CreateResponse(HttpStatusCode.NoContent);

            try
            {
                await this.evenementService.DeleteEvenement(id);
            }

            catch (Exception ex)
            {
                this.logger.LogError("{errorMessage} {ex.Message}", errorMessage, ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.WriteString($"{errorMessage} {ex.Message}");
            }

            return response;
        }

        /// <summary>
        /// Récupère tous les évènements.
        /// </summary>
        /// <param name="req">Requête entrante.</param>
        /// <returns>Retourne le retour de la fonction.</returns>
        [Function("GetAllEvenements")]
        public async Task<HttpResponseData> GetAllEvenements(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Evenements")] HttpRequestData req)
        {
            logger.LogInformation("C# HTTP trigger function processed a request to get all evenements.");
            string errorMessage = "Error getting all evenements:";

            var response = req.CreateResponse();

            try
            {
                var evenements = await this.evenementService.GetAllEvenements();
                await response.WriteAsJsonAsync(evenements);
            }

            catch (Exception ex)
            {
                logger.LogError("{errorMessage} {ex.Message}", errorMessage, ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.WriteString($"{errorMessage} {ex.Message}");
            }

            return response;
        }

        /// <summary>
        /// Mise à jour d'un profil.
        /// </summary>
        /// <param name="req">Requête entrante.</param>
        /// <returns>Retourne l'évènement mis à jour.</returns>
        [Function("UpdateEvenement")]
        public async Task<HttpResponseData> UpdateEvenement(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "Evenements/")] HttpRequestData req,
        FunctionContext executionContext)
        {
            string errorMessage = "Error updating identifiants:";

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var evenement = JsonSerializer.Deserialize<EvenementUpDetailedDto>(requestBody);

            var response = req.CreateResponse();

            try
            {
                var updatedEvenement = await this.evenementService.UpdateEvenement(evenement!);

                await response.WriteAsJsonAsync(updatedEvenement);
            }

            catch (Exception ex)
            {
                logger.LogError("{errorMessage} {ex.Message}", errorMessage, ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.WriteString($"{errorMessage} {ex.Message}");
            }

            return response;
        }

        /// <summary>
        /// Creation d'un évènement.
        /// </summary>
        /// <param name="req">Requête entrante.</param>
        /// <returns>Retourne une <see cref="Task"/> de type <seealso cref="HttpResponseData"/>.</returns>
        [Function("CreateEvenements")]
        public async Task<HttpResponseData> CreateEvenements([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Evenements")] HttpRequestData req)
        {
            var response = req.CreateResponse();
            string errorMessage = "Error saving identifiants:";

            string requestBody;
            using (var reader = new StreamReader(req.Body, Encoding.UTF8))
            {
                requestBody = await reader.ReadToEndAsync();
            }
            try
            {
                var evenement = JsonSerializer.Deserialize<EvenementUpDto>(requestBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var savedEvenement = await this.evenementService.SaveEvenement(evenement!);
                await response.WriteAsJsonAsync(savedEvenement);
                response.StatusCode = HttpStatusCode.Created;
            }

            catch (JsonException ex)
            {
                this.logger.LogError("{errorMessage} {ex.Message}", errorMessage, ex.Message);
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                response.Body = new MemoryStream(Encoding.UTF8.GetBytes($"Bad input in argument {errorMessage} {ex.Message}"));
            }

            catch (Exception ex)
            {
                this.logger.LogError("{errorMessage} {ex.Message}", errorMessage, ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                response.Body = new MemoryStream(Encoding.UTF8.GetBytes($"{errorMessage} {ex.Message}"));
            }

            return response;
        }
    }
}
