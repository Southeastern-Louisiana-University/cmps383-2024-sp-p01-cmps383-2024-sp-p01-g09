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
            _dataContext = dataContext;
        }

        [HttpGet]

        public IActionResult GetAll()
        {
           
            var hotelsFromDatabase = _dataContext
                .Hotels
                .Select(x => new HotelDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address
                })
                .ToList();
            
            return Ok(new List<Hotel>());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var hotel = _dataContext
                .Hotels
                .FirstOrDefault(x => x.Id == id);

            if (hotel == null)
            {
                return NotFound("hotel does not exist");
            }

            var hotelGetDto = new HotelDto 
            {
                
                Id = hotel.Id,
                Name = hotel.Name,
                Address = hotel.Address
            
            };

              = hotelGetDto;

            return Ok();
        }
    }
}
