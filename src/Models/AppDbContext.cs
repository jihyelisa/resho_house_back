using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<EventImage> EventImages { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        
    //     modelBuilder.Entity<Like>()
    //         .HasOne(l => l.Event)
    //         .WithMany()
    //         .HasForeignKey(l => l.EventId)
    //         .OnDelete(DeleteBehavior.Cascade);

    //     modelBuilder.Entity<Like>()
    //         .HasOne(l => l.Comment)
    //         .WithMany()
    //         .HasForeignKey(l => l.CommentId)
    //         .OnDelete(DeleteBehavior.Cascade);


        
    }
}