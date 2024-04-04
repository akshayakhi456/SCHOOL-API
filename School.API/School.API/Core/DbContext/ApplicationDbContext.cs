using School.API.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace School.API.Core.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public  virtual  DbSet<Students> Students { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }
        public virtual DbSet<Enquiry> Enquiry { get; set; }
        public virtual DbSet<Expenses> Expenses { get; set; }
        public virtual DbSet<Guardian> Guardians { get; set; }
        public virtual  DbSet<StudentAddress> StudentAddresses { get; set; }

        public virtual DbSet<EnquiryEntranceExam> EnquiryEntranceExams { get; set; }
        public virtual DbSet<PaymentsEnquiry> paymentsEnquiry { get; set; }

        public virtual DbSet<Classes> classes { get; set; }
        public virtual DbSet<Section> section { get; set; }
        public virtual DbSet<EnquiryQuestions> enquiryQuestions { get; set; }

    }
}
