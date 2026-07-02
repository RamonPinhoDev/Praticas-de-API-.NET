using APICatologo.Data;
using APICatologo.DTOs;
using APICatologo.DTOs.Mappins;
using APICatologo.Filter;
using APICatologo.Interfaces;
using APICatologo.Models;
using APICatologo.Pagination;
using APICatologo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace APICatologo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _uof;

        public CategoriasController(IUnitOfWork uof)
        {
            _uof = uof;
        }
        //[Authorize]

        /// <summary>
        /// Filtra pelo nome da categoria
        /// </summary>
        /// <param name="categoriaFiltroNome"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("pagination/filtro/nome")]
      
        public async Task< ActionResult<IEnumerable<CategoriasDTO>>> GetCatFiltrado([FromQuery] CategoriaFiltroNome categoriaFiltroNome)
        {
            var categ = await _uof.CategoriaRepository.GetCategoriasFiltroNomeAsync(categoriaFiltroNome);

            var metda = new
            {
                categ.TotalCount,
                categ.PageSize,
                categ.CurrentPage,
                categ.TotalPages,
                categ.HasNext,
                categ.HasPrevious

            };
            Response.Headers.Append("X-pagination", JsonConvert.SerializeObject(metda));

            var categDTO = categ.TocategoriaDtoList();

            return Ok(categDTO);

            // return GetParamters(categ);
        }
        [HttpGet("Pagination")]
        public async Task<ActionResult<IEnumerable<CategoriasDTO>> >GetParamters([FromQuery] CategoriaParametrs cat)
        {
            var categ = await _uof.CategoriaRepository.GetCategoriasAsync(cat);

            var metda = new
            {
                categ.TotalCount,
                categ.PageSize,
                categ.CurrentPage,
                categ.TotalPages,
                categ.HasNext,
                categ.HasPrevious

            };
            Response.Headers.Append("X-pagination", JsonConvert.SerializeObject(metda));

            var categDTO = categ.TocategoriaDtoList();

            return Ok(categDTO);

        }
        /// <summary>
        /// Filtra pelo nome da categoria
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>  

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriasDTO>>> Get()
        {

            var cat = await _uof.CategoriaRepository.GetAllAsync(); 

            var catDTO = cat.TocategoriaDtoList();




            return Ok(catDTO);

        }

        [HttpGet("{id:int}", Name = "ObterRota")]
        public async Task<IActionResult >Get2(int id)
        {

            var cat = await _uof.CategoriaRepository.GetAsync(c => c.CategoriaId == id);

            var catDTO = cat.ToCategoriaDto();

            return Ok(catDTO);

        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put)) ]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoriasDTO>> Put(int id, CategoriasDTO categoriasDTO)
        {
            var catego = categoriasDTO.ToCategoria();


            var cate = _uof.CategoriaRepository.Update(catego);
         await   _uof.CommitAsync();
            var categs = cate.ToCategoriaDto();
            return Ok(cate);
        }
        /// <summary>
        /// Cria categoria
        /// </summary>
        /// <param name="categoriasDTO"></param>
        /// <returns></returns>
        /// <remarks>
        /// Exemplo de resquet :
        /// 
        /// POST api/Categoria
        /// 
        /// {
        /// Nome: teste
        /// 
        /// ImagemUrl: teste
        /// }
        /// 
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<CategoriasDTO> >Post(CategoriasDTO categoriasDTO)
        {

            var categoria = categoriasDTO.ToCategoria();
            _uof.CategoriaRepository.Create(categoria);
            _uof.CommitAsync();

            return new CreatedAtRouteResult("ObterRota", new { Id = categoriasDTO.CategoriaId }, categoriasDTO);

        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoriasDTO>> Delete(int id)
        {
            var categoria = await _uof.CategoriaRepository.GetAsync(c => c.CategoriaId == id);
            _uof.CategoriaRepository.Delete(categoria);

            _uof.CommitAsync();
            var categoriasDTO = categoria.ToCategoriaDto();
            return Ok(categoriasDTO);



        }

    }
}
