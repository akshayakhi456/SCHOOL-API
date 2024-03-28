using School.API.Core.DbContext;
using School.API.Core.Entities;
using School.API.Core.Interfaces;

namespace School.API.Core.Services
{
    public class PaymentService: IPayment
    {
        private readonly  ApplicationDbContext _applicationDbContext;
        public PaymentService(ApplicationDbContext applicationDbContext) {
            _applicationDbContext = applicationDbContext;
        }

        public bool create(Payments payment)
        {
            _applicationDbContext.Payments.Add(payment);
            _applicationDbContext.SaveChanges();
            return true;
        }

        public List<Payments> list()
        {
            var res = _applicationDbContext.Payments.ToList();
            return res;
        }

        public List<Payments> listById(int id)
        {
            var res = _applicationDbContext.Payments.Where(x=>x.studentId == id).ToList();
            return res;
        }
    }
}
