using School.API.Core.Entities;

namespace School.API.Core.Interfaces
{
    public interface IPayment
    {
        List<Payments> list();

        List<Payments> listById(int id);
        bool create(Payments payments);
    }
}
