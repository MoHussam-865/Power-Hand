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
    public class ProductsController(IApiItemsRepo itemsRepo, IPostsRepo postsRepo) : ControllerBase
    {
        private readonly IApiItemsRepo _itemsRepo = itemsRepo;
        private readonly IPostsRepo _postsRepo = postsRepo;



        [HttpPost]
        /*[ProducesResponseType(200, Type = typeof(Message))]
        [ProducesResponseType(500)]*/
        public async Task<ActionResult<Message>> GetProducts([FromBody] Message message)
        {
            // get the last update
            int myLastUpdate = await _itemsRepo.GetLastUpdate();
            int lastUpdate = message.LastUpdate;

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



    }
}
