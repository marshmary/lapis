using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lapis.API.Dtos;
using Lapis.API.Helpers;
using Lapis.API.Models;
using Lapis.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Lapis.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        private readonly IImgBBHelper _imgBBHelper;
        private readonly IJwtGenerator _jwtGenerator;

        public ImagesController(IImageService imageService, IMapper mapper, IImgBBHelper imgBBHelper, IJwtGenerator jwtGenerator)
        {
            _imageService = imageService;
            _mapper = mapper;
            _imgBBHelper = imgBBHelper;
            _jwtGenerator = jwtGenerator;
        }

        /// <summary>
        /// Get all image using paginations filters
        /// </summary>
        /// <param name="paginationParameters"></param>
        /// <returns>PaginationResponse</returns>
        [HttpGet]
        public async Task<ActionResult<PaginationResponse<ICollection<ImageReadDto>>>> GetAllImages([FromQuery] PaginationParameters paginationParameters)
        {
            (long totalRecord, IEnumerable<Image> imageItems) = await _imageService.GetAll(paginationParameters);

            var rs = new PaginationResponse<ICollection<ImageReadDto>>()
            {
                TotalRecord = totalRecord,
                Payload = _mapper.Map<ICollection<ImageReadDto>>(imageItems)
            };

            return Ok(rs);
        }

        /// <summary>
        /// Get an image detail by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetImageById")]
        public async Task<ActionResult<Image>> GetImageById(string id)
        {
            var imageItem = await _imageService.GetById(id);
            if (imageItem == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ImageReadDto>(imageItem));
        }

        /// <summary>
        /// Upload new image to DB and image source
        /// </summary>
        /// <param name="imageCreateDto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateImage([FromForm] ImageCreateDto imageCreateDto)
        {
            (bool verify, string message) = FileExtensionCheckHelper.CheckImageExtension(imageCreateDto.Image);

            if (verify == false)
            {
                return BadRequest(new ResponseDto(400, message));
            }

            // Get user Id from user access token
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var principal = _jwtGenerator.GetPrincipalFromExpiredToken(_bearer_token);
            var claims = principal.Identities.First().Claims.ToList();
            var userIdFromAccessToken = claims?.FirstOrDefault(c => c.Type.EndsWith("id", StringComparison.CurrentCultureIgnoreCase))?.Value;

            var imageModel = _mapper.Map<Image>(imageCreateDto);

            // Upload image to ImgBB
            Stream stream = imageCreateDto.Image.OpenReadStream();
            (string thumbnailUrl, string mediumUrl, string highUrl) = await _imgBBHelper.Upload(stream, imageCreateDto.Image.FileName, imageCreateDto.Image.Length, imageCreateDto.Image.ContentType);

            // Try to get Width and Height
            (int height, int width, string orientation) = ImageSizeHelper.GetImageSize(imageCreateDto.Image);

            // Set image Model properties
            imageModel.Thumbnail = thumbnailUrl;
            imageModel.Medium = mediumUrl;
            imageModel.Hight = highUrl;
            imageModel.Size = new Size
            {
                Height = height,
                Width = width
            };
            imageModel.Orientation = orientation;
            imageModel.CreatedBy = userIdFromAccessToken;

            await _imageService.Create(imageModel);

            var imageReadDto = _mapper.Map<ImageReadDto>(imageModel);
            return Created("", new ResponseDto(201));
        }

        /// <summary>
        /// Update image tags
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialImageUpdate(string id, JsonPatchDocument<ImageUpdateDto> patchDocument)
        {
            var imageModelFromService = await _imageService.GetById(id);
            if (imageModelFromService == null)
            {
                return NotFound(new ResponseDto(404, "User not found"));
            }

            var imageToPatch = _mapper.Map<ImageUpdateDto>(imageModelFromService);
            patchDocument.ApplyTo(imageToPatch, ModelState);

            if (!TryValidateModel(imageToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(imageToPatch, imageModelFromService);

            await _imageService.Update(id, imageModelFromService);

            return Ok(new ResponseDto(200));
        }

        /// <summary>
        /// Delete an image in DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteImage(string id)
        {
            var rs = await _imageService.Delete(id);
            if (rs == false)
            {
                return NotFound(new ResponseDto(404));
            }

            return Ok(new ResponseDto(200));
        }
    }
}