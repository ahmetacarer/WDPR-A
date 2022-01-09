using Microsoft.AspNetCore.Identity;

namespace WDPR_A.ViewModels;

public class RoleSystem
{
    private readonly WDPRContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public RoleSystem(WDPRContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task SeedRoles()
    {
        await CreateRole("Client");
        await CreateRole("Guardian");
        await CreateRole("Orthopedagogue");
        await CreateRole("Moderator");
    }

    public async Task CreateRole(string roleName)
    {
        if (!(await _roleManager.RoleExistsAsync(roleName)))
        {
            await _roleManager.CreateAsync(new IdentityRole(roleName));
            _context.SaveChanges();
        }
    }

    public async Task DeleteRole(string roleName)
    {
        if (!(await _roleManager.RoleExistsAsync(roleName)))
        {
            await _roleManager.DeleteAsync(new IdentityRole(roleName));
            _context.SaveChanges();
        }
    }

    public async Task AddUserToRole(IdentityUser user, string roleName)
    {
        var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
        if (!usersInRole.Any(u => u == user))
        {
            await _userManager.AddToRoleAsync(user, roleName);
            await _context.SaveChangesAsync();
        }

    }

    public async Task RemoveRoleFromUser(IdentityUser user, string roleName)
    {
        var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
        if (!usersInRole.Any(u => u == user))
        {
            await _userManager.RemoveFromRoleAsync(user, roleName);
            await _context.SaveChangesAsync();
        }
    }
}