using CustomerManagement.DTO;
using CustomerManagement.IServices;
using CustomerManagement.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusServices statusServices;

        public StatusController(IStatusServices _statusServices)
        {
            statusServices = _statusServices;
        }

        // GET: api/<StatusController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusDTO>>> Get()
        {
            try
            {
                var status = await statusServices.GetAllStatusesAsync();
                return Ok(status);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET api/<StatusController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StatusDTO>> Get(int id)
        {
            try
            {
                var status = await statusServices.GetStatusByIdAsync(id);
                return Ok(status);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST api/<StatusController>
        [HttpPost]
        public async Task<ActionResult<Status>> Post([FromBody] CreateStatusDTO createStatusDTO)
        {
            try
            {
                var status = await statusServices.CreateStatusAsync(createStatusDTO);
                return Ok(status);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }

        // PUT api/<StatusController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<StatusDTO>> Put(int id, [FromBody] UpdateStatusDTO updateStatusDTO)
        {
            try
            {
                if (id != updateStatusDTO.StatusId) throw new InvalidOperationException("Invalid id provided in request body");

                var updatedStatus = await statusServices.UpdateStatusAsync(id, updateStatusDTO);
                return Ok(updatedStatus);
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

        // DELETE api/<StatusController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                var isDeleted = await statusServices.DeleteStatusAsync(id);
                return Ok(isDeleted);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }
    }
}
