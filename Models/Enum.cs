namespace doan_ttcn.Models
{
    public enum OrderStatus
    {
        Pending=0,
        Confirmed=1,
        Shipping=2,
        Completed=3,
        Cancelled=4
    }
    public enum OrderChannel
    {
        Website=0,
        InStore=1,
        Email=2
    }
}