using Microsoft.EntityFrameworkCore;

namespace Bulky.Web.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
    }
}