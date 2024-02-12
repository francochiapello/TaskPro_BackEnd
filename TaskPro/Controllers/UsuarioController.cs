using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using TaskPro.Helpers;
using TaskPro.Models.Shared;
using TaskPro.Models.Usuarios;
using TaskPro.Security;
using TaskPro.Services;

namespace TaskPro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService usuarioService;
        private readonly TokenHelper tokenHelper;
        public UsuarioController(IUsuarioService usuarioService, IConfiguration configuration) 
        {
            this.usuarioService = usuarioService;
            this.tokenHelper = new TokenHelper(configuration["Jwt:Key"], "", "");

        }
        [ServiceFilter(typeof(Authorization))]
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<List<UsuarioDTO>>> GetAll()
        {
            try
            {
                var result = await this.usuarioService.getAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [ServiceFilter(typeof(Authorization))]
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<List<simpleList>>> GetAllSimpleList()
        {
            try
            {
                var result = await this.usuarioService.getSimpleList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [ServiceFilter(typeof(Authorization))]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UsuarioDTO>> GetById(int id)
        {
            try
            {
                HttpContext.Items.TryGetValue("dni", out var userId);

                if (id == 0) throw new Exception("El id no puede ser igual a 0");

                var result = await this.usuarioService.getOneAsync(id);

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
        [ServiceFilter(typeof(Authorization))]
        [HttpGet("[action]")]
        public async Task<ActionResult<UsuarioDTO>> GetProfile()
        {
            try
            {
                if (!HttpContext.Items.TryGetValue("id", out var userId)) throw new UnknownUserException("usuario ingresado es invalido");

                if (Convert.ToInt32(userId) == 0) throw new Exception("El id no puede ser igual a 0");

                var result = await this.usuarioService.getOneAsync(Convert.ToInt32(userId));

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
        [ServiceFilter(typeof(Authorization))]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<UsuarioDTO>> Create([FromBody] CreateUsuarioDTO data)
        {
            try
            {
                if (data is null) throw new Exception("El modelo ingresado es invalido");

                if (ModelState.IsValid)
                {
                    var result = await this.usuarioService.createAsync(data);

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

        [ServiceFilter(typeof(Authorization))]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UsuarioDTO>> Update(int id, [FromBody] UsuarioDTO data)
        {
            try
            {
                if (id == 0) throw new Exception("El dni no puede ser igual a 0");

                if (data is null) throw new Exception("El modelo ingresado es invalido");

                if (ModelState.IsValid)
                {
                    var result = await this.usuarioService.updateAsync(id, data);

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
        [ServiceFilter(typeof(Authorization))]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                if (id == 0) throw new Exception("El dni no puede ser igual a 0");

                this.usuarioService.deleteAsync(id);

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
        [AllowAnonymous]
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<string>> Register([FromBody] RegisterDTO data)
        {
            try
            {
                if (data is null) throw new Exception("El modelo no es valido");
                if (ModelState.IsValid)
                {
                    var result = await this.usuarioService.registerAsync(data);

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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO login)
        {
            try
            {
                if (login is null) throw new Exception("El modelo ingresado no es valido");

                if (ModelState.IsValid)
                {
                    var result = await this.usuarioService.loginAsync(login);

                    var token = tokenHelper.GenerateToken(result.Id.ToString());
                    return Ok(token);
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
            catch (ValidationException ex)
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
    }
}
