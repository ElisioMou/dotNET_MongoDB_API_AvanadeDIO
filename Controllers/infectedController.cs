using DOTNET_MONGODB_API.Data.Collections;
using DOTNET_MONGODB_API.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace DOTNET_MONGODB_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectadoController : ControllerBase
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<infected> _infectadosCollection;

        public InfectadoController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _infectadosCollection = _mongoDB.DB.GetCollection<infected>(typeof(infected).Name.ToLower());
        }

        [HttpPost]
        public ActionResult SalvarInfectado([FromBody] infectedDTO dto)
        {
            var infectado = new infected(dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);

            _infectadosCollection.InsertOne(infectado);
            
            return StatusCode(201, "Infectado adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult ObterInfectados()
        {
            var infectados = _infectadosCollection.Find(Builders<infected>.Filter.Empty).ToList();
            
            return Ok(infectados);
        }
    }
}