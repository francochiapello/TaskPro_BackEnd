using Microsoft.AspNetCore.Mvc;
using TaskPro.Models.Comentarios;
using TaskPro.Models.Shared;
using TaskPro.Models.Tareas;
using TaskPro.Persistence;
using TaskPro.Security;
using TaskPro.Services;

namespace TaskPro.Controllers
{
    [ServiceFilter(typeof(Authorization))]
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : Controller
    {
        private IComentarioService comentarioService;
        public ComentarioController(IComentarioService comentarioService, ComentarioDAO comentarioDAO,TareaDAO tareaDAO)
        {
            this.comentarioService = comentarioService;
            this.comentarioService.setDao(comentarioDAO,tareaDAO);
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult<List<ComentarioDTO>> GetAllMy()
        {
            try
            {
                if (!HttpContext.Items.TryGetValue("id", out var userId)) throw new UnknownUserException("usuario ingresado es invalido");
                this.comentarioService.setUsuario(Convert.ToInt32(userId));

                var result = this.comentarioService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ComentarioDTO>> GetById(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id)) throw new Exception("El id es necesario");

                var result = await this.comentarioService.getOneByIdAsync(id);

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
        public async Task<ActionResult<ComentarioDTO>> Create([FromBody] CreateComentarioDTO data)
        {
            try
            {
                if (!HttpContext.Items.TryGetValue("id", out var id)) throw new UnknownUserException("usuario ingresado es invalido");
                this.comentarioService.setUsuario(Convert.ToInt32(id));

                if (data is null) throw new Exception("El modelo ingresado es invalido");

                if (ModelState.IsValid)
                {
                    var result = await this.comentarioService.createAsync(data);

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

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ComentarioDTO>> Update(string id, [FromBody] UpdateComentarioDTO data)
        {
            try
            {
                if (!HttpContext.Items.TryGetValue("id", out var userId)) throw new UnknownUserException("usuario ingresado es invalido");
                this.comentarioService.setUsuario(Convert.ToInt32(userId));

                if (string.IsNullOrWhiteSpace(id)) throw new Exception("El id es necesario");

                if (data is null) throw new Exception("El modelo ingresado es invalido");

                if (ModelState.IsValid)
                {
                    var result = await this.comentarioService.updateAsync(id, data);

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
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> Delete(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id)) throw new Exception("El id es necesario");

                await this.comentarioService.deleteAsync(id);

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
