namespace RMS.Models;

public enum OrderStatus
{
    AwaitingPickup = 1, //The order is ready for pickup by the customer.
    Shipped = 2, //The order has been shipped out for delivery.
    Pending = 3, //The order has been placed but not yet processed.
    Delivered = 4, //The order has been successfully delivered to the customer.
    Cancelled = 5, //The order has been cancelled either by the customer or by the system.
    Returned = 6 //The order has been returned by the customer for some reason.
}