using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RMS.Models;
using RMS.Repository;

namespace RMS.Services;

public class SalesService : ISalesService
{
    public SalesService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository,
    ICustomerRepository customerRepository, IStaffRepository staffRepository, IStoreRepository storeRepository)
    {
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
        _customerRepository = customerRepository;
        _staffRepository = staffRepository;
        _storeRepository = storeRepository;
    }

    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IStaffRepository _staffRepository;
    private readonly IStoreRepository _storeRepository;

    public IEnumerable<Order> GetOrders()
    {
        return _orderRepository.GetAll()
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .Include(o => o.Staff)
            .Include(o => o.Store);
    }

    public IEnumerable<Order> GetOrdersWhere(Expression<Func<Order, bool>> where)
    {
        return _orderRepository.Get(where)
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .Include(o => o.Staff)
            .Include(o => o.Store);
    }

    public Order GetOrderById(int id)
    {
        if (!_orderRepository.IdExists(id))
            return null;

        return _orderRepository.Get(o => o.OrderId == id)
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .Include(o => o.Staff)
            .Include(o => o.Store).First();
    }

    public bool AddOrder(Order order)
    {
        if (!_orderRepository.Add(order))
            return false;

        return true;
    }

    public bool DeleteOrder(Order order)
    {
        if (!_orderRepository.Delete(order))
            return false;

        return true;
    }

    public bool UpdateOrder(Order order)
    {
        if (!_orderRepository.Update(order))
            return false;

        return true;
    }

    public bool AddOrderItem(OrderItem item)
    {
        if (!_orderItemRepository.Add(item))
            return false;

        return true;
    }

    public bool DeleteOrderItem(OrderItem item)
    {
        if (!_orderItemRepository.Delete(item))
            return false;

        return true;
    }

    public IEnumerable<Customer> GetCustomers()
    {
        return _customerRepository.GetAll()
            .Include(c => c.Orders);
    }

    public IEnumerable<Customer> GetCustomersWhere(Expression<Func<Customer, bool>> where)
    {
        return _customerRepository.Get(where)
            .Include(c => c.Orders);
    }

    public Customer GetCustomerById(int id)
    {
        if (!_customerRepository.IdExists(id))
            return null;

        return _customerRepository.Get(c => c.CustomerId == id)
            .Include(c => c.Orders).First();
    }

    public bool AddCustomer(Customer customer)
    {
        if (!_customerRepository.Add(customer))
            return false;

        return true;
    }

    public bool DeleteCustomer(Customer customer)
    {
        if (!_customerRepository.Delete(customer))
            return false;

        return true;
    }

    public bool UpdateCustomer(Customer customer)
    {
        if (!_customerRepository.Update(customer))
            return false;

        return true;
    }

    public IEnumerable<Staff> GetStaff()
    {
        return _staffRepository.GetAll()
            .Include(s => s.InverseManager)
            .Include(s => s.Manager)
            .Include(s => s.Orders)
            .Include(s => s.Store);
    }

    public IEnumerable<Staff> GetStaffWhere(Expression<Func<Staff, bool>> where)
    {
        return _staffRepository.GetAll()
            .Include(s => s.InverseManager)
            .Include(s => s.Manager)
            .Include(s => s.Orders)
            .Include(s => s.Store);
    }

    public Staff GetStaffById(int id)
    {
        if (!_staffRepository.IdExists(id))
            return null;

        return _staffRepository.Get(s => s.StaffId == id)
            .Include(s => s.InverseManager)
            .Include(s => s.Manager)
            .Include(s => s.Orders)
            .Include(s => s.Store).First();
    }

    public bool AddStaff(Staff staff)
    {
        if (!_staffRepository.Add(staff))
            return false;

        return true;
    }

    public bool DeleteStaff(Staff staff)
    {
        if (!_staffRepository.Delete(staff))
            return false;

        return true;
    }

    public bool UpdateStaff(Staff staff)
    {
        if (!_staffRepository.Update(staff))
            return false;

        return true;
    }

    public IEnumerable<Store> GetStores()
    {
        return _storeRepository.GetAll()
            .Include(s => s.Orders)
            .Include(s => s.Staff)
            .Include(s => s.Stocks);
    }

    public IEnumerable<Store> GetStoresWhere(Expression<Func<Store, bool>> where)
    {
        return _storeRepository.Get(where)
            .Include(s => s.Orders)
            .Include(s => s.Staff)
            .Include(s => s.Stocks);
    }

    public Store GetStoreById(int id)
    {
        if (!_storeRepository.IdExists(id))
            return null;

        return _storeRepository.Get(s => s.StoreId == id)
            .Include(s => s.Orders)
            .Include(s => s.Staff)
            .Include(s => s.Stocks).First();
    }

    public bool AddStore(Store store)
    {
        if (!_storeRepository.Add(store))
            return false;

        return true;
    }

    public bool DeleteStore(Store store)
    {
        if (!_storeRepository.Delete(store))
            return false;

        return true;
    }

    public bool UpdateStore(Store store)
    {
        if (!_storeRepository.Update(store))
            return false;

        return true;
    }
}

public interface ISalesService
{
    IEnumerable<Order> GetOrders();
    IEnumerable<Order> GetOrdersWhere(Expression<Func<Order, bool>> where);
    Order GetOrderById(int id);
    bool AddOrder(Order order);
    bool DeleteOrder(Order order);
    bool UpdateOrder(Order order);
    bool AddOrderItem(OrderItem item);
    bool DeleteOrderItem(OrderItem item);
    IEnumerable<Customer> GetCustomers();
    IEnumerable<Customer> GetCustomersWhere(Expression<Func<Customer, bool>> where);
    Customer GetCustomerById(int id);
    bool AddCustomer(Customer customer);
    bool DeleteCustomer(Customer customer);
    bool UpdateCustomer(Customer customer);
    IEnumerable<Staff> GetStaff();
    IEnumerable<Staff> GetStaffWhere(Expression<Func<Staff, bool>> where);
    Staff GetStaffById(int id);
    bool AddStaff(Staff staff);
    bool DeleteStaff(Staff staff);
    bool UpdateStaff(Staff staff);
    IEnumerable<Store> GetStores();
    IEnumerable<Store> GetStoresWhere(Expression<Func<Store, bool>> where);
    Store GetStoreById(int id);
    bool AddStore(Store store);
    bool DeleteStore(Store store);
    bool UpdateStore(Store store);
}
