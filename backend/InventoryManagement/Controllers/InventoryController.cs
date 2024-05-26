using InventoryManagement.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Route("api/Inventory")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class InventoryController : ControllerBase
    {
        private readonly InvDbContext _context;

        public InventoryController(InvDbContext context)
        {
            _context = context;
        }

        #region Purchase API

        [HttpGet("GetAllPurchases")]
        public ApiResponse GetAllPurchases()
        {
            ApiResponse _res = new ApiResponse();
            try
            {
                var all = (from purchase in _context.Purchases
                           join product in _context.Products on purchase.productId equals product.productId
                           select new
                           {
                               invoiceAmount = purchase.invoiceAmount,
                               invoiceNo = purchase.invoiceNo,
                               productId = purchase.productId,
                               purchaseDate = purchase.purchaseDate,
                               purchaseId = purchase.purchaseId,
                               quantity = purchase.quantity,
                               supplierName = purchase.supplierName,
                               productName = product.productName,
                           } ).OrderByDescending(m => m.productId).ToList();
                _res.Result = true;
                _res.Data = all;
                return _res;
            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.Message;
                return _res;
            }
        }

        [HttpPost("CreateNewPurchase")]
        public ApiResponse CreateNewPurchase([FromBody] Purchase obj)
        {
            ApiResponse _res = new ApiResponse();

            try
            {
                var purchase = _context.Purchases.SingleOrDefault(m => m.invoiceNo.ToLower() == obj.invoiceNo.ToLower());
                if (purchase == null)
                {
                    _context.Purchases.Add(obj);
                    _context.SaveChanges();

                    var stockProduct = _context.Stocks.SingleOrDefault(m => m.productId == obj.productId);
                    if(stockProduct == null)
                    {
                        Stock _stock = new Stock()
                        {
                            createdDate = DateTime.Now,
                            lastModifiedDate = DateTime.Now,
                            productId = obj.productId,
                            quantity = obj.quantity
                        };
                        _context.Stocks.Add(_stock);
                        _context.SaveChanges();
                    }
                    else
                    {
                        stockProduct.quantity += obj.quantity;
                        stockProduct.lastModifiedDate = DateTime.Now;
                        _context.SaveChanges();
                    }

                    _res.Result = true;
                    _res.Message = "Purchase created successfully";
                    return _res;
                }
                else
                {
                    _res.Result = false;
                    _res.Message = "Failed to create a purchase, invoiceNo already exists";
                    return _res;
                }
            }catch(Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.Message;
                return _res;
            }
        }

        #endregion

        #region Sale API

        [HttpGet("GetAllSales")]
        public ApiResponse GetAllSales()
        {
            ApiResponse _res = new ApiResponse();
            try
            {
                var all = (from sale in _context.Sales
                           join product in _context.Products on sale.productId equals product.productId
                           select new
                           {
                               saleId = sale.saleId,
                               phoneNumber = sale.phoneNumber,
                               invoiceNo = sale.invoiceNo,
                               productId = sale.productId,
                               saleDate = sale.saleDate,
                               customerName = sale.customerName,
                               quantity = sale.quantity,
                               totalAmount = sale.totalAmount,
                               productName = product.productName
                           }).OrderByDescending(m => m.saleId).ToList();
                _res.Result = true;
                _res.Data = all;
                return _res;
            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.Message;
                return _res;
            }
        }

        [HttpPost("CreateNewSale")]
        public ApiResponse CreateNewSale([FromBody] Sale obj)
        {
            ApiResponse _res = new ApiResponse();

            try
            {
                var sale = _context.Sales.SingleOrDefault(m => m.invoiceNo.ToLower() == obj.invoiceNo.ToLower());
                var stockProduct = _context.Stocks.SingleOrDefault(m => m.productId == obj.productId);
                if (sale == null && stockProduct != null)
                {
                    _context.Sales.Add(obj);

                    stockProduct.quantity = stockProduct.quantity - obj.quantity;
                    stockProduct.lastModifiedDate = DateTime.Now;
                    _context.SaveChanges();


                    _res.Result = true;
                    _res.Message = "Sale created successfully";
                    return _res;
                }
                else
                {
                    _res.Result = false;
                    _res.Message = "Failed to create a sale, invoiceNo already exists";
                    return _res;
                }
            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.Message;
                return _res;
            }
        }

        #endregion


        #region Product API

        [HttpGet("GetAllProduct")]
        public ApiResponse GetAllProducts()
        {
            ApiResponse _res = new ApiResponse();
            try
            {
                var all = _context.Products.ToList();
                _res.Result = true;
                _res.Data = all;
                return _res;
            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.Message;
                return _res;
            }
        }

        [HttpPost("CreateNewProduct")]
        public ApiResponse CreateNewProduct([FromBody] Product obj)
        {
            ApiResponse _res = new ApiResponse();

            try
            {
                var product = _context.Products.SingleOrDefault(m => m.productName.ToLower() == obj.productName.ToLower());

                if (product == null)
                {
                    _context.Products.Add(obj);
                    _context.SaveChanges();

                    _res.Result = true;
                    _res.Message = "Product created successfully";
                    return _res;
                }
                else
                {
                    _res.Result = false;
                    _res.Message = "Failed to create a product, product already exists";
                    return _res;
                }
            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.Message;
                return _res;
            }
        }

        #endregion

        #region Stock API

        [HttpGet("GetAllStock")]
        public ApiResponse GetAllStock()
        {
            ApiResponse _res = new ApiResponse();
            try
            {
                var all = (from stock in _context.Stocks
                           join product in _context.Products on stock.productId equals product.productId
                           select new
                           {
                               stockId = stock.stockId,
                               productId = stock.productId,
                               quantity = stock.quantity,
                               createdDate = stock.createdDate,
                               lastModifiedDate = stock.lastModifiedDate,
                               productName = product.productName
                           }).OrderByDescending(m => m.stockId).ToList();
                _res.Result = true;
                _res.Data = all;
                return _res;
            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.Message;
                return _res;
            }
        }

        [HttpGet("CheckStockByProductId")]
        public ApiResponse CheckStockByProductId(int productId)
        {
            ApiResponse _res = new ApiResponse();
            try
            {
                var stock = _context.Stocks.SingleOrDefault(m => m.productId == productId);
                if(stock != null)
                {
                    if (stock.quantity > 0)
                    {
                        _res.Result = true;
                        _res.Data = stock;
                        _res.Message = "Stock available";
                    }
                    else
                    {
                        _res.Result = false;
                        _res.Message = "Stock not available";
                    }
                }
                else
                {
                    _res.Result = false;
                    _res.Message = "Stock not available";
                }

                return _res;
            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.Message;
                return _res;
            }
        }

        #endregion
    }
}
