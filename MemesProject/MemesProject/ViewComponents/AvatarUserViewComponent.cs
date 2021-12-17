﻿using MemesProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MemesProject.ViewComponents
{
    public class AvatarUserViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public AvatarUserViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdendity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdendity.FindFirst(ClaimTypes.NameIdentifier);

            if(claim != null)
            {
                byte[] ImageByte = await _db.Users.Where(u => u.Id == claim.Value).Select(selector: u => u.AvatarImage).FirstOrDefaultAsync();
                return View(ImageByte);
            }

            return View(null);

        
        }
    }
}
