using System.Linq;
using Selu383.SP24.Api.Entities;
using Selu383.SP24.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Selu383.SP24.Api.Features;



namespace Selu383.SP24.Api.Controllers
{
    [ApiController]
    [Route("api/hotels")]
    public class HotelController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public HotelController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        [HttpGet("all")]

        public List<Hotel> GetAll()
        {
           
            return _dataContext.Set<Hotel>().ToList();
        }

        [HttpGet("{id:int}")]
        public ActionResult<Hotel> GetById(int id)
        {
            var hotel = _dataContext
                .Hotels
                .FirstOrDefault(x => x.Id == id);

            if (hotel == null)
            {
                return NotFound("hotel does not exist");
            }

            return hotel;
        }
    }
}
