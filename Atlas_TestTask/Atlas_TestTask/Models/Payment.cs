namespace Atlas_TestTask.Models
{
    public class Payment
    {
        public int Number { get; private set; }
        public DateTime Date { get; private set; }
        
        public double FullPayment { get; private set; }
        public double PaymentByPercents { get; private set; }
        public double PaymentByBody { get; private set; }
        public double Rest { get; private set; }

        public Payment(int number, DateTime date, double fullPayment, double paymentByPercents, double paymentByBody, double rest)
        {
            Number = number;
            Date = date;
            PaymentByPercents = paymentByPercents;
            PaymentByBody = paymentByBody;
            Rest = rest;
            FullPayment = fullPayment;
        }

        public static List<Payment> ComputePaymentsByCredit(double loanSum, double loanRate, int loanTerm)
        {
            List<Payment> payments = new List<Payment>();
            double i = (loanRate / 100) / 12;
            double annuitRatio = (i * Math.Pow(1 + i, (double)loanTerm)) / (Math.Pow(1 + i, (double)loanTerm) - 1);
            double monthlyPayment = annuitRatio * loanSum;
            for (int j = 1; j <= loanTerm; j++)
            {
                double paymentByPercents = loanSum * i;
                double paymentByBody = monthlyPayment - paymentByPercents;
                loanSum -= paymentByBody;
                payments.Add(new Payment(j, DateTime.Now.AddMonths(j), monthlyPayment, paymentByPercents, paymentByBody, loanSum));
            }
            return payments;
        }

        public static List<Payment> ComputePaymentsByCreditForDailyRate(double loanSum, double loanRate, int loanTerm, int paymentStep)
        {
            List<Payment> payments = new List<Payment>();
            double i = (loanRate / 100) * paymentStep;
            double periods = (double)loanTerm / (double)paymentStep;
            double annuitRatio = (i * Math.Pow(1 + i, periods)) / (Math.Pow(1 + i, periods) - 1);
            double periodPayment = annuitRatio * loanSum;
            for (int j = 1; j <= periods; j++)
            {
                double paymentByPercents = loanSum * i;
                double paymentByBody = periodPayment - paymentByPercents;
                loanSum -= paymentByBody;
                payments.Add(new Payment(j, DateTime.Now.AddDays(j * paymentStep), periodPayment, paymentByPercents, paymentByBody, loanSum));
            }
            return payments;
        }
    }
}
