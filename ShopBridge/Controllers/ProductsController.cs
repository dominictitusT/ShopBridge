using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopBridge.Model;
using ShopBridge.Model.Products;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {

        private readonly ProductsDbContext _myDbContext;
        private FileInfo filename;
        private IHostingEnvironment _host;
        public ProductsController(ProductsDbContext ProductsDbContext, IHostingEnvironment host)
        {
            _myDbContext = ProductsDbContext;
            _host = host;
        }

        /// <summary>
        /// Getting Data From InventoryDetails Table
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public string GetData()
        {
            if (_myDbContext.InventoryDetails.ToList().Count() > 0)
            {
                return JsonConvert.SerializeObject(_myDbContext.InventoryDetails.ToList());
            }

            return null;
        }
        /// <summary>
        /// Add Data 
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<string> AddProduct(dynamic obj)
        {
            try
            {
                var file = Request.Form.Files.Count!=0 ?Request.Form.Files[0] : null;
                var data = Request.Form["model"];
                InventoryDetails inv = JsonConvert.DeserializeObject<InventoryDetails>(data.ToString());
                _myDbContext.InventoryDetails.Add(inv);
                string rootpath = _host.ContentRootPath;
                if (file != null)
                {
                    filename = new FileInfo(file.FileName);
                    inv.extension = filename.Extension;
                }
                _myDbContext.SaveChanges();
                if (file != null)
                {
                    string filepath = Path.Combine(rootpath, @"wwwroot\images", inv.UnniqId.ToString() + filename.Extension);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return JsonConvert.SerializeObject(_myDbContext.InventoryDetails.ToList());
        }

        /// <summary>
        /// Remove Data  from InventoryDetails Table
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public string RemoveProduct(int invid)
        {
            try
            {
                var inv = _myDbContext.InventoryDetails.Where(s => s.UnniqId == invid).FirstOrDefault();
                if (inv != null)
                {
                    string rootpath = _host.ContentRootPath;
                    string filepath = Path.Combine(rootpath, @"wwwroot\images", inv.UnniqId.ToString() + inv.UnniqId.ToString() + inv.extension);
                    if (System.IO.File.Exists(filepath))
                    {
                        System.IO.File.Delete(filepath);
                    }
                    _myDbContext.InventoryDetails.Remove(inv);
                    _myDbContext.SaveChanges();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return JsonConvert.SerializeObject(_myDbContext.InventoryDetails.ToList());
        }


    }
}