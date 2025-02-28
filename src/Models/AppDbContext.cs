using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> users { get; set; }
    public DbSet<Event> events { get; set; }
    public DbSet<EventParticipant> event_participants { get; set; }
    public DbSet<Category> categories { get; set; }
    public DbSet<EventImage> event_images { get; set; }
    public DbSet<Comment> comments { get; set; }
    public DbSet<Like> likes { get; set; }
    public DbSet<Notification> notifications { get; set; }

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