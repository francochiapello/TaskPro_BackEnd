using Microsoft.AspNetCore.Mvc;
using TaskPro.Models.Proyectos;
using TaskPro.Models.Shared;
using TaskPro.Models.Tareas;
using TaskPro.Persistence;
using TaskPro.Services;

namespace TaskPro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : Controller
    {
        private readonly ITareaService tareaService;
        public TareaController(ITareaService tareaService,TareaDAO tarea,ComentarioDAO comentarioDAO) 
        {
            this.tareaService = tareaService;
            this.tareaService.setDao(tarea,comentarioDAO);
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult<List<TareaDTO>> GetAll()
        {
            try
            {
                var result = this.tareaService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("[action]/{id:int}")]
        public async Task<ActionResult<List<TareaDTO>>> FindByProyectId(int id)
        {
            try
            {
                var result = await this.tareaService.findByProyect(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<TareaDTO>> GetById([FromRoute] string id)
        {
            try
            {
                //HttpContext.Items.TryGetValue("dni", out var userId);

                if (string.IsNullOrWhiteSpace(id)) throw new Exception("El id es necesario");

                var result = await this.tareaService.getOneByIdAsync(id);

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
        public async Task<ActionResult<TareaDTO>> Create([FromBody] CreateTareaDTO data)
        {
            try
            {
                //if (!HttpContext.Items.TryGetValue("dni", out var dni)) throw new UnknownUserException("usuario ingresado invalido");

                if (data is null) throw new Exception("El modelo ingresado es invalido");

                if (ModelState.IsValid)
                {
                    var result = await this.tareaService.createAsync(data);

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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TareaDTO>> Update([FromHeader] string id, [FromBody] UpdateTareaDTO data)
        {
            try
            {
                //if (!HttpContext.Items.TryGetValue("dni", out var dni)) throw new UnknownUserException("usuario ingresado invalido");

                if (string.IsNullOrWhiteSpace(id)) throw new Exception("El id es necesario");

                if (data is null) throw new Exception("El modelo ingresado es invalido");

                if (ModelState.IsValid)
                {
                    var result = await this.tareaService.updateAsync(id, data);

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
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> Delete([FromHeader] string id)
        {
            try
            {
                //if (!HttpContext.Items.TryGetValue("dni", out var dni)) throw new UnknownUserException("usuario ingresado invalido");

                if (string.IsNullOrWhiteSpace(id)) throw new Exception("El id es necesario");

                await this.tareaService.deleteAsync(id);

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
