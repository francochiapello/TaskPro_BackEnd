using Microsoft.AspNetCore.Mvc;
using TaskPro.Models.Proyectos;
using TaskPro.Models.Shared;
using TaskPro.Models.Usuarios;
using TaskPro.Services;

namespace TaskPro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoController : Controller
    {
        private readonly IProyectoService proyectoService;
        public ProyectoController(IProyectoService proyectoService)
        {
            this.proyectoService = proyectoService;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<List<ProyectoDTO>>> GetAll()
        {
            try
            {
                var result = await this.proyectoService.getAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProyectoDTO>> GetById(int id)
        {
            try
            {
                //HttpContext.Items.TryGetValue("dni", out var userId);

                if (id == 0) throw new Exception("El id no puede ser igual a 0");

                var result = await this.proyectoService.getOneByIdAsync(id);

                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ProyectoDTO>> Create([FromBody] CreateProyectoDTO data)
        {
            try
            {
                //if (!HttpContext.Items.TryGetValue("dni", out var dni)) throw new UnknownUserException("usuario ingresado invalido");

                if (data is null) throw new Exception("El modelo ingresado es invalido");

                if (ModelState.IsValid)
                {
                    var result = await this.proyectoService.createAsync(data);

                    return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
                }
                else
                {
                    var modelErrors = ModelState.AllErrors();
                    return BadRequest(modelErrors);
                }
            }
            catch (AlreadyExistException ex)
            {
                return StatusCode(409, ex.Message);
            }
            catch (UnknownUserException ex)
            {
                return StatusCode(401, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProyectoDTO>> Update(int id, [FromBody] UpdateProyectoDTO data)
        {
            try
            {
                //if (!HttpContext.Items.TryGetValue("dni", out var dni)) throw new UnknownUserException("usuario ingresado invalido");

                if (id == 0) throw new Exception("El dni no puede ser igual a 0");

                if (data is null) throw new Exception("El modelo ingresado es invalido");

                if (ModelState.IsValid)
                {
                    var result = await this.proyectoService.updateAsync(id, data);

                    return Ok(result);
                }
                else
                {
                    var modelErrors = ModelState.AllErrors();
                    return BadRequest(modelErrors);
                }
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AlreadyExistException ex)
            {
                return StatusCode(409, ex.Message);
            }
            catch (UnknownUserException ex)
            {
                return StatusCode(401, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<bool> Delete(int id)
        {
            try
            {
                //if (!HttpContext.Items.TryGetValue("dni", out var dni)) throw new UnknownUserException("usuario ingresado invalido");

                if (id == 0) throw new Exception("El dni no puede ser igual a 0");

                this.proyectoService.delete(id);

                return Ok(true);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnknownUserException ex)
            {
                return StatusCode(401, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
