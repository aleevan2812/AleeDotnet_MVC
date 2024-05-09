using AleeBook.DataAccess.Data;
using AleeBook.DataAccess.Repository.IRepository;
using AleeBook.Models;
using AleeBook.Models.ViewModels;
using AleeBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AleeBookWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class UserController : Controller
{
    // private readonly ApplicationDbContext _db;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<IdentityUser> _userManager;

    public UserController(ApplicationDbContext db, UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork,
        RoleManager<IdentityRole> roleManager)
    {
        // _db = db;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult RoleManagement(string userId)
    {
        // var RoleId = _db.UserRoles.FirstOrDefault(u => u.UserId == userId).RoleId;

        var RoleVM = new RoleManagementVM
        {
            ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId, "Company"),
            RoleList = _roleManager.Roles.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Name
            }),
            CompanyList = _unitOfWork.Company.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            })
        };

        RoleVM.ApplicationUser.Role = _userManager.GetRolesAsync(_unitOfWork.ApplicationUser.Get(u => u.Id == userId))
            .GetAwaiter().GetResult().FirstOrDefault();

        return View(RoleVM);
    }

    [HttpPost]
    public IActionResult RoleManagement(RoleManagementVM roleManagementVM)
    {
        // var RoleId = _db.UserRoles.FirstOrDefault(u => u.UserId == roleManagementVM.ApplicationUser.Id).RoleId;
        // string oldRole = _db.Roles.FirstOrDefault(u => u.Id == RoleId).Name;

        var oldRole = _userManager
            .GetRolesAsync(_unitOfWork.ApplicationUser.Get(u => u.Id == roleManagementVM.ApplicationUser.Id))
            .GetAwaiter().GetResult().FirstOrDefault();

        var applicationUser =
            _unitOfWork.ApplicationUser.Get(u => u.Id == roleManagementVM.ApplicationUser.Id);

        if (!(roleManagementVM.ApplicationUser.Role == oldRole))
        {
            // role is need to update

            // ApplicationUser applicationUser = _db.ApplicationUsers.FirstOrDefault(u => u.Id == roleManagementVM.ApplicationUser.Id);
            if (roleManagementVM.ApplicationUser.Role == SD.Role_Company)
                applicationUser.CompanyId = roleManagementVM.ApplicationUser.CompanyId;
            if (oldRole == SD.Role_Company)
                applicationUser.CompanyId = null;

            // _db.SaveChanges();
            _unitOfWork.ApplicationUser.Update(applicationUser);
            _unitOfWork.Save();
            _userManager.RemoveFromRoleAsync(applicationUser, oldRole).GetAwaiter()
                .GetResult();
            _userManager.AddToRoleAsync(applicationUser, roleManagementVM.ApplicationUser.Role).GetAwaiter()
                .GetResult();
        }
        else
        {
            if (oldRole == SD.Role_Company && applicationUser.CompanyId != roleManagementVM.ApplicationUser.CompanyId)
            {
                applicationUser.CompanyId = roleManagementVM.ApplicationUser.CompanyId;
                _unitOfWork.ApplicationUser.Update(applicationUser);
                _unitOfWork.Save();
            }
        }

        return RedirectToAction("Index");
    }

    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        // var objUserList = _db.ApplicationUsers.Include(u => u.Company).ToList();
        var objUserList = _unitOfWork.ApplicationUser.GetAll(includeProperties: "Company").ToList();
        // var userRoles = _db.UserRoles.ToList();
        // var roles = _db.Roles.ToList();

        foreach (var user in objUserList)
        {
            // var roleId = userRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId;
            // user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;
            user.Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();
            if (user.Company == null)
                user.Company = new Company { Name = "" };
        }

        return Json(new { data = objUserList });
    }


    [HttpPost]
    public IActionResult LockUnlock([FromBody] string id)
    {
        // var objFromDb = _db.ApplicationUsers.FirstOrDefault(u => u.Id == id);
        var objFromDb = _unitOfWork.ApplicationUser.Get(u => u.Id == id);
        if (objFromDb == null)
            return Json(new { success = false, message = "Error while Locking/Unlocking" });

        if (objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now)
            //user is currently locked and we need to unlock them
            objFromDb.LockoutEnd = DateTime.Now;
        else
            objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);

        // _db.SaveChanges();
        _unitOfWork.ApplicationUser.Update(objFromDb);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Operation Successful" });
    }

    #endregion
}