using CleanArchitecture.Entities.Bank;
using CleanArchitecture.Entities.Clients;
using CleanArchitecture.Entities.Invoices;
using CleanArchitecture.Entities.Leaves;
using CleanArchitecture.Entities.Orders;
using CleanArchitecture.Entities.Orders.DeliveryNotes;
using CleanArchitecture.Entities.Produit;
using CleanArchitecture.Entities.Projects;
using CleanArchitecture.Entities.Purchases;
using CleanArchitecture.Entities.Sales;
using CleanArchitecture.Entities.Suppliers;
using CleanArchitecture.Entities.Transaction;
using CleanArchitecture.Entities.Users;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.FrameworksAndDrivers
{
    public class AppDbContext : IdentityDbContext<Employee>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<OrderClient> OrderClients { get; set; }
        public DbSet<OrderSupplier> OrderSuppliers { get; set; }
        public DbSet<InvoiceClient> InvoiceClients { get; set; }
        public DbSet<InvoiceSupplier> InvoiceSuppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<InvoiceLineItem> InvoiceLineItems { get; set; }
        public DbSet<DeliveryItem> DeliveryItems { get; set; }
        public DbSet<BonDeReception> BonDeReceptions { get; set; }
        public DbSet<BonDeReceptionItem> BonDeReceptionItems { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<HR> RHs { get; set; }
        public DbSet<Commerciale> Commerciales { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<TaskProject> TaskProjects { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<PasswordHistory> PasswordHistories { get; set; }
        public DbSet<Devis> Devises { get; set; }
        public DbSet<DeliveryNote> DeliveryNotes { get; set; }
        public DbSet<Stock> Stocks { get; set; }
    
        public DbSet<TransactionAccount> TransactionAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relations Client
            modelBuilder.Entity<Client>()
                .HasMany(c => c.Sales)
                .WithOne(s => s.Client)
                .HasForeignKey(s => s.ClientId);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.OrderClients)
                .WithOne(oc => oc.Client)
                .HasForeignKey(oc => oc.ClientID);

            modelBuilder.Entity<Devis>()
                .HasOne(d => d.Client)
                .WithMany(c => c.Devises)
                .HasForeignKey(d => d.ClientId);

            modelBuilder.Entity<Devis>()
                .HasMany(d => d.Products)
                .WithMany(p => p.Devises);

            // Relations DeliveryNote
            modelBuilder.Entity<DeliveryNote>()
                .HasOne(dn => dn.OrderClient)
                .WithMany(oc => oc.DeliveryNotes)
                .HasForeignKey(dn => dn.OrderClientId);

            modelBuilder.Entity<DeliveryNote>()
                .HasMany(dn => dn.DeliveryNoteItems)
                .WithOne(dni => dni.DeliveryNote)
                .HasForeignKey(dni => dni.DeliveryNoteId);

            // Relations Supplier
            modelBuilder.Entity<Supplier>()
                .HasMany(s => s.Purchases)
                .WithOne(p => p.Supplier)
                .HasForeignKey(p => p.SupplierId);

            modelBuilder.Entity<Supplier>()
                .HasMany(s => s.OrderSuppliers)
                .WithOne(os => os.Supplier)
                .HasForeignKey(os => os.SupplierID);

            modelBuilder.Entity<OrderSupplier>()
                .HasMany(os => os.BonDeReceptions)
                .WithOne(br => br.OrderSupplier)
                .HasForeignKey(br => br.OrderSupplierId);

            modelBuilder.Entity<BonDeReceptionItem>()
                .HasOne(bri => bri.BonDeReception)
                .WithMany(br => br.Items)
                .HasForeignKey(bri => bri.BonDeReceptionId);

            modelBuilder.Entity<BonDeReceptionItem>()
                .HasOne(bri => bri.Item)
                .WithMany(p => p.BonDeReceptionItems)
                .HasForeignKey(bri => bri.ProductId);

            // Relations Product
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Sales)
                .WithOne(s => s.Product)
                .HasForeignKey(s => s.ProductId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Purchases)
                .WithOne(pu => pu.Product)
                .HasForeignKey(pu => pu.ProductId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderItems)
                .WithOne(oi => oi.Product)
                .HasForeignKey(oi => oi.ProductId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.InvoiceLineItems)
                .WithOne(il => il.Product)
                .HasForeignKey(il => il.ProductId);

            // Définir la relation entre OrderClient et Client
            modelBuilder.Entity<OrderClient>()
                .HasOne(o => o.Client) // OrderClient a un Client
                .WithMany(c => c.OrderClients) // Client a plusieurs OrderClients
                .HasForeignKey(o => o.ClientID); // Clé étrangère explicitement définie

            modelBuilder.Entity<OrderClient>()
              .HasOne(oc => oc.Devis)     // Un OrderClient a un seul Devis
              .WithOne(d => d.OrderClient) // Un Devis a un seul OrderClient
              .HasForeignKey<OrderClient>(oc => oc.DevisId) // La clé étrangère est dans OrderClient
              .OnDelete(DeleteBehavior.Restrict);  // Utiliser Restrict pour éviter les chemins de suppression en cascade



            // Définir la relation entre OrderSupplier et Supplier
            modelBuilder.Entity<OrderSupplier>()
                .HasOne(o => o.Supplier) // OrderSupplier a un Supplier
                .WithMany(s => s.OrderSuppliers) // Supplier a plusieurs OrderSuppliers
                .HasForeignKey(o => o.SupplierID); // Clé étrangère explicitement définie

            modelBuilder.Entity<InvoiceClient>()
                .HasOne(ic => ic.OrderClient)
                .WithMany()
                .HasForeignKey(ic => ic.OrderClientId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation Admin et Employee
            modelBuilder.Entity<Admin>()
                .HasMany(a => a.ManagedEmployees)
                .WithOne(e => e.Admin)
                .HasForeignKey(e => e.AdminId);

            // Configuration de l'héritage pour Employee
            modelBuilder.Entity<Employee>()
                .HasDiscriminator<string>("EmployeeType")
                .HasValue<Employee>("Employee")
                .HasValue<HR>("RH")
                .HasValue<Manager>("Manager")
                .HasValue<Commerciale>("Commerciale");

            // Relations Project, TaskProject et Employee
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Manager)
                .WithMany(m => m.ManagedProjects)
                .HasForeignKey(p => p.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.TeamMembers)
                .WithMany(e => e.Projects);

            modelBuilder.Entity<TaskProject>()
                .HasOne(tp => tp.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(tp => tp.ProjectId);

            modelBuilder.Entity<TaskProject>()
               .HasOne(tp => tp.Employee)
               .WithMany(e => e.AssignedTasks)
               .HasForeignKey(tp => tp.EmployeeId)
               .OnDelete(DeleteBehavior.Restrict); // Utiliser Restrict ou NoAction pour éviter la cascade

            modelBuilder.Entity<TaskProject>()
                .HasOne(tp => tp.AssignedTo)
                .WithMany() // Assume that Employee does not have a direct collection of tasks
                .HasForeignKey(tp => tp.AssignedToId);

            modelBuilder.Entity<Manager>()
                .HasMany(m => m.DepartmentTasks)
                .WithOne() // Si TaskProject n'a pas de propriété inverse vers Manager
                .HasForeignKey(tp => tp.AssignedToId); // À adapter selon votre logique

            // Configuration des clés primaires pour les entités
            modelBuilder.Entity<BonDeReceptionItem>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Stock>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<TransactionAccount>()
                .HasKey(t => t.TransactionId);

            // Relation entre Employee et LeaveRequest
            modelBuilder.Entity<LeaveRequest>()
                .HasOne(l => l.Employee)
                .WithMany(e => e.LeaveRequests)
                .HasForeignKey(l => l.EmployeeId);


            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId }); // Clé composite UserId et RoleId

            // Configuration de la relation avec Employee
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)  // Un UserRole a un Employee
                .WithMany(e => e.Roles)  // Un Employee a plusieurs UserRoles
                .HasForeignKey(ur => ur.UserId)  // Clé étrangère UserId
                .OnDelete(DeleteBehavior.Cascade);  // Cascade Delete

            // Configuration de la relation avec Role
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)  // Un UserRole a un Role
                .WithMany(r => r.UserRoles)  // Un Role a plusieurs UserRoles
                .HasForeignKey(ur => ur.RoleId)  // Clé étrangère RoleId
                .OnDelete(DeleteBehavior.Restrict);






            // Relation between Invoice and InvoiceLineItem
            modelBuilder.Entity<Invoice>()
                .HasMany(i => i.Items)
                .WithOne(li => li.Invoice)
                .HasForeignKey(li => li.InvoiceId)
                 .OnDelete(DeleteBehavior.Restrict);

            // Relation between InvoiceClient and OrderClient
            modelBuilder.Entity<InvoiceClient>()
                .HasOne(ic => ic.OrderClient)
                .WithMany() // Assuming OrderClient does not hold a collection of InvoiceClient
                .HasForeignKey(ic => ic.OrderClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation between InvoiceClient and DeliveryNote
            modelBuilder.Entity<InvoiceClient>()
                .HasOne(ic => ic.DeliveryNote)
                .WithMany() // Assuming DeliveryNote does not hold a collection of InvoiceClient
                .HasForeignKey(ic => ic.DeliveryNoteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation between InvoiceClient and Sale
            modelBuilder.Entity<InvoiceClient>()
                .HasMany(ic => ic.Sales)
                .WithOne(s => s.Invoice)
                .HasForeignKey(s => s.InvoiceClientInvoiceId)
               .OnDelete(DeleteBehavior.Restrict);

            // Relation between InvoiceSupplier and OrderSupplier
            modelBuilder.Entity<InvoiceSupplier>()
                .HasOne(s => s.OrderSupplier)
                .WithMany() // Assuming OrderSupplier does not hold a collection of InvoiceSupplier
                .HasForeignKey(s => s.OrderSupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation between InvoiceSupplier and BonDeReception
            modelBuilder.Entity<InvoiceSupplier>()
                .HasOne(s => s.BonDeReception)
                .WithMany() // Assuming BonDeReception does not hold a collection of InvoiceSupplier
                .HasForeignKey(s => s.BonDeReceptionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation between InvoiceLineItem and Product
            modelBuilder.Entity<InvoiceLineItem>()
                .HasOne(li => li.Product)
                .WithMany(p => p.InvoiceLineItems)  // Vérifie que cette collection est définie dans Product
                .HasForeignKey(li => li.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Client)
                .WithMany(c => c.Sales)  // Assure-toi que cette collection est bien définie dans Client
                .HasForeignKey(s => s.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Product)
                .WithMany(p => p.Sales)  // Assure-toi que cette collection est bien définie dans Product ou Item
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


            // Configuration de Role
            modelBuilder.Entity<Role>()
                .HasKey(r => r.RoleId); // Définir la clé primaire pour Role

            // Relation 1:n entre Role et UserRole (un rôle peut être attribué à plusieurs utilisateurs)
            modelBuilder.Entity<Role>()
                .HasMany(r => r.UserRoles) // Un rôle peut avoir plusieurs associations UserRole
                .WithOne(ur => ur.Role) // Chaque UserRole est associé à un rôle
                .HasForeignKey(ur => ur.RoleId); // Clé étrangère dans UserRole pointant vers Role

            // Relation n:n entre Employee et Role via UserRole
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId }); // Clé composite dans UserRole

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User) // Un UserRole est lié à un Employee
                .WithMany(e => e.Roles) // Un Employee peut avoir plusieurs UserRoles
                .HasForeignKey(ur => ur.UserId); // Clé étrangère dans UserRole pointant vers Employee

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role) // Un UserRole est lié à un Role
                .WithMany(r => r.UserRoles) // Un Role peut être associé à plusieurs UserRoles
                .HasForeignKey(ur => ur.RoleId); // Clé étrangère dans UserRole pointant vers Role

            modelBuilder.Entity<Employee>()
                .HasOne(u => u.UserProfile) // Each Employee has one UserProfile
                .WithOne(p => p.User)       // Each UserProfile is associated with one Employee
                .HasForeignKey<UserProfile>(p => p.UserId); // UserId in UserProfile is the foreign key




        


            // Exemple pour Employee-Project (relation plusieurs-à-plusieurs)
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Projects)
                .WithMany(p => p.TeamMembers)
                .UsingEntity<Dictionary<string, object>>(
                    "EmployeeProject",
                    j => j.HasOne<Project>().WithMany().HasForeignKey("ProjectId").OnDelete(DeleteBehavior.Restrict),
                    j => j.HasOne<Employee>().WithMany().HasForeignKey("EmployeeId").OnDelete(DeleteBehavior.Restrict)
                );

        }
    }
}
