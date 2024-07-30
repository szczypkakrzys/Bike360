using Bike360.Domain.Common;

namespace Bike360.Application.Services;

public class PaymentService
{
    public static CustomersActivitiesRelations RelationAfterPayment(
        CustomersActivitiesRelations relation,
        double paymentAmount)
    {
        var (entireCostLeft, advancedPaymentLeft) = CalculatePayment(
                                                paymentAmount,
                                                relation.EntireCostLeftToPay,
                                                relation.AdvancedPaymentLeftToPay);

        relation.EntireCostLeftToPay = entireCostLeft;
        relation.AdvancedPaymentLeftToPay = advancedPaymentLeft;

        if (relation.AdvancedPaymentDate == null && relation.AdvancedPaymentLeftToPay == 0)
            relation.AdvancedPaymentDate = DateOnly.FromDateTime(DateTime.UtcNow);
        if (relation.EntireAmountPaymentDate == null && relation.EntireCostLeftToPay == 0)
            relation.EntireAmountPaymentDate = DateOnly.FromDateTime(DateTime.UtcNow);

        return relation;
    }

    private static (double entireCostLeft, double advancedPaymentLeft) CalculatePayment(
        double paymentAmount,
        double entireCostLeft,
        double advancedPaymentLeft)
    {
        if (paymentAmount == entireCostLeft)
        {
            entireCostLeft = 0;
            advancedPaymentLeft = 0;
        }
        else
        {
            entireCostLeft -= paymentAmount;

            if (advancedPaymentLeft > 0)
            {
                if (paymentAmount < advancedPaymentLeft)
                    advancedPaymentLeft -= paymentAmount;
                else
                    advancedPaymentLeft = 0;
            }
        }

        return (entireCostLeft, advancedPaymentLeft);
    }
}
