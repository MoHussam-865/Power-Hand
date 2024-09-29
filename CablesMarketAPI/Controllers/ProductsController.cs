using CablesMarketAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyDatabase.Models;
using MyDatabase.Repository.Items;
using MyDatabase.Repository.Posts;
using System.ComponentModel;

namespace CablesMarketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IApiItemsRepo itemsRepo, IPostsRepo postsRepo, HttpClient httpClient) : ControllerBase
    {
        private readonly IApiItemsRepo _itemsRepo = itemsRepo;
        private readonly IPostsRepo _postsRepo = postsRepo;
        private readonly HttpClient _httpClient = httpClient;
        private readonly string _imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "Product Images");


        [HttpPost("update")]
        public async Task<ActionResult<Message>> GetProducts([FromBody] int lastUpdate)
        {
            // get the last update
            int myLastUpdate = await _itemsRepo.GetLastUpdate();
            //int lastUpdate = message.LastUpdate;

            // get required products
            List<Item> items = (myLastUpdate > lastUpdate)? await _itemsRepo.GetItems(lastUpdate) : [];
 
            // save invoice if exist
            /*if (message.Invoice != null)
            {
                // send the invoice via email
            }*/

            // get the posts
            List<Post> posts = await _postsRepo.GetPosts();

            // add all together  
            Message responce = new(lastUpdate: myLastUpdate, items: items, posts: posts);

            return Ok(responce);
        }



        [HttpGet("download")]
        public async Task<IActionResult> DownloadImage([FromBody] string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
            {
                return BadRequest("No Image Path Provided");
            }
            imagePath = Path.Combine(_imagesPath, imagePath);

            byte[] image;

            if (Uri.IsWellFormedUriString(imagePath, UriKind.Absolute))
            {
                image = await _httpClient.GetByteArrayAsync(imagePath); 

            }
            else
            {
                if (!System.IO.File.Exists(imagePath))
                {
                    return NotFound("Image Not Found");
                }
                image = await System.IO.File.ReadAllBytesAsync(imagePath);
            }

            var fileExtention = Path.GetExtension(imagePath)?.ToLower();
            
            string content;
            switch (fileExtention)
            {
                case ".jpg":
                case ".jpeg":
                    content = "image/jpeg";
                    break;
                case ".png":
                    content = "image/png";
                    break;
                default:
                    content = "application/octet-stream";
                    break;
            }
            return File(image, content);
        }


        [HttpPost("order")]
        public ActionResult ReciveInvoice([FromBody] Invoice invoice)
        {
            if (invoice == null)
            {
                return BadRequest("No invoice Sent");
            }

            // save the invoice and send it as email


            return Ok("Done");
        }


        [HttpGet("hello")]
        public ActionResult Hello()
        {
            return Ok("Hello");
        }

    }
}
