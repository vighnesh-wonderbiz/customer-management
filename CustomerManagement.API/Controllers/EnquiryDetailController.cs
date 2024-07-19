using CustomerManagement.DTO;
using CustomerManagement.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerManagement.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EnquiryDetailController : ControllerBase
    {
        private readonly IEnquiryDetailServices enquiryDetailServices;

        public EnquiryDetailController(IEnquiryDetailServices _enquiryDetailServices)
        {
            enquiryDetailServices = _enquiryDetailServices;
        }

        // GET: api/<EnquiryDetailController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnquiryDetailDTO>>> Get()
        {
            try
            {
                var enquiryDetails = await enquiryDetailServices.GetAllEnquiryDetailsAsync();
                return Ok(enquiryDetails);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }

        // GET api/<EnquiryDetailController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EnquiryDetailDTO>> Get(int id)
        {
            try
            {
                var enquiryDetail = await enquiryDetailServices.GetEnquiryDetailByIdAsync(id);
                return Ok(enquiryDetail);
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

        // POST api/<EnquiryDetailController>
        [HttpPost]
        public async Task<ActionResult<EnquiryDetailDTO>> Post([FromBody] CreateEnquiryDetailDTO createEnquiryDetailDTO)
        {
            try
            {
                var enquiryDetail = await enquiryDetailServices.CreateEnquiryDetailAsync(createEnquiryDetailDTO);
                return Ok(enquiryDetail);

            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }

        // PUT api/<EnquiryDetailController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<EnquiryDetailDTO>> Put(int id, [FromBody] UpdateEnquiryDetailDTO updateEnquiryDetailDTO)
        {
            try
            {
                if (id != updateEnquiryDetailDTO.EnquiryDetailsId) throw new InvalidOperationException("Invalid id provided in request body");

                var updatedEnquiryDetail = await enquiryDetailServices.UpdateEnquiryDetailAsync(id, updateEnquiryDetailDTO);
                return Ok(updatedEnquiryDetail);
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

        // DELETE api/<EnquiryDetailController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                var isDeleted = await enquiryDetailServices.DeleteEnquiryDetailAsync(id);
                return Ok(isDeleted);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }
    }
}
