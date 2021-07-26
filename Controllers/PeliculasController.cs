using APIPeliculas.Models;
using APIPeliculas.Models.DTOs;
using APIPeliculas.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace APIPeliculas.Controllers
{
    [Authorize]
    [Route("api/Peliculas")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ApiPeliculas")]
    public class PeliculasController : Controller
    {
        private readonly IPeliculaRepository _pelRepo;
        private readonly IWebHostEnvironment _hostEnvironment;
        private IMapper _mapper;

        public PeliculasController(IPeliculaRepository pelRepo, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _pelRepo = pelRepo;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetPeliculas()
        {
            var listaPeliculas = _pelRepo.GetPeliculas();

            var listaPeliculasDTO = new List<PeliculaDTO>();

            foreach (var lista in listaPeliculas)
            {
                listaPeliculasDTO.Add(_mapper.Map<PeliculaDTO>(lista));
            }

            return Ok(listaPeliculasDTO);
        }


        [AllowAnonymous]
        [HttpGet("{peliculaId:int}",Name = "GetPelicula")]
        public IActionResult GetPelicula(int PeliculaId)
        {
            var itemPelicula = _pelRepo.GetPelicula(PeliculaId);
            if(itemPelicula == null)
            {
                return NotFound();
            }
            var itemPeliculaDTO = _mapper.Map<PeliculaDTO>(itemPelicula);
            return Ok(itemPeliculaDTO);
        }


        [AllowAnonymous]
        [HttpGet("GetPeliculasEnCategoria/{categoriaId:int}")]
        public IActionResult GetPeliculasEnCategoria(int categoriaId)
        {
            var listaPelicula = _pelRepo.GetPeliculasEnCategoria(categoriaId);

            if(listaPelicula == null)
            {
                return NotFound();
            }

            var itemPelicula = new List<PeliculaDTO>();
            foreach(var item in listaPelicula)
            {
                itemPelicula.Add(_mapper.Map<PeliculaDTO>(item));
            }
            return Ok(itemPelicula);
        }


        [AllowAnonymous]
        [HttpGet("Buscar")]
        public IActionResult Buscar(String nombre)
        {
            try
            {
                var resultado = _pelRepo.BuscarPelicula(nombre);
                if (resultado.Any())
                {
                    return Ok(resultado);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error recuperando datos de la aplicaion.");
            }
        }



        [HttpPost]
        public IActionResult CrearPelicula([FromForm]PeliculaCreateDTO PeliculaDto)
        {
            if(PeliculaDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_pelRepo.ExistePelicula(PeliculaDto.Nombre))
            { 
                ModelState.AddModelError("","La Pelicula ya existe.");
                return StatusCode(404, ModelState);
            }

            //Subida de archivo.
            var archivo = PeliculaDto.Foto;
            String rutaPrincipal = _hostEnvironment.WebRootPath;
            var archivos = HttpContext.Request.Form.Files;

            if(archivo.Length > 0)
            {
                //Nueva imagen
                var nombreFoto = Guid.NewGuid().ToString();
                var subidas = Path.Combine(rutaPrincipal, @"Fotos");
                var extension = Path.GetExtension(archivos[0].FileName);

                using (var fileStreams = new FileStream(Path.Combine(subidas, nombreFoto + extension), FileMode.Create))
                {
                    archivos[0].CopyTo(fileStreams);
                }
                PeliculaDto.RutaImagen = @"/Fotos/" + nombreFoto + extension;
            }
            

            var Pelicula = _mapper.Map<Pelicula>(PeliculaDto);

            if (!_pelRepo.CrearPelicula(Pelicula))
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el registro{Pelicula.Nombre}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetPelicula",new { PeliculaId = Pelicula.Id },Pelicula);
        }


        [HttpPatch("{PeliculaId:int}", Name = "ActualizarPelicula")]
        public IActionResult ActualizarPelicula(int PeliculaId,[FromBody]PeliculaDTO PeliculaDTO)
        {
            if(PeliculaDTO==null || PeliculaId!=PeliculaDTO.Id)
            {
                return BadRequest(ModelState);
            }
            var Pelicula = _mapper.Map<Pelicula>(PeliculaDTO);
            if(!_pelRepo.ActualizarPelicula(Pelicula))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro{Pelicula.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        [HttpDelete("{PeliculaId:int}", Name = "BorrarPelicula")]
        public IActionResult BorrarPelicula(int PeliculaId)
        {
            if (!_pelRepo.ExistePelicula(PeliculaId))
            {
                return NotFound();
            }
            var Pelicula = _pelRepo.GetPelicula(PeliculaId);
            if (!_pelRepo.BorrarPelicula(Pelicula))
            {
                ModelState.AddModelError("", $"Algo salio mal borrando el registro{Pelicula.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
