namespace Atlas_TestTask.Models
{
    public class PaymentsViewModel
    {
        public List<Payment> Payments { get; private set; }
        public double Overpayments { get; private set; }

        public PaymentsViewModel(List<Payment> payments)
        {
            Payments = payments;
            Overpayments = payments.Sum(p => p.PaymentByPercents);
        }
    }
}
