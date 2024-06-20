using DTOs.DTOs.Assets;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Services.Services.Interface;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private IAssetServices assetServices;

        public AssetsController(IAssetServices assetServices)
        {
            this.assetServices = assetServices;
        }

        // ___________________________ 1- Create ___________________________
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(AddOrUpdateAssetDTO assetDTO)
        {
            var createAsset = await assetServices.Create(assetDTO);
            if (createAsset != null)
            {
                return Ok(createAsset);
            }
            else
            {
                return BadRequest("This Asset already exist !...");
            }
        }


        // ___________________________ 2- Read to Get one Asset ___________________________
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetOne(int ID)
        {
            var assets = await assetServices.GetOneByID(ID);
            if (assets == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(assets);
            }
        }

        // ___________________________ 3- to Search by Name And Category for Assets ___________________________

        public async Task<List<Asset>> SearchByName(string name)
        {
            var assetsList = await assetServices.SearchByName(name);
            return assetsList;
        }

        public async Task<List<Asset>>SearchByCategory(Category category)
        {
            var assetList = await assetServices.SearchByCategory(category);
            return assetList;
        }

        // ___________________________ 4- Update ___________________________
        [HttpPut("{name:string}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(AddOrUpdateAssetDTO assetDTO, int ID)
        {
            var UpdateAsset = await assetServices.GetOneByID(ID);
            if (UpdateAsset == null)
            {
                return BadRequest("There is no asset with this name !....");
            }
            else
            {
                await assetServices.Update(assetDTO, ID);
                return Ok($"The name of category {ID} was updated from '{UpdateAsset.AssetID}' to be '{assetDTO.AssetName}' ....");
            }
        }

        // ___________________________ 4- Delete ___________________________

        [HttpDelete("{ID:int}")]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int ID)
        {
            var deleteServices = await assetServices.Delete(ID);
            if (deleteServices)
            {
                return Ok($"The asset '{ID}' was deleted successfully !....");
            }
            else
            {
                return BadRequest("There is no asset with this ID !....");
            }
        }

    }
}
