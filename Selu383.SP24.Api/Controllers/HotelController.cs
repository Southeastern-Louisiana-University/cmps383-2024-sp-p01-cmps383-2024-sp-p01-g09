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

        /*public List<Hotel> GetAll()
        {
            return _dataContext.Set<Hotel>().ToList();
        }*/
        public ActionResult<IEnumerable<HotelDto>> ListAllHotels()
        {
            var hotels = _dataContext.Set<Hotel>()
                .Select(x => new HotelDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address
                })
                .ToList();

            return Ok(hotels);
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

        [HttpPost]
        public IActionResult Create(HotelDto hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(hotel.Name)) //|| hotel.Name.Trim() == "string")
            {
                ModelState.AddModelError("Name", "Name is required");
                return BadRequest(ModelState);
            }

            if (hotel.Name.Length > 120)
            {
                ModelState.AddModelError("Name", "The hotel name must not exceed 120 characters");
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(hotel.Address)) // || hotel.Address.Trim() == "string")
            {
                ModelState.AddModelError("Address", "Address is required");
                return BadRequest(ModelState);
            }

            var newHotel = new Hotel
            {
                Name = hotel.Name,
                Address = hotel.Address,
            };

            _dataContext.Add(newHotel);
            _dataContext.SaveChanges();

            var createdHotelDto = new HotelDto
            {
                Id = newHotel.Id,
                Name = newHotel.Name,
                Address = newHotel.Address
            };

            return CreatedAtAction(nameof(newHotel), new { id = newHotel.Id }, createdHotelDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateHotelById(int id, HotelDto updatedHotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(updatedHotel.Name))
            {
                ModelState.AddModelError("Name", "Name is required");
                return BadRequest(ModelState);
            }

            if (updatedHotel.Name.Length > 120)
            {
                ModelState.AddModelError("Name", "Name cannot be longer than 120 characters");
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(updatedHotel.Address))
            {
                ModelState.AddModelError("Address", "Address is required");
                return BadRequest(ModelState);
            }

            var existingHotel = _dataContext
                .Set<Hotel>()
                .FirstOrDefault(x => x.Id == id);

            if (existingHotel == null)
            {
                return NotFound();
            }

            existingHotel.Name = updatedHotel.Name;
            existingHotel.Address = updatedHotel.Address;

            _dataContext.SaveChanges();

            var updatedHotelDto = new HotelDto
            {
                Id = existingHotel.Id,
                Name = existingHotel.Name,
                Address = existingHotel.Address
            };

            return Ok(updatedHotelDto);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteHotel(int id)
        {
            var hotel = _dataContext
                .Set<Hotel>()
                .FirstOrDefault(x => x.Id == id);

            if (hotel == null)
            {
                return NotFound();
            }

            _dataContext.Set<Hotel>().Remove(hotel);
            _dataContext.SaveChanges();

            return Ok(new HotelDto
            {
                Name = hotel.Name,
                Address = hotel.Address,
                Id = hotel.Id
            });
        }
    }
}
