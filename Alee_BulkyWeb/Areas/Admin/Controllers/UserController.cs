﻿using AleeBook.DataAccess.Data;
using AleeBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AleeBookWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class UserController : Controller
{
    private readonly ApplicationDbContext _db;

    public UserController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        return View();
    }

    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        var objUserList = _db.ApplicationUsers.Include(u => u.Company).ToList();
        var userRoles = _db.UserRoles.ToList();
        var roles = _db.Roles.ToList();
        
        foreach (var user in objUserList)
        {
            var roleId = userRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId;
            user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;
            if (user.Company == null)
                user.Company = new() { Name = "" };
        }
        return Json(new { data = objUserList });
    }

    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        return Json(new { success = true, message = "Delete Successful" });
    }

    #endregion API CALLS
}