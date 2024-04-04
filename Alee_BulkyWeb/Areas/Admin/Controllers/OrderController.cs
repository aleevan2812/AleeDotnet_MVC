using AleeBook.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace AleeBookWeb.Areas.Admin.Controllers;

public class OrderController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        return View();
    }

    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        var objOrderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();
        return Json(new { data = objOrderHeaders });
    }

    #endregion
}