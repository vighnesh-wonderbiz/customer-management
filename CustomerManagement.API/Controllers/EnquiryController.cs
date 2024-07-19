using CustomerManagement.DTO;
using CustomerManagement.IServices;
using CustomerManagement.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnquiryController : ControllerBase
    {
        private readonly IEnquiryServices enquiryServices;

        public EnquiryController(IEnquiryServices _enquiryServices)
        {
            enquiryServices = _enquiryServices;
        }

        // GET: api/<EnquiryController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnquiryDTO>>> Get()
        {
            try
            {
                var enquiries = await enquiryServices.GetAllEnquiriesAsync();
                return Ok(enquiries);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }

        // GET api/<EnquiryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EnquiryDTO>> Get(int id)
        {
            try
            {
                var enquiry = await enquiryServices.GetEnquiryByIdAsync(id);
                return Ok(enquiry);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }

        // POST api/<EnquiryController>
        [HttpPost]
        public async Task<ActionResult<EnquiryDTO>> Post([FromBody] CreateEnquiryDTO createEnquiryDto)
        {
            try
            {
                var enquiry = await enquiryServices.CreateEnquiryAsync(createEnquiryDto);
                return Ok(enquiry);

            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }

        // PUT api/<EnquiryController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<EnquiryDTO>> Put(int id, [FromBody] UpdateEnquiryDTO updateEnquiryDTO)
        {
            try
            {
                if (id != updateEnquiryDTO.EnquiryId) throw new InvalidOperationException("Invalid id provided in request body");

                var updatedEnquiry = await enquiryServices.UpdateEnquiryAsync(id, updateEnquiryDTO);
                return Ok(updatedEnquiry);
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }

        // DELETE api/<EnquiryController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                var isDeleted = await enquiryServices.DeleteEnquiryAsync(id);
                return Ok(isDeleted);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }
    }
}
