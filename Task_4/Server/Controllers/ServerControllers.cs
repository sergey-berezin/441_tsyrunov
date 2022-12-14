using System.Collections.ObjectModel;
using System.Net;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Database;

namespace Server.Controllers
{
    [ApiController]
    [Route("images")]
    [Produces("application/json")]
    public class ServerControllers : ControllerBase
    {
        private IDatabase db;

        private readonly ILogger<ServerControllers> _logger;

        public ServerControllers(IDatabase db, ILogger<ServerControllers> logger)
        {
            this.db = db;
            _logger = logger;
        }

        /// <summary>
        /// POST image with given image path and byteSource 
        /// </summary>
        /// <param name="data">Input image byte data and string path name</param>
        /// <returns>Newly created instance of the ImageInfo type</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ImageInfo>> PostImage([FromBody]imgPostClass data, CancellationToken ctn)
        {
            string request = "POST /images";
            _logger.LogInformation($"[{DateTime.Now}]  {request}");

            try
            {
                var img = await db.PostImage(data.byteSource, data.path, ctn);
                if (img != null)
                {
                    var id = img.imageId;

                    _logger.LogInformation($"[{DateTime.Now}]  Request {request} has succeeded with status {(int)HttpStatusCode.Created}");
                    return Created(id.ToString(), img);
                }

                _logger.LogWarning($"[{DateTime.Now}]  Request {request} ended with status {(int)HttpStatusCode.OK}: Image already exists in DB");
                return base.StatusCode((int)HttpStatusCode.OK, "Image already exists in DB");
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogWarning($"[{DateTime.Now}]  Request {request} has succeeded with status {(int)HttpStatusCode.NoContent}: Processing image was cancelled");
                return base.NoContent();
            }
            catch (Error err)
            {
                _logger.LogError($"[{DateTime.Now}]  Request {request} failed with status {(int)err.Status}: {err.Message}");
                return StatusCode((int)err.Status, err.Message);
            }
        }

        /// <summary>
        /// GET all images
        /// </summary>
        /// <param></param>
        /// <returns>Collection of ImageInfo type</returns>
        [HttpGet]
        public async Task<ActionResult<ObservableCollection<ImageInfo>>> GetImages()
        {
            string request = "GET /images";
            _logger.LogInformation($"[{DateTime.Now}]  {request}");

            try
            {
                var images = db.GetAllImages();

                _logger.LogInformation($"[{DateTime.Now}]  Request {request} has succeeded with status {(int)HttpStatusCode.OK}");
                return base.Ok(images);
            }
            catch (Error err)
            {
                _logger.LogError($"[{DateTime.Now}]  Request {request} failed with status {(int)err.Status}: {err.Message}");
                return StatusCode((int)err.Status, err.Message);
            }
        }

        /// <summary>
        /// GET image for a given ID 
        /// </summary>
        /// <param name="id">Input image ID</param>
        /// <returns>ImageInfo type object</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ImageInfo>> GetImage(int id)
        {
            string request = $"GET /images/{id}";
            _logger.LogInformation($"[{DateTime.Now}]  {request}");

            try
            {
                var image = db.GetImageById(id);

                _logger.LogInformation($"[{DateTime.Now}]  Request {request} has succeeded with status {(int)HttpStatusCode.OK}");
                return Ok(image);
            }
            catch (Error err)
            {
                _logger.LogError($"[{DateTime.Now}]  Request {request} failed with status {(int)err.Status}: {err.Message}");
                return StatusCode((int)err.Status, err.Message);
            }
        }

        /// <summary>
        /// GET images, where given emotion is most likely
        /// </summary>
        /// <param name="emotion">Input emotion string</param>
        /// <returns>Collection of ImageInfo type</returns>
        [HttpGet("emotion/{emotion}")]
        public async Task<ActionResult<ObservableCollection<ImageInfo>>> GetImagesForEmotion(string emotion)
        {
            string request = $"GET /images/emotion/{emotion}";
            _logger.LogInformation($"[{DateTime.Now}]  {request}");

            try
            {
                var images = db.GetImagesByEmotion(emotion);

                _logger.LogInformation($"[{DateTime.Now}]  Request {request} has succeeded with status {(int)HttpStatusCode.OK}");
                return base.Ok(images);
            }
            catch (Error err)
            {
                _logger.LogError($"[{DateTime.Now}]  Request {request} failed with status {(int)err.Status}: {err.Message}");
                return StatusCode((int)err.Status, err.Message);
            }
        }

        /// <summary>
        /// DELETE all images from DB
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteImages()
        {
            string request = "DELETE /images";
            _logger.LogInformation($"[{DateTime.Now}]  {request}");

            try
            {
                db.DeleteAllImages();

                _logger.LogInformation($"[{DateTime.Now}]  Request {request} has succeeded with status {(int)HttpStatusCode.NoContent}");
                return base.NoContent();
            }
            catch (Error err)
            {
                _logger.LogError($"[{DateTime.Now}]  Request {request} failed with status {(int)err.Status}: {err.Message}");
                return StatusCode((int)err.Status, err.Message);
            }
        }

        /// <summary>
        /// DELETE image for a given ID from DB
        /// </summary>
        /// <param name="id">Input image ID</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteImage(int id)
        {
            string request = $"DELETE /images/{id}";
            _logger.LogInformation($"[{DateTime.Now}]  {request}");

            try
            {
                db.DeleteImageById(id);

                _logger.LogInformation($"[{DateTime.Now}]  Request {request} has succeeded with status {(int)HttpStatusCode.NoContent}");
                return base.NoContent();
            }
            catch (Error err)
            {
                _logger.LogError($"[{DateTime.Now}]  Request {request} failed with status {(int)err.Status}: {err.Message}");
                return StatusCode((int)err.Status, err.Message);
            }
        }
    }
}