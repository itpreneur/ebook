﻿namespace EBook.API.Controllers
{
    using AutoMapper;
    using EBook.API.Models.Dto;
    using EBook.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class EBooksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEBooksService _eBooksService;

        public EBooksController(IMapper mapper, IEBooksService eBooksService)
        {
            _mapper = mapper;
            _eBooksService = eBooksService;
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchByTitle([FromQuery]string title, [FromQuery]bool fuzzy = false)
        {
            if (string.IsNullOrEmpty(title))
                return BadRequest();

            // @TODO:
            // - Replace if with a map
            var books = fuzzy
                ? await _eBooksService.FuzzySearchByTitle(title)
                : await _eBooksService.SearchByTitle(title);

            var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);

            return Ok(booksDto);
        }
    }
}